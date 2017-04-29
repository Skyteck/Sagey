using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest
{
    class InventoryManager
    {
        public List<Item> itemList;
        int capacity = 28;
        ContentManager _Content;
        public InventoryManager(ContentManager content)
        {
            _Content = content;
            itemList = new List<Item>();
            
        }

        public Texture2D getTexture(Item item)
        {
            Texture2D newTex = _Content.Load<Texture2D>("Art/" + item._Name);
            if(newTex != null)
            {
                return newTex;
            }
            return null;
        }

        public void AddItem(Item item)
        {
            if(!(this.itemList.Count >= this.capacity))
            {
                item.itemtexture = getTexture(item);
                itemList.Add(item);
            }
        }
    }
}
