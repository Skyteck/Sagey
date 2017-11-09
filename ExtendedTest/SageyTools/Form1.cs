using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sagey.GameObjects.Items;
using Newtonsoft.Json;

namespace SageyTools
{
    public partial class Form1 : Form
    {
        List<Sagey.Item> ItemList;
        List<Sagey.Recipe> RecipeList;
        int ingredientCount = 0;
        bool ingredientChanged = false;
        public Form1()
        {
            InitializeComponent();

            ItemList = new List<Sagey.Item>();
            RecipeList = new List<Sagey.Recipe>();

            LoadItemStuff();
            LoadRecipeStuff();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(ItemList[listBox1.SelectedIndex]._Name);
            tbItemName.Text = ItemList[listBox1.SelectedIndex]._Name;
            nSaleValue.Value = ItemList[listBox1.SelectedIndex]._SaleValue;
            nWeight.Value =  (Decimal)ItemList[listBox1.SelectedIndex]._Weight;
            cbStacks.Checked = ItemList[listBox1.SelectedIndex]._Stackable;

            listBox2.SelectedIndex = (int)ItemList[listBox1.SelectedIndex]._ID-1;
        }

        private void tbItemName_TextChanged(object sender, EventArgs e)
        {
            ItemList[listBox1.SelectedIndex]._Name = tbItemName.Text;
        }

        private void nWeight_ValueChanged(object sender, EventArgs e)
        {
            ItemList[listBox1.SelectedIndex]._Weight = (int)nWeight.Value;
        }

        private void nSaleValue_ValueChanged(object sender, EventArgs e)
        {
            ItemList[listBox1.SelectedIndex]._SaleValue = (int)nSaleValue.Value;
        }

        private void cbStacks_CheckedChanged(object sender, EventArgs e)
        {
            ItemList[listBox1.SelectedIndex]._Stackable = cbStacks.Checked;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemList[listBox1.SelectedIndex]._ID = (Sagey.Enums.ItemID)listBox2.SelectedIndex + 1;

        }

        private void BtnNewItem_Click(object sender, EventArgs e)
        {
            Sagey.Item newItem = new Sagey.Item();
            listBox1.Items.Add(newItem);
            ItemList.Add(newItem);
        }

        private void btnSaveItems_Click(object sender, EventArgs e)
        {
            int currentIndex = listBox1.SelectedIndex;
            listBox1.Items.Clear();

            string cd = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.SetCurrentDirectory(@"..\..\..\ExtendedTest\Content\JSON");
            cd = System.IO.Directory.GetCurrentDirectory();
            string path = @"ItemList.json";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(ItemList, Formatting.Indented);
                file.WriteLine(json);
            }

            foreach (Sagey.Item item in ItemList)
            {
                listBox1.Items.Add(item._Name);
            }
            listBox1.SelectedIndex = currentIndex;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(tabControl1.SelectedIndex == 0)
            //{
            //    LoadItemStuff();
            //}
            //else if(tabControl1.SelectedIndex == 1)
            //{
            //    LoadRecipeStuff();
            //}
        }

        private void LoadItemStuff()
        {
            listBox1.SelectionMode = SelectionMode.One;
            string cd = System.IO.Directory.GetCurrentDirectory();
                System.IO.Directory.SetCurrentDirectory(@"..\..\..\ExtendedTest\Content\JSON");
            cd = System.IO.Directory.GetCurrentDirectory();
            string path = @"ItemList.json";


            Array values = Enum.GetValues(typeof(Sagey.Enums.ItemID));

            foreach (int value in values)
            {
                ListItem thing = new ListItem();
                thing.name = Enum.GetName(typeof(Sagey.Enums.ItemID), value);
                thing.value = value;
                listBox2.Items.Add(thing.name);
            }


            var file = System.IO.File.ReadAllText(path);
            ItemList = JsonConvert.DeserializeObject<List<Sagey.Item>>(file);

            foreach (Sagey.Item item in ItemList)
            {
                listBox1.Items.Add(item._Name);
            }
            listBox1.SelectedIndex = 0;
        }

        private void LoadRecipeStuff()
        {
            listBox3.SelectionMode = SelectionMode.One;


            string cd = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.SetCurrentDirectory(@"..\..\..\ExtendedTest\Content\JSON");
            cd = System.IO.Directory.GetCurrentDirectory();
            string path = @"RecipeList.json";

            var file = System.IO.File.ReadAllText(path);
            RecipeList = JsonConvert.DeserializeObject<List<Sagey.Recipe>>(file);


            Array values = Enum.GetValues(typeof(Sagey.Enums.ItemID));

            foreach (int value in values)
            {
                ListItem thing = new ListItem();
                thing.name = Enum.GetName(typeof(Sagey.Enums.ItemID), value);
                thing.value = value;
                cbItems.Items.Add(thing.name);
                cbIngreident.Items.Add(thing.name);
            }

            foreach (Sagey.Recipe recipe in RecipeList)
            {
                listBox3.Items.Add(recipe.Name);

            }



            listBox3.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecipeList[listBox3.SelectedIndex].outputID = (Sagey.Enums.ItemID)cbItems.SelectedIndex + 1;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbRecipeName.Text = RecipeList[listBox3.SelectedIndex ].Name;
            numAmount.Value = RecipeList[listBox3.SelectedIndex].amount;
            numCraftTime.Value = (Decimal)RecipeList[listBox3.SelectedIndex].CraftingTime;
            //find the item with the ID that matches the output item ID

            cbItems.SelectedIndex = (int)RecipeList[listBox3.SelectedIndex].outputID - 1;

            
            listBox4.Items.Clear();
            foreach (Sagey.Ingredient thing in RecipeList[listBox3.SelectedIndex].ingredients)
            {
                listBox4.Items.Add(thing.Name);
            }
            listBox4.SelectedIndex = 0;
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex < 0) return;
            if(ingredientChanged)
            {
                ingredientChanged = false;
                return;
            }
            int recipelistIndex = listBox3.SelectedIndex;
            int ingredientListIndex = listBox4.SelectedIndex;
            Console.WriteLine( RecipeList[recipelistIndex].Name);

            Console.WriteLine(RecipeList[recipelistIndex].ingredients[ingredientListIndex].Name);
            cbIngreident.SelectedIndex = (int)RecipeList[recipelistIndex].ingredients[ingredientListIndex]._ItemID-1 ;
            numIngredientAmt.Value = (decimal)RecipeList[recipelistIndex].ingredients[ingredientListIndex].Amount;


        }

        private void btnAddIngredient_Click_1(object sender, EventArgs e)
        {
            Sagey.Ingredient newOne = new Sagey.Ingredient();
            RecipeList[listBox3.SelectedIndex].ingredients.Add(newOne);
            listBox4.Items.Add(newOne.Name);
            listBox4.SelectedIndex = RecipeList[listBox3.SelectedIndex].ingredients.Count - 1;
        }

        private void tbRecipeName_TextChanged(object sender, EventArgs e)
        {
            RecipeList[listBox3.SelectedIndex].Name = tbRecipeName.Text;
        }

        private void numAmount_ValueChanged(object sender, EventArgs e)
        {
            RecipeList[listBox3.SelectedIndex].amount = (int)numAmount.Value;
        }

        private void numCraftTime_ValueChanged(object sender, EventArgs e)
        {
            RecipeList[listBox3.SelectedIndex].CraftingTime = (float)numCraftTime.Value;
        }

        private void cbIngreident_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sagey.Item itemFound = ItemList.Find(x => x._ID == (Sagey.Enums.ItemID)cbIngreident.SelectedIndex + 1);
            if(itemFound == null)
            {
                Console.WriteLine("No item found with ID: " + ((Sagey.Enums.ItemID)cbIngreident.SelectedIndex + 1));
                return;
            }
            RecipeList[listBox3.SelectedIndex].ingredients[listBox4.SelectedIndex]._ItemID = itemFound._ID;

            RecipeList[listBox3.SelectedIndex].ingredients[listBox4.SelectedIndex].Name = itemFound._Name;
            //listBox4.Items[listBox4.SelectedIndex] = ItemList.Find(x => x._ID == RecipeList[listBox3.SelectedIndex].ingredients[listBox4.SelectedIndex]._ItemID)._Name;
            ingredientChanged = true;
            //create a new ingreident of the selected type and amount
            Sagey.Ingredient newOne = new Sagey.Ingredient();
            newOne.Name = itemFound._Name;
            newOne.Amount = (int)numIngredientAmt.Value;
            // remove the ingredient from the recipe's ingredient list
            //RecipeList[listBox3.SelectedIndex].ingredients.RemoveAt(listBox4.SelectedIndex);
            ////add the new ingreident to recipe ingredient list
            //RecipeList[listBox3.SelectedIndex].ingredients.Add(newOne);
            //clear the listbox4
            listBox4.Items[listBox4.SelectedIndex] = newOne.Name;
            ////readd the recipes ingredients
            //foreach (Sagey.Ingredient thing in RecipeList[listBox3.SelectedIndex].ingredients)
            //{
            //    listBox4.Items.Add(thing.Name);
            //}
        }

        private void numIngredientAmt_ValueChanged(object sender, EventArgs e)
        {
            int recipelistIndex = listBox3.SelectedIndex;
            int ingredientListIndex = listBox4.SelectedIndex;
            if(ingredientListIndex <0)
            {
                ingredientListIndex = 0;
            }
            RecipeList[recipelistIndex].ingredients[ingredientListIndex].Amount = (int)numIngredientAmt.Value;
        }

        private void btnNewRecipe_Click(object sender, EventArgs e)
        {
            Sagey.Recipe newRecipe = new Sagey.Recipe();
            newRecipe.ingredients.Add(new Sagey.Ingredient());
            listBox3.Items.Add(newRecipe);
            RecipeList.Add(newRecipe);

            listBox3.SelectedIndex = listBox3.Items.Count - 1;
        }

        private void btnSaveRecipes_Click(object sender, EventArgs e)
        {

            int currentIndex = listBox1.SelectedIndex;
            listBox3.Items.Clear();

            string cd = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.SetCurrentDirectory(@"..\..\..\ExtendedTest\Content\JSON");
            cd = System.IO.Directory.GetCurrentDirectory();
            string path = @"RecipeList.json";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(RecipeList, Formatting.Indented);
                file.WriteLine(json);
            }

            foreach (Sagey.Recipe item in RecipeList)
            {
                listBox3.Items.Add(item.Name);
            }
            listBox3.SelectedIndex = currentIndex;
        }

        private void btnDeleteIngredient_Click(object sender, EventArgs e)
        {
            RecipeList[listBox3.SelectedIndex].ingredients.RemoveAt(listBox4.SelectedIndex);
            listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            listBox4.SelectedIndex = 0;
        }
    }

    public class ListItem
    {
        public string name;
        public int value;
    }
}
