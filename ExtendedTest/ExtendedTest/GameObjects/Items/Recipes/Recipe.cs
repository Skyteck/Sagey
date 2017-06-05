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
        public List<Ingredient> ingredients;
        public Item output;
        public int amount;
        public float CraftingTime;
        public WorldObject.WorldObjectTag MadeOnTag;

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
