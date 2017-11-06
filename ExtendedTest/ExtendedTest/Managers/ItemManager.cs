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

namespace Sagey.Managers
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


            foreach (Item item in ItemList)
            {
                item.itemtexture = GetTexture(item._Name.Replace(" ", "") + "Item");
            }
            
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

        public Item GetItem(Enums.ItemID itemType)
        {
            return ItemList.Find(x => x._ID == itemType);
        }
    }
}
