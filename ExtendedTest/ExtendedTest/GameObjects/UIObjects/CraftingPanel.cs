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
    public class CraftingPanel : UIPanel
    {
        Managers.ChemistryManager _ChemistryManager;
        SpriteFont count;
        Texture2D SelectedBG;
        Texture2D normalBG;
        List<CraftingSlot> CraftSlots;
        public CraftingPanel(Managers.ChemistryManager ChemM)
        {
            _ChemistryManager = ChemM;
            CraftSlots = new List<CraftingSlot>();
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            count = content.Load<SpriteFont>("Fonts/Fipps");
            SelectedBG = content.Load<Texture2D>("Art/itemSlotSelected");
            normalBG = content.Load<Texture2D>("Art/itemSlotNormal");
        }

        public override void UpdateActive(GameTime gameTime)
        {
            CraftSlots.Clear();
            foreach (Recipe recipe in _ChemistryManager.ActiveRecipes)
            {
                CraftingSlot slot = new CraftingSlot(SelectedBG, normalBG, recipe, count);
                CraftSlots.Add(slot);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            int rows = 5;
            int columns = 6;
            int bufferX = 48;
            int bufferY = 64;
            int itemsDrawn = 0;
            Vector2 StartPos = this._TopLeft;
            StartPos.X += 8;
            StartPos.Y += 8;

            if (CraftSlots.Count > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Vector2 pos = new Vector2(StartPos.X + (j * bufferX), StartPos.Y + (i * bufferY));
                        CraftSlots[itemsDrawn].Position = pos;
                        CraftSlots[itemsDrawn].Draw(spriteBatch);
                        //spriteBatch.Draw(_ChemistryManager.ActiveRecipes[itemsDrawn].output.itemtexture, pos, Color.White);

                        itemsDrawn++;
                        if (itemsDrawn >= CraftSlots.Count)
                        {
                            return;
                        }
                    }
                }
            }
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

        public CraftingSlot(Texture2D sBG, Texture2D nBG, Recipe recipe, SpriteFont f)
        {
            MyRecipe = recipe;
            regularBG = nBG;
            selectedBG = sBG;
            font = f;
        }

        public void SetRecipe(Recipe recipe)
        {
            MyRecipe = recipe;
            Active = true;
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
                spriteBatch.Draw(MyRecipe.output.itemtexture, Position, Color.White);
                //draw ingredient list
                int buffer = 20;
                int count = 0;
                foreach (itemSlot slot in MyRecipe.ingredients)
                {
                    Vector2 drawPos = new Vector2(Position.X, Position.Y + 20 + (buffer * count));
                    spriteBatch.DrawString(font, slot.Amount.ToString(), drawPos, Color.White);
                    spriteBatch.DrawString(font, slot.Name, new Vector2(drawPos.X + 16, drawPos.Y), Color.White);
                    count++;
                }
                //gray ingredients out if can't make recipe
            }
        }
    }
}