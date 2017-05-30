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
        SpriteFont count;

        public InventoryPanel(Managers.InventoryManager invenM)
        {
            _InventoryManager = invenM;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            count = content.Load<SpriteFont>("Fonts/Fipps");

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
}
