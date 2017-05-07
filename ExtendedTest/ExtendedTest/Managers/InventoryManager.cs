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
                if(itemList.Count < this.capacity) //Bad can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new InventorySlot();
                    //create the item
                    Texture2D itemTex = getTexture(item);
                    //put item in slot
                    item.itemtexture = itemTex;
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
            int i = 0;
            foreach(InventorySlot item in itemList)
            {
                Vector2 Pos = new Vector2(StartPos.X+(i * 32), StartPos.Y);
                item.itemInSlot.Draw(spriteBatch, Pos);
            }
        }
    }

    public class InventorySlot
    {
        public Item itemInSlot;
        public int amount;
        public InventorySlot()
        {

        }
    }
}
