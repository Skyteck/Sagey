using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.GameObjects.UIObjects
{
    class BankPanel : UIPanel
    {
        Managers.BankManager _BankManager;
        List<DrawSpot> _PanelSpots;
        int itemsDrawn = 0;

        public BankPanel(Managers.BankManager bm)
        {
            _BankManager = bm;
            _PanelSpots = new List<DrawSpot>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);

            for (int i = 0; i < _BankManager._Capacity; i++)
            {
                DrawSpot spot = new DrawSpot(extraTex);
                _PanelSpots.Add(spot);
            }
        }

        new public Item.ItemType ProcessClick(Vector2 pos)
        {
            foreach (DrawSpot slot in _PanelSpots.FindAll(x => x._Active == true))
            {
                if (slot.myRect.Contains(pos))
                {
                    return slot.mySlot.ItemInSlot._Type;
                }
            }
            return Item.ItemType.kItemNone;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /* thinking through scroll bar
             * scroll down one click to increment items drawn by columns?
             * 
             * 
             * */
            base.Draw(spriteBatch);

            int buffer = 32;
            int columns = adJustedWidth / buffer;
            int rows = adjustedHeight / buffer;
            int toDraw = columns * rows;
            int itemsDrawn = 0;
            int currentRow = 0;
            int currentColumn = 0;

            if (toDraw < _BankManager.itemSlots.Count)
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
            if (itemsDrawn >= _BankManager.itemSlots.Count)
            {
                itemsDrawn = _BankManager.itemSlots.Count - columns;
            }
            Vector2 StartPos = HelperFunctions.PointToVector(_TopEdge.Location);
            StartPos.X += 8;
            StartPos.Y += 8;

            ResetSlots();

            while (itemsDrawn < _BankManager.itemSlots.Count)
            {
                //where to draw?
                Vector2 pos = new Vector2(StartPos.X + (currentColumn * buffer), StartPos.Y + (currentRow * buffer));
                ActivateSlot(_BankManager.itemSlots[itemsDrawn], pos);
                //draw
                //_InventoryManager.itemSlots[itemsDrawn]._Position = pos;
                //_InventoryManager.itemSlots[itemsDrawn].ItemInSlot.Draw(spriteBatch, pos);
                //_PanelSpots[itemsDrawn].Draw(spriteBatch);
                //spriteBatch.Draw(newSpot.mySlot.ItemInSlot.itemtexture, pos, Color.White);
                if (_BankManager.itemSlots[itemsDrawn].ItemInSlot._Stackable)
                {
                    spriteBatch.DrawString(count, _BankManager.itemSlots[itemsDrawn].Amount.ToString(), new Vector2(pos.X + 8, pos.Y - 12), Color.White);
                }
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

            foreach (DrawSpot spot in _PanelSpots.FindAll(x => x._Active == true))
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

        private void ActivateSlot(ItemSlot slot, Vector2 pos)
        {
            _PanelSpots.Find(x => x._Active == false).Setup(slot, pos);
        }

    }
}
