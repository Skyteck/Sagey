using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey
{
    public class Recipe
    {
        public string Name;
        public String _HoverText = "Hover Text!";
        public List<Ingredient> ingredients;
        public Enums.ItemID outputID;
        public int amount;
        public float CraftingTime;
        public WorldObject.WorldObjectTag MadeOnTag;
        public Texture2D RecipeTexture;

        public Recipe()
        {
            ingredients = new List<Ingredient>();
        }

    }

    public class Ingredient
    {
        public Enums.ItemID _ItemID;
        public int Amount;
        public Ingredient()
        {

        }
    }
}
