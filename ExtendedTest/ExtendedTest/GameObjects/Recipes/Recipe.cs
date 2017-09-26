using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest
{
    public class Recipe
    {
        public string Name;
        public String _HoverText = "Hover Text!";
        public List<Ingredient> ingredients;
        public Item.ItemType output;
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
        public Item.ItemType _ItemType;
        public int Amount;
        public Ingredient()
        {

        }
    }
}
