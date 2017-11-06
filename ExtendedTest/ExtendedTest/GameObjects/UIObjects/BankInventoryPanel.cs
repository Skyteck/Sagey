using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.GameObjects.UIObjects
{
    class BankInventoryPanel : UIPanel
    {
        Managers.InventoryManager _InventoryManager;
        List<DrawSpot> _PanelSpots;
        int itemsDrawn = 0;

        public BankInventoryPanel(Managers.InventoryManager invenM)
        {
            _InventoryManager = invenM;
            _PanelSpots = new List<DrawSpot>();
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

            if (this._BoundingBox.Contains(InputHelper.MouseScreenPos))
            {
                foreach (DrawSpot slot in _PanelSpots)
                {
                    if (slot.myRect.Contains(InputHelper.MouseScreenPos))
                    {
                        Console.WriteLine(slot.MouseOver);
                    }
                }
            }
        }

        public Enums.ItemID ProcessClick(Vector2 pos)
        {
            foreach (DrawSpot slot in _PanelSpots.FindAll(x => x._Active == true))
            {
                if (slot.myRect.Contains(pos))
                {
                    return slot.mySlot.ItemInSlot._ID;
                }
            }
            return Enums.ItemID.kItemNone;
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

            if (toDraw < _InventoryManager.itemSlots.Count)
            {
                if (this._BoundingBox.Contains(InputHelper.MouseScreenPos) && InputHelper.MouseScrolled)
                {
                    if (InputHelper.MouseScrolledUp)
                    {
                        scrollPos--;
                        if (scrollPos < 0)
                        {
                            scrollPos = 0;
                        }
                    }
                    else if (InputHelper.MouseScrolledDown)
                    {
                        scrollPos++;
                        if (scrollPos > rows)
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
            if (itemsDrawn >= _InventoryManager.itemSlots.Count)
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
                //draw
                //_InventoryManager.itemSlots[itemsDrawn]._Position = pos;
                //_InventoryManager.itemSlots[itemsDrawn].ItemInSlot.Draw(spriteBatch, pos);
                //_PanelSpots[itemsDrawn].Draw(spriteBatch);
                //spriteBatch.Draw(newSpot.mySlot.ItemInSlot.itemtexture, pos, Color.White);
                //if (_PanelSpots[itemsDrawn].mySlot.ItemInSlot._Stackable)
                //{
                //    spriteBatch.DrawString(count, _InventoryManager.itemSlots[itemsDrawn].Amount.ToString(), new Vector2(pos.X + 8, pos.Y - 12), Color.White);
                //}
                //done drawing
                currentColumn++;
                if (currentColumn >= columns)
                {
                    currentColumn = 0;
                    currentRow++;
                    if (currentRow >= rows)
                    {
                        break;
                    }
                }
                itemsDrawn++;
            }

            //foreach (DrawSpot spot in _PanelSpots.FindAll(x => x._Active == true))
            //{
            //    spot.Draw(spriteBatch, count);
            //}
        }

        private void ResetSlots()
        {
            foreach (DrawSpot spot in _PanelSpots.FindAll(x => x._Active == true))
            {
                spot.Reset();
            }
        }

        private void ActivateSlot(ItemSlot slot, Vector2 pos)
        {
            _PanelSpots.Find(x => x._Active == false).Setup(slot, pos);
        }
    }
}
