using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest.GameObjects.UIObjects
{
    public class InventoryPanel : UIPanel
    {
        Managers.InventoryManager _InventoryManager;
        List<Rectangle> rectList;
        SpriteFont count;
        Texture2D SelectedBG;
        int scrollPos = 0;
        int itemsDrawn = 0;

        public InventoryPanel(Managers.InventoryManager invenM)
        {
            _InventoryManager = invenM;
            rectList = new List<Rectangle>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            count = content.Load<SpriteFont>("Fonts/Fipps");
            SelectedBG = content.Load<Texture2D>("Art/itemSlotSelected");
        }

        new public Item.ItemType ProcessClick(Vector2 pos)
        {
            foreach (InventorySlot slot in _InventoryManager.itemSlots)
            {
                if (slot.myRect.Contains(pos))
                {
                    return slot.ItemInSlot._Type;
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
            StartPos.X += 8;
            StartPos.Y += 8;

            while(itemsDrawn < _InventoryManager.itemSlots.Count)
            {
                //where to draw?
                Vector2 pos = new Vector2(StartPos.X + (currentColumn * buffer), StartPos.Y + (currentRow * buffer));
                //draw
                _InventoryManager.itemSlots[itemsDrawn]._Position = pos;
                _InventoryManager.itemSlots[itemsDrawn].ItemInSlot.Draw(spriteBatch, pos);
                if (_InventoryManager.itemSlots[itemsDrawn].ItemInSlot._Stackable)
                {
                    spriteBatch.DrawString(count, _InventoryManager.itemSlots[itemsDrawn].Amount.ToString(), new Vector2(pos.X + 8, pos.Y - 12), Color.White);
                }
                //done drawing
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
        }

    }

    class InventoryDrawSpot
    {
        SpriteFont font;
        InventorySlot mySlot;
        Vector2 pos;
        public InventoryDrawSpot(SpriteFont f, InventorySlot slot, Vector2 p)
        {
            font = f;
            mySlot = slot;
            pos = p;
        }
    }
}
