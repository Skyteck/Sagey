﻿using System;
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
        SpriteFont count;
        Texture2D SelectedBG;
        int scrollPos = 0;

        public InventoryPanel(Managers.InventoryManager invenM)
        {
            _InventoryManager = invenM;
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
            int itemsDrawn = 0;
            Vector2 StartPos = HelperFunctions.PointToVector(_TopEdge.Location);
            StartPos.X += 8;
            StartPos.Y += 8;
            if (_InventoryManager.itemSlots.Count > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Vector2 pos = new Vector2(StartPos.X + (j * buffer), StartPos.Y + (i * buffer));
                        _InventoryManager.itemSlots[itemsDrawn]._Position = pos;
                        

                        _InventoryManager.itemSlots[itemsDrawn].ItemInSlot.Draw(spriteBatch, pos);


                        if (_InventoryManager.itemSlots[itemsDrawn].ItemInSlot._Stackable)
                        {
                            spriteBatch.DrawString(count, _InventoryManager.itemSlots[itemsDrawn].Amount.ToString(), new Vector2(pos.X + 8, pos.Y - 12), Color.White);
                        }
                        itemsDrawn++;
                        if (itemsDrawn >= _InventoryManager.itemSlots.Count)
                        {
                            return;
                        }
                    }
                }
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
