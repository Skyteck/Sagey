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
        public Form1()
        {
            InitializeComponent();
            listBox1.SelectionMode = SelectionMode.One;
            ItemList = new List<Sagey.Item>();

            string path = @"C:\Users\Richard.Ellzy\Desktop\New folder\Sagey\ExtendedTest\ExtendedTest\Content\JSON\ItemList.json";


            var file = System.IO.File.ReadAllText(path);
            ItemList = JsonConvert.DeserializeObject<List<Sagey.Item>>(file);

            foreach (Sagey.Item item in ItemList)
            {
                listBox1.Items.Add(item._Name);
            }
            listBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(ItemList[listBox1.SelectedIndex]._Name);
            tbItemName.Text = ItemList[listBox1.SelectedIndex]._Name;
            nSaleValue.Value = ItemList[listBox1.SelectedIndex]._SaleValue;
            nWeight.Value =  (Decimal)ItemList[listBox1.SelectedIndex]._Weight;
            cbStacks.Checked = ItemList[listBox1.SelectedIndex]._Stackable;


        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            Sagey.Item newItem = new Sagey.Item();
            listBox1.Items.Add(newItem);
            ItemList.Add(newItem);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int currentIndex = listBox1.SelectedIndex;
            listBox1.Items.Clear();


            string path = @"C:\Users\Richard.Ellzy\Desktop\New folder\Sagey\ExtendedTest\ExtendedTest\Content\JSON\ItemList.json";
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
    }
}
