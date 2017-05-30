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
    public class CraftingPanel : UIPanel
    {
        Managers.ChemistryManager _ChemistryManager;
        SpriteFont count;

        public CraftingPanel(Managers.ChemistryManager ChemM)
        {
            _ChemistryManager = ChemM;
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
            if (_ChemistryManager.ActiveRecipes.Count > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Vector2 pos = new Vector2(StartPos.X + (j * buffer), StartPos.Y + (i * buffer));

                        spriteBatch.Draw(_ChemistryManager.ActiveRecipes[itemsDrawn].output.itemtexture, pos, Color.White);
                        
                        itemsDrawn++;
                        if (itemsDrawn >= _ChemistryManager.ActiveRecipes.Count)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
