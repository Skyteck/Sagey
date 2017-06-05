using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ExtendedTest.Managers
{
    public class InventoryManager
    {
        public List<InventorySlot> itemSlots;
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
            InventorySlot itemSlot = itemSlots.Find(x => x.ItemInSlot._Type == item._Type);
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

        public void RemoveItem(Item.ItemType itemType, int amount = 1)
        {
            List<InventorySlot> itemSlot = itemSlots.FindAll(x => x.ItemInSlot._Type == itemType);
            ReallyRemoveItem(itemSlot, amount);

        }

        internal void RemoveItem(string name, int amount)
        {
            List<InventorySlot> itemSlot = itemSlots.FindAll(x => x.ItemInSlot._Name == name);

            ReallyRemoveItem(itemSlot, amount);
        }

        private void ReallyRemoveItem(List<InventorySlot> Slots, int Amount)
        {
            int numberRemoved = 0;
            foreach(InventorySlot slot in Slots)
            {
                if (slot != null)
                {
                    if (slot.ItemInSlot._Stackable)
                    {
                        slot.Amount -= Amount;
                        numberRemoved = Amount;
                        if (slot.Amount <= 0)
                        {
                            itemSlots.Remove(slot);
                        }
                    }
                    else
                    {
                        itemSlots.Remove(slot);
                        numberRemoved++;
                    }

                    if (slot.ItemInSlot == selectedItem)
                    {
                        selectedItem = null;
                    }
                }
                if(numberRemoved >= Amount)
                {
                    break;
                }

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



        public int getItemCount(Item.ItemType itemType)
        {
            InventorySlot itemSlot = itemSlots.Find(x => x.ItemInSlot._Type == itemType);
            if(itemSlot == null)
            {
                return 0;
            }
            if (itemSlot.ItemInSlot._Stackable)
            {
                return itemSlot.Amount;
            }
            else
            {
                int count = itemSlots.Count(x => x.ItemInSlot._Type == itemType);
                return count;
            }

        }
    }


}
