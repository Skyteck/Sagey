using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTest.Managers
{
    public class ItemManager
    {
        ContentManager _Content;
        List<Item> ItemList;
        public ItemManager(ContentManager c)
        {
            _Content = c;
            ItemList = new List<Item>();
        }

        public void LoadItems(String path)
        {
            string GiveUp = File.ReadAllText(@"Content\JSON\Items.json");
            JObject jo = JObject.Parse(GiveUp);
            JObject effThis = (JObject)jo["Items"];
            IEnumerable<JToken> dsdsdsdsd = effThis.Values();

            foreach(JToken token in dsdsdsdsd)
            {
                Item newItem = new Item();
                newItem._Name = token["Name"].ToString();
                int itemID = Convert.ToUInt16(token["ID"]);
                if (Enum.IsDefined(typeof(Item.ItemType), itemID))
                {
                    Item itemInList = ItemList.Find(x => x._Type == (Item.ItemType)itemID);
                    if(itemInList == null)
                    {
                        newItem._Type = (Item.ItemType)itemID;
                    }
                    else
                    {
                        Console.WriteLine("Item ID: " + itemID + " Already in list.");
                        newItem._Type = Item.ItemType.kItemError;
                    }
                }
                else
                {
                    Console.WriteLine(newItem._Name + " incorrect item ID!");
                    newItem._Type = Item.ItemType.kItemError;
                }
                newItem._Weight = (double)token["Weight"];
                newItem._SaleValue = (int)token["SaleValue"];
                newItem._Stackable = (bool)token["Stackable"];
                newItem._Uses = (int)token["Uses"];
                newItem.itemtexture = this.getTexture(newItem._Name);
                ItemList.Add(newItem);
            }

        }

        public Texture2D getTexture(String itemName, bool selected = false)
        {
            Texture2D newTex;
            try
            {
                if (selected)
                {
                    newTex = _Content.Load<Texture2D>("Art/" + itemName + "Selected");
                }
                else
                {
                    newTex = _Content.Load<Texture2D>("Art/" + itemName);
                }
            }
            catch
            {
                Console.WriteLine("Failed loading texture for item: " + itemName);
                //item texture wasn't found. Load default texture
                newTex = _Content.Load<Texture2D>("Art/Nulltexture");
            }
            return newTex;
        }
    }
}
