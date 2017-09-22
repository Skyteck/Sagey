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
        int capacity = 28;

        //Managers
        ItemManager _ItemManager;
        
        public Item selectedItem;

        public InventoryManager(ItemManager IM)
        {
            _ItemManager = IM;
            itemSlots = new List<InventorySlot>();
            
        }
                
        public void AddItem(Item.ItemType itemType, int amount = 1)
        {
            //find if the item already exists in a slot
            if (itemType == Item.ItemType.kItemNone) return;

            Item itemToAdd = _ItemManager.GetItem(itemType);

            if(itemToAdd == null)
            {
                Console.WriteLine("Error finding itemtype: " + itemType);
                return;
            }

            InventorySlot itemSlot = itemSlots.Find(x => x.ItemInSlot._Type == itemToAdd._Type);

            //item stackable, slot not found make one with amount
            //item not stackable, create new slot with it


            //item stackable, slot found. add to it.
            if (itemToAdd._Stackable && itemSlot != null) //item is stackable and a slot for it was found
            {
                itemSlot.Amount += amount;
            }
            else if(itemToAdd._Stackable && itemSlot == null)
            {
                if (itemSlots.Count < this.capacity) //Bag can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new InventorySlot();
                    //create the item
                    //put item in slot
                    itemToAdd.itemtexture = _ItemManager.GetTexture(itemToAdd);
                    itemSlot.ItemInSlot = itemToAdd;
                    itemSlot.Amount = amount;
                    itemSlots.Add(itemSlot);

                }
                else
                {
                    //error adding item message;
                }
            }
            else //item not stackable or item not found, create a new slot
            {
                if(itemSlots.Count < this.capacity) //Bag can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new InventorySlot();
                    //create the item
                    //put item in slot
                    itemToAdd.itemtexture = _ItemManager.GetTexture(itemToAdd); 
                    itemSlot.ItemInSlot = itemToAdd;
                    itemSlot.Amount = 1;
                    itemSlots.Add(itemSlot);
                    
                }
                else
                {
                    //error adding item message;
                }
            }
        }

        public void AddItem(GameObjects.Items.ItemBundle bundle)
        {
            AddItem(bundle.output, bundle.amount);
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

        public List<string> getList()
        {
            List<string> items = new List<string>();
            foreach (InventorySlot slot in itemSlots)
            {
                string thing = string.Format("{0} {1}", (int)slot.ItemInSlot._Type, slot.Amount);
                items.Add(thing);
            }
            return items;
        }
    }
}
