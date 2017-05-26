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
        List<InventorySlot> itemSlots;
        SpriteFont count;
        int capacity = 28;
        ContentManager _Content;


        Texture2D normalBG;
        Texture2D SelectedBG;
        public Item selectedItem;

        public InventoryManager(ContentManager content)
        {
            _Content = content;
            itemSlots = new List<InventorySlot>();
            
        }

        public void loadContent()
        {
            count = _Content.Load<SpriteFont>("Fonts/Fipps");
            normalBG = _Content.Load<Texture2D>("Art/itemSlotNormal");
            SelectedBG = _Content.Load<Texture2D>("Art/itemSlotSelected");
        }

        public Texture2D getTexture(Item item, bool selected = false)
        {
            Texture2D newTex;
            try
            {
                if(selected)
                {
                    newTex = _Content.Load<Texture2D>("Art/" + item._Name + "Selected");
                }
                else
                {
                    newTex = _Content.Load<Texture2D>("Art/" + item._Name);
                }
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
            InventorySlot itemSlot = itemSlots.Find(x => x.ItemInSlot.ID == item.ID);
            if (item._Stackable && itemSlot != null) //item is stackable and a slot for it was found
            {
                itemSlot.Amount += amount;
            }
            else //item not stackable or item not found, create a new slot
            {
                if(itemSlots.Count < this.capacity) //Bag can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new InventorySlot();
                    //create the item
                    //put item in slot
                    item.itemtexture = getTexture(item); 
                    itemSlot.ItemInSlot = item;
                    itemSlot.Amount = 1;
                    itemSlots.Add(itemSlot);
                }
                else
                {
                    //error adding item message;
                }
            }
        }

        public void RemoveItem(Item item, int amount = 1)
        {
            if(item == selectedItem)
            {
                selectedItem = null;
            }
            InventorySlot itemSlot = itemSlots.Find(x => x.ItemInSlot.ID == item.ID);
            if(item._Stackable)
            {
                itemSlot.Amount -= amount;
                if(itemSlot.Amount<= 0)
                {
                    itemSlots.Remove(itemSlot);
                }
            }
            else
            {
                itemSlots.Remove(itemSlot);
            }

        }

        public Item checkClicks(Vector2 pos)
        {
            foreach(InventorySlot item in itemSlots)
            {
                if(item.myRect.Contains(pos))
                {
                    return item.ItemInSlot;
                }
            }
            return null;
        }

        public void SelectItem(Vector2 pos)
        {
            foreach (InventorySlot item in itemSlots)
            {
                if (item.myRect.Contains(pos))
                {
                    selectedItem = item.ItemInSlot;
                }
            }
        }

        public void SelectItem(Item item)
        {
            foreach(InventorySlot itemSlot in itemSlots)
            {
                if(itemSlot.ItemInSlot == item)
                {
                    selectedItem = itemSlot.ItemInSlot;
                    return;
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
            if(itemSlots.Count>0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Vector2 pos = new Vector2(StartPos.X + (j * buffer), StartPos.Y + (i * buffer));
                        itemSlots[itemsDrawn]._Position = pos;
                        if (itemSlots[itemsDrawn].ItemInSlot == selectedItem)
                        {
                            spriteBatch.Draw(SelectedBG, new Vector2(pos.X - 8, pos.Y - 8), Color.White);
                        }
                        itemSlots[itemsDrawn].ItemInSlot.Draw(spriteBatch, pos);
                        if (itemSlots[itemsDrawn].ItemInSlot._Stackable)
                        {
                            spriteBatch.DrawString(count, itemSlots[itemsDrawn].Amount.ToString(), new Vector2(pos.X + 8, pos.Y - 12), Color.White);
                        }
                        itemsDrawn++;
                        if (itemsDrawn >= itemSlots.Count)
                        {
                            return;
                        }
                    }
                }
            }

        }
    }


}
