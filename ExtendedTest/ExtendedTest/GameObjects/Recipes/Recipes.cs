using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Recipes
{
    class MatchesRecipe : Recipe
    {
        public MatchesRecipe()
        {
            Name = "Matches";

            Ingredient ingredient = new Ingredient();
            ingredient._ItemType = Item.ItemType.kItemLog;
            ingredient.Amount = 2;
            ingredients.Add(ingredient);

            ingredient = new Ingredient();
            ingredient._ItemType = Item.ItemType.kItemOre;
            ingredient.Amount = 1;
            ingredients.Add(ingredient);

            output = Item.ItemType.kItemMatches;
            amount = 2;
            CraftingTime = 1.0f; //one second?
            MadeOnTag = WorldObject.WorldObjectTag.kNoneTag;
        }        
    }

    class FishStickRecipe : Recipe
    {
        public FishStickRecipe()
        {
            Name = "Fish Stick";

            Ingredient ingredient = new Ingredient();
            ingredient._ItemType = Item.ItemType.kItemFish;
            ingredient.Amount = 1;
            ingredients.Add(ingredient);

            ingredient = new Ingredient();
            ingredient._ItemType = Item.ItemType.kItemLog;
            ingredient.Amount = 1;
            ingredients.Add(ingredient);

            output = Item.ItemType.kItemFishStick;
            amount = 1;
            CraftingTime = 1f;
            MadeOnTag = WorldObject.WorldObjectTag.kFireTag;
        }
    }

    class DoubleLogRecipe : Recipe
    {
        public DoubleLogRecipe()
        {
            Name = "2xLog";
            Ingredient ingredient = new Ingredient();
            ingredient._ItemType = Item.ItemType.kItemLog;
            ingredient.Amount = 2;
            ingredients.Add(ingredient);

            output = Item.ItemType.kItemLog;
            amount = 1;
            CraftingTime = 0.5f;
            MadeOnTag = WorldObject.WorldObjectTag.kNoneTag;
        }
    }
}
