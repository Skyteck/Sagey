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
    public class InventoryManager
    {
        List<InventorySlot> itemList;
        int capacity = 28;
        ContentManager _Content;
        public InventoryManager(ContentManager content)
        {
            _Content = content;
            itemList = new List<InventorySlot>();
            
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

        public void AddItem(Item item, int amount = 1)
        {
            //find if the item already exists in a slot
            InventorySlot itemSlot = itemList.Find(x => x.itemInSlot.ID == item.ID);
            if (item._Stackable && itemSlot != null) //item is stackable and a slot for it was found
            {
                itemSlot.amount += amount;
            }
            else //item not stackable or item not found, create a new slot
            {
                if(itemList.Count < this.capacity) //Bag can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new InventorySlot();
                    //create the item
                    //put item in slot
                    item.itemtexture = getTexture(item); 
                    itemSlot.itemInSlot = item;
                    itemSlot.amount = 1;
                    itemList.Add(itemSlot);
                }
                else
                {
                    //error adding item message;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 StartPos)
        {
            int rows = 5;
            int columns = 6;
            int buffer = 30;
            int itemsDrawn = 0;
            StartPos.X += 8;
            StartPos.Y += 8;
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    Vector2 pos = new Vector2(StartPos.X + (j * buffer), StartPos.Y + (i * buffer));
                    itemList[itemsDrawn].itemInSlot.Draw(spriteBatch, pos);
                    itemsDrawn++;
                    if(itemsDrawn >= itemList.Count)
                    {
                        return;
                    }
                }
            }
        }
    }


}
