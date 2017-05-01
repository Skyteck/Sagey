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
            if(newTex == null)
            {
                Console.WriteLine("Failed loading texture for item: " + item._Name);
                //item texture wasn't found. Load default texture
                newTex = _Content.Load<Texture2D>("Art/nullTexture");
            }
            return newTex;
        }

        public void AddItem(Item item)
        {
            if(!(this.itemList.Count >= this.capacity))
            {
                Texture2D itemTex = getTexture(item);
                if(itemTex != null)
                {
                    item.itemtexture = getTexture(item);
                }
                itemList.Add(item);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 StartPos)
        {
            int i = 0;
            foreach(Item item in itemList)
            {
                Vector2 Pos = new Vector2(StartPos.X+(i * 32), StartPos.Y);
                item.Draw(spriteBatch, Pos);
            }
        }
    }
}
