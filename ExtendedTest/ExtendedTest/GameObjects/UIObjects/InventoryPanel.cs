using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Sagey.GameObjects.UIObjects
{
    public class InventoryPanel : UIPanel
    {
        public event EventHandler ItemSelected;

        Managers.InventoryManager _InventoryManager;
        List<DrawSpot> _PanelSpots;
        DrawSpot SelectedSpot = null;
        int itemsDrawn = 0;

        public InventoryPanel(Managers.InventoryManager invenM)
        {
            _InventoryManager = invenM;
            _PanelSpots = new List<DrawSpot>();

            _InventoryManager.InventoryChanged += HandleInventoryChanged;
            
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);

            for (int i = 0; i < _InventoryManager.capacity; i++)
            {
                DrawSpot spot = new DrawSpot(extraTex);
                _PanelSpots.Add(spot);
            }
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            //if (this._BoundingBox.Contains(InputHelper.MouseScreenPos))
            //{
            //    foreach (DrawSpot slot in _PanelSpots)
            //    {
            //        if (slot.myRect.Contains(InputHelper.MouseScreenPos))
            //        {
            //            Console.WriteLine(slot.MouseOver);
            //        }
            //    }
            //}
        }

        public override void ProcessClick(Vector2 pos)
        {
            foreach (DrawSpot slot in _PanelSpots.FindAll(x=>x._Active == true))
            {
                if (slot.myRect.Contains(pos))
                {
                    DeselectSlot();
                    SelectSlot(slot);
                }
            }
        }


        private void ActivateSlot(ItemSlot slot, Vector2 pos)
        {
            _PanelSpots.Find(x => x._Active == false).Setup(slot, pos);
        }

        private void SelectSlot(DrawSpot spot)
        {
            SelectedSpot = spot;
            SelectedSpot._Selected = true;
            _InventoryManager.selectedItem = spot.mySlot.ItemInSlot;
        }

        private void DeselectSlot()
        {
            if (SelectedSpot == null) return;
            SelectedSpot._Selected = false;
            SelectedSpot = null;
            if(_InventoryManager.selectedItem != null)
            {
                _InventoryManager.selectedItem = null;
            }
        }

        public void OnItemSelected()
        {
            ItemSelected?.Invoke(this, EventArgs.Empty);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /* thinking through scroll bar
             * scroll down one click to increment items drawn by columns?
             * 
             * 
             * */
            base.Draw(spriteBatch);

            int buffer = 37;
            int columns = adJustedWidth / buffer;
            int rows = adjustedHeight / buffer;
            int toDraw = columns * rows;
            int itemsDrawn = 0;
            int currentRow = 0;
            int currentColumn = 0;

            if(toDraw < _InventoryManager.itemSlots.Count)
            {
                if (this._BoundingBox.Contains(InputHelper.MouseScreenPos) && InputHelper.MouseScrolled)
                {
                    if(InputHelper.MouseScrolledUp)
                    {
                        scrollPos--;
                        if(scrollPos < 0)
                        {
                            scrollPos = 0;
                        }
                    }
                    else if(InputHelper.MouseScrolledDown)
                    {
                        scrollPos++;
                        if(scrollPos > rows)
                        {
                            scrollPos = rows;
                        }
                    }
                }
            }
            else
            {
                scrollPos = 0;
            }
            
            itemsDrawn = columns * scrollPos;
            if(itemsDrawn>= _InventoryManager.itemSlots.Count)
            {
                itemsDrawn = _InventoryManager.itemSlots.Count - columns;
            }
            Vector2 StartPos = HelperFunctions.PointToVector(_TopEdge.Location);
            StartPos.X += 16;
            StartPos.Y += 16;

            ResetSlots();

            while (itemsDrawn < _InventoryManager.itemSlots.Count)
            {
                //where to draw?
                Vector2 pos = new Vector2(StartPos.X + (currentColumn * buffer), StartPos.Y + (currentRow * buffer));
                ActivateSlot(_InventoryManager.itemSlots[itemsDrawn], pos);

                currentColumn++;
                if(currentColumn >= columns)
                {
                    currentColumn = 0;
                    currentRow++;
                    if(currentRow >= rows)
                    {
                        break;
                    }
                }
                itemsDrawn++;
            }
            
            foreach(DrawSpot spot in _PanelSpots.FindAll(x=>x._Active == true))
            {
                spot.Draw(spriteBatch, count);
            }
        }

        private void ResetSlots()
        {
            foreach (DrawSpot spot in _PanelSpots.FindAll(x => x._Active == true))
            {
                spot.Reset();
            }
        }



        public void HandleInventoryChanged(object sender, EventArgs args)
        {
            //CheckRecipes();
        }
    }

    public class DrawSpot
    {
        public ItemSlot mySlot;
        public Vector2 _Position;
        public bool _Active = false;
        public Texture2D bgTex;
        public string MouseOver;
        int buffer = 34;
        public bool _Selected = false;
        public DrawSpot(Texture2D tex)
        {
            bgTex = tex;
        }

        public Rectangle myRect
        {
            get
            {
                return new Rectangle((int)_Position.X - 8, (int)_Position.Y - 8, buffer, buffer);
            }
        }

        public void Setup(ItemSlot slot, Vector2 pos)
        {
            mySlot = slot;
            _Position = pos;
            _Active = true;
            MouseOver = mySlot.ItemInSlot._Name;
        }

        public void Reset()
        {
            mySlot = null;
            _Position = Vector2.Zero;
            _Active = false;
            MouseOver = string.Empty;
        }

        public void Draw(SpriteBatch sb, SpriteFont font)
        {
            if (!_Active) return;
            //draw BG
            if(_Selected)
            {
                int border = 3;
                Rectangle rect = myRect;
                sb.Draw(bgTex, new Rectangle(rect.X, rect.Y, border, rect.Height + border), Color.White);
                sb.Draw(bgTex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);
                sb.Draw(bgTex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);
                sb.Draw(bgTex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
            }
            //sb.Draw(bgTex, myRect, Color.White);
            sb.Draw(mySlot.ItemInSlot.itemtexture, new Vector2(_Position.X - 8, _Position.Y + 8), Color.White);

            if(mySlot.ItemInSlot._Stackable)
            {
                sb.DrawString(font, mySlot.Amount.ToString(), new Vector2(_Position.X, _Position.Y - font.LineSpacing), Color.White);
            }
        }
    }
}
