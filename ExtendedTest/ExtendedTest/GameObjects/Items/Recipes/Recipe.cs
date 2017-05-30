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
        public List<itemSlot> ingredients;
        public Item output;
        public int amount;
        public float CraftingTime;
        public WorldObject.WorldObjectTag MadeOnTag;

        public Recipe()
        {
            ingredients = new List<itemSlot>();
        }

    }

    public class itemSlot
    {
        public string Name;
        public int Amount;
        public itemSlot()
        {

        }
    }
}
