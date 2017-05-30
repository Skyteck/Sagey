using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class ChemistryManager
    {
        InventoryManager _InvenManager;
        WorldObjectManager _WorldObjectManager;
        NPCManager _NPCManager;
        ContentManager _Content;

        public List<Recipe> RecipeList;
        public List<Recipe> ActiveRecipes;
        public ChemistryManager(InventoryManager invenM, WorldObjectManager WOM, NPCManager NPCM, ContentManager content)
        {
            RecipeList = new List<Recipe>();
            ActiveRecipes = new List<Recipe>();
            _InvenManager = invenM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
            _Content = content;

            Recipe matches = new Recipe();
            matches.Name = "Matches";
            itemSlot slot = new itemSlot();
            slot.Name = "Log";
            slot.Amount = 2;
            matches.ingredients.Add(slot);
            slot = new itemSlot();
            slot.Name = "Ore";
            slot.Amount = 1;
            matches.ingredients.Add(slot);
            matches.output = new Matches();
            matches.amount = 1;
            matches.CraftingTime = 1.0f; //one second?
            matches.MadeOnTag = WorldObject.WorldObjectTag.kNoneTag;
            RecipeList.Add(matches);

            Recipe DoubleLog = new Recipe();
            DoubleLog.Name = "2xLog";
            slot = new itemSlot();
            slot.Name = "Log";
            slot.Amount = 1;
            DoubleLog.ingredients.Add(slot);
            DoubleLog.output = new Log();
            DoubleLog.amount = 1;
            DoubleLog.CraftingTime = 0.5f;
            DoubleLog.MadeOnTag = WorldObject.WorldObjectTag.kNoneTag;
            RecipeList.Add(DoubleLog);


        }

        public void LoadIcons()
        {
            foreach (Recipe recipe in RecipeList)
            {
                recipe.output.itemtexture = getTexture(recipe.output);
            }
        }

        public Texture2D getTexture(Item item)
        {
            Texture2D newTex;
            try
            {
                newTex = _Content.Load<Texture2D>("Art/" + item._Name);
            }
            catch
            {
                Console.WriteLine("Failed loading texture for item: " + item._Name);
                //item texture wasn't found. Load default texture
                newTex = _Content.Load<Texture2D>("Art/Nulltexture");
            }
            return newTex;
        }

        public void CheckRecipes()
        {
            foreach(Recipe recipe in RecipeList)
            {
                bool itemsFound = false;
                foreach(itemSlot slot in recipe.ingredients)
                {
                    int amt = _InvenManager.getItemCount(slot.Name);
                    if(amt >= slot.Amount)
                    {
                        itemsFound = true;
                    }
                    else
                    {
                        itemsFound = false;
                        break;
                    }
                }
                bool recipeInList = ActiveRecipes.Contains(recipe);
                if (itemsFound) //items needed found
                {
                    if(!recipeInList) // items needed found and the recipe isn't in the active list.
                    {
                        ActiveRecipes.Add(recipe);
                    }
                } 
                else //items not found, if recipe in active list remove it
                {
                    if(recipeInList)
                    {
                        ActiveRecipes.Remove(recipe);
                    }
                }
            }
        }
    }
}
