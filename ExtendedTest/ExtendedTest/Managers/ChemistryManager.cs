﻿using Microsoft.Xna.Framework.Content;
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
        ItemManager _ItemManager;

        public List<Recipe> RecipeList;
        public List<Recipe> ActiveRecipes;

        public ChemistryManager(InventoryManager invenM, WorldObjectManager WOM, NPCManager NPCM, ContentManager content, ItemManager IM)
        {
            RecipeList = new List<Recipe>();
            ActiveRecipes = new List<Recipe>();
            _InvenManager = invenM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
            _Content = content;
            _ItemManager = IM;

            Recipe matches = new Recipes.MatchesRecipe();
            RecipeList.Add(matches);

            Recipe DoubleLog = new Recipes.DoubleLogRecipe();
            RecipeList.Add(DoubleLog);

            Recipe fishStick = new Recipes.FishStickRecipe();

            RecipeList.Add(fishStick);
        }

        public void LoadIcons()
        {
            foreach (Recipe recipe in RecipeList)
            {
                recipe.RecipeTexture = _ItemManager.GetTexture(_ItemManager.GetItem(recipe.output)._Name+"Item");
            }
        }

        public void CheckRecipes()
        {
            foreach(Recipe recipe in RecipeList)
            {
                bool itemsFound = false;
                foreach(Ingredient slot in recipe.ingredients)
                {
                    int amt = _InvenManager.getItemCount(slot._ItemType);
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

        public void ProcessRecipe(Recipe recipe)
        {
            //make sure the recipe is active...

            Recipe theRecipe = ActiveRecipes.Find(x => x == recipe);
            if(theRecipe != null)
            {
                foreach(Ingredient slot in recipe.ingredients)
                {
                    _InvenManager.RemoveItem(slot._ItemType, slot.Amount);                    
                }
                _InvenManager.AddItem(recipe.output, recipe.amount);
                this.CheckRecipes();
            }
            else
            {
                Console.WriteLine("Null recipe:" + recipe.Name);
            }
        }

    }
}
