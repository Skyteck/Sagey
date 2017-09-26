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
        SpriteFont _TheFont;
        Texture2D SelectedBG;
        Texture2D normalBG;
        List<CraftingSlot> CraftSlots;

        public enum PanelMode
        {
            kModeAll,
            kModeHand,
            kModeFire
        }

        PanelMode CurrentMode = PanelMode.kModeHand;

        public CraftingPanel(Managers.ChemistryManager ChemM)
        {
            _ChemistryManager = ChemM;
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            _TheFont = content.Load<SpriteFont>("Fonts/Fipps");
            SelectedBG = content.Load<Texture2D>("Art/itemSlotSelected");
            normalBG = content.Load<Texture2D>("Art/itemSlotNormal");

            for(int i = 0; i < 30; i++)
            {
                CraftingSlot slot = new CraftingSlot(SelectedBG, normalBG, _TheFont);
                CraftSlots.Add(slot);
            }
        }

        public override void ProcessClick(Vector2 pos)
        {
            foreach (CraftingSlot slot in CraftSlots)
            {
                if (slot.MyRect.Contains(pos))
                {
                    _ChemistryManager.ProcessRecipe(slot.MyRecipe);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach(CraftingSlot slot in CraftSlots)
            {
                slot.Reset();
            }
            

            foreach (Recipe recipe in _ChemistryManager.ActiveRecipes)
            {
                ActivateSlot(recipe);
            }

            int rows = 5;
            int columns = 6;
            int bufferX = 48;
            int bufferY = 64;
            int itemsDrawn = 0;
            Vector2 StartPos = this._TopLeft;
            StartPos.X += 8;
            StartPos.Y += 8;
            List<CraftingSlot> currentSlots = new List<CraftingSlot>();
            switch(CurrentMode)
            {
                case PanelMode.kModeHand:
                    currentSlots = CraftSlots.FindAll(x => x.MyRecipe.MadeOnTag == WorldObject.WorldObjectTag.kNoneTag);
                    break;
                case PanelMode.kModeFire:
                    currentSlots = CraftSlots.FindAll(x => x.MyRecipe.MadeOnTag == WorldObject.WorldObjectTag.kFireTag);
                    break;
                default:
                    currentSlots = CraftSlots;
                    break;
            }

            if (currentSlots.Count > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Vector2 pos = new Vector2(StartPos.X + (j * bufferX), StartPos.Y + (i * bufferY));
                        currentSlots[itemsDrawn].Position = pos;
                        currentSlots[itemsDrawn].Draw(spriteBatch);
                        //spriteBatch.Draw(_ChemistryManager.ActiveRecipes[itemsDrawn].output.itemtexture, pos, Color.White);

                        itemsDrawn++;
                        if (itemsDrawn >= currentSlots.Count)
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void ActivateSlot(Recipe recipe)
        {
            CraftSlots.Find(x => x.Active == false).SetRecipe(recipe);
        }
    }

    public class CraftingSlot
    {
        public Texture2D itemTexture;
        SpriteFont font;
        public Recipe MyRecipe;
        public Vector2 Position;
        public Texture2D regularBG;
        public Texture2D selectedBG;
        public bool Selected = false;
        public bool Active = true;
        public Rectangle MyRect => new Rectangle((int)Position.X, (int)Position.Y, 32, 32);

        public CraftingSlot(Texture2D sBG, Texture2D nBG, SpriteFont f)
        {
            regularBG = nBG;
            selectedBG = sBG;
            font = f;
        }

        public void SetRecipe(Recipe recipe)
        {
            MyRecipe = recipe;
            Active = true;
        }

        public void Reset()
        {
            Active = false;
            MyRecipe = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Active)
            {
                Texture2D textureUsed;
                if (Selected)
                {
                    textureUsed = selectedBG;
                }
                else
                {
                    textureUsed = regularBG;
                }
                //draw BG
                spriteBatch.Draw(textureUsed, Position, Color.White);
                //draw recipe output
                spriteBatch.Draw(MyRecipe.RecipeTexture, Position, Color.White);
                //draw ingredient list
                int buffer = 20;
                int count = 0;
                foreach (Ingredient slot in MyRecipe.ingredients)
                {
                    Vector2 drawPos = new Vector2(Position.X, Position.Y + 20 + (buffer * count));
                    spriteBatch.DrawString(font, slot.Amount.ToString(), drawPos, Color.White);
                    //spriteBatch.DrawString(font, slot._ItemType, new Vector2(drawPos.X + 16, drawPos.Y), Color.White);
                    count++;
                }
                //gray ingredients out if can't make recipe
            }
        }
    }
}
