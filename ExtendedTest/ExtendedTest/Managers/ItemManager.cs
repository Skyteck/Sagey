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
        public List<Item> ItemList;
        public ItemManager(ContentManager c)
        {
            _Content = c;
            ItemList = new List<Item>();
        }

        public void LoadItems(String path)
        {
            
            var file = System.IO.File.ReadAllText(path);
            ItemList = JsonConvert.DeserializeObject<List<Item>>(file);
            //ItemList.Clear();




            //ItemList.Add(new Items.Log());
            //ItemList.Add(new Items.Fish());
            //ItemList.Add(new Items.Ore());
            //ItemList.Add(new Items.FishStick());
            //ItemList.Add(new Items.Matches());
            //ItemList.Add(new Items.Strawberry());


            foreach (Item item in ItemList)
            {
                item.itemtexture = GetTexture(item._Name + "Item");
            }

            //    string GiveUp = File.ReadAllText(@"Content\JSON\Items2.json");
            //    JObject jo = JObject.Parse(GiveUp);
            //    JObject effThis = (JObject)jo["Items"];
            //    IEnumerable<JToken> dsdsdsdsd = effThis.Values();

            //    foreach(JToken token in dsdsdsdsd)
            //    {
            //        Item newItem = new Item();
            //        newItem._Name = token["Name"].ToString();
            //        int itemID = Convert.ToUInt16(token["ID"]);
            //        if (Enum.IsDefined(typeof(Item.ItemType), itemID))
            //        {
            //            Item itemInList = ItemList.Find(x => x._Type == (Item.ItemType)itemID);
            //            if(itemInList == null)
            //            {
            //                newItem._Type = (Item.ItemType)itemID;
            //            }
            //            else
            //            {
            //                Console.WriteLine("Item ID: " + itemID + " Already in list.");
            //                newItem._Type = Item.ItemType.kItemError;
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine(newItem._Name + " incorrect item ID!");
            //            newItem._Type = Item.ItemType.kItemError;
            //        }
            //        newItem._Weight = (double)token["Weight"];
            //        newItem._SaleValue = (int)token["SaleValue"];
            //        newItem._Stackable = (bool)token["Stackable"];
            //        newItem._Uses = (int)token["Uses"];
            //        newItem.itemtexture = this.getTexture(newItem._Name);
            //        ItemList.Add(newItem);
            //    }

            //JsonSerializer ser = new JsonSerializer();
            //ser.NullValueHandling = NullValueHandling.Ignore;

            //Item testItem = ItemList[0];
            //using (StreamWriter sw = new StreamWriter(@"Content/JSON.wtf.json"))
            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    ser.Serialize(writer, testItem);
            //}

            //string test = JsonConvert.SerializeObject(testItem);
            //Console.WriteLine(test);
        }

        internal Texture2D GetTexture(Item item, bool selected = false)
        {
            return GetTexture(item._Name + "Item", selected);
        }

        public Texture2D GetTexture(String itemName, bool selected = false)
        {
            Texture2D newTex;
            try
            {
                if (selected)
                {
                    newTex = _Content.Load<Texture2D>("Art/"+ itemName + "Selected");
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

        public Item GetItem(String itemName)
        {
            return ItemList.Find(x => x._Name == itemName);
        }

        public Item GetItem(Item.ItemType itemType)
        {
            return ItemList.Find(x => x._Type == itemType);
        }
    }
}
