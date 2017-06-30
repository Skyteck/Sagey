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
        SpriteFont count;
        Texture2D SelectedBG;

        public BankPanel(Managers.BankManager BM)
        {
            _BankManager = BM;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            count = content.Load<SpriteFont>("Fonts/Fipps");
            SelectedBG = content.Load<Texture2D>("Art/itemSlotSelected");
        }

        new public Item.ItemType ProcessClick(Vector2 pos)
        {
            foreach(InventorySlot slot in _BankManager.itemSlots)
            {
                if(slot.myRect.Contains(pos))
                {
                    return slot.ItemInSlot._Type;
                }
            }
            return Item.ItemType.kItemNone;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            int rows = 5;
            int columns = 6;
            int buffer = 30;
            int itemsDrawn = 0;
            Vector2 StartPos = this._TopLeft;
            StartPos.X += 8;
            StartPos.Y += 8;
            if (_BankManager.itemSlots.Count > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Vector2 pos = new Vector2(StartPos.X + (j * buffer), StartPos.Y + (i * buffer));
                        _BankManager.itemSlots[itemsDrawn]._Position = pos;

                        if (_BankManager.itemSlots[itemsDrawn].ItemInSlot == _BankManager._SelectedItem)
                        {
                            spriteBatch.Draw(SelectedBG, new Vector2(pos.X - 8, pos.Y - 8), Color.White);
                        }
                        _BankManager.itemSlots[itemsDrawn].ItemInSlot.Draw(spriteBatch, pos);
                        //if (_BankManager.itemSlots[itemsDrawn].ItemInSlot._Stackable)
                        //{
                            spriteBatch.DrawString(count, _BankManager.itemSlots[itemsDrawn].Amount.ToString(), new Vector2(pos.X + 8, pos.Y - 12), Color.White);
                        //}
                        itemsDrawn++;
                        if (itemsDrawn >= _BankManager.itemSlots.Count)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
