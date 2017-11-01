using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sagey.Managers
{
    public class InventoryManager
    {
        public event EventHandler InventoryChanged;

        public List<ItemSlot> itemSlots;
        public int capacity = 28;

        //Managers
        ItemManager _ItemManager;
        
        public Item selectedItem;

        public InventoryManager(ItemManager IM)
        {
            _ItemManager = IM;
            itemSlots = new List<ItemSlot>();
            
        }
                
        public void AddItem(Enums.ItemID itemType, int amount = 1)
        {
            //find if the item already exists in a slot
            if (itemType == Enums.ItemID.kItemNone) return;
            bool dirty = false;
            Item itemToAdd = _ItemManager.GetItem(itemType);

            if(itemToAdd == null)
            {
                Console.WriteLine("Error finding itemtype: " + itemType);
                return;
            }

            ItemSlot itemSlot = itemSlots.Find(x => x.ItemInSlot._ID == itemToAdd._ID);

            //item stackable, slot not found make one with amount
            //item not stackable, create new slot with it


            //item stackable, slot found. add to it.
            if (itemToAdd._Stackable && itemSlot != null) //item is stackable and a slot for it was found
            {
                itemSlot.Amount += amount;
                dirty = true;
            }
            else if(itemToAdd._Stackable && itemSlot == null)
            {
                if (itemSlots.Count < this.capacity) //Bag can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new ItemSlot();
                    //create the item
                    //put item in slot
                    itemToAdd.itemtexture = _ItemManager.GetTexture(itemToAdd);
                    itemSlot.ItemInSlot = itemToAdd;
                    itemSlot.Amount = amount;
                    itemSlots.Add(itemSlot);
                    dirty = true;

                }
                else
                {
                    //error adding item message;
                }
            }
            else //item not stackable or item not found, create a new slot
            {
                for(int i = 0; i < amount; i++)
                {
                    if(itemSlots.Count < this.capacity) //Bag can only be so full...
                    {
                        // create new slot for the item
                        itemSlot = new ItemSlot();
                        //create the item
                        //put item in slot
                        itemToAdd.itemtexture = _ItemManager.GetTexture(itemToAdd); 
                        itemSlot.ItemInSlot = itemToAdd;
                        itemSlot.Amount = 1;
                        itemSlots.Add(itemSlot);
                        dirty = true;
                    
                    }
                    else
                    {
                        //error adding item message;
                    }
                }
                
            }

            if(dirty)
            {
                OnInventoryChanged();
            }
        }

        public void AddItem(GameObjects.Items.ItemBundle bundle)
        {
            AddItem(bundle.outputID, bundle.amount);
        }

        public void RemoveItem(Enums.ItemID itemType, int amount = 1)
        {
            List<ItemSlot> itemSlot = itemSlots.FindAll(x => x.ItemInSlot._ID == itemType);
            ReallyRemoveItem(itemSlot, amount);

        }

        internal void RemoveItem(string name, int amount)
        {
            List<ItemSlot> itemSlot = itemSlots.FindAll(x => x.ItemInSlot._Name == name);

            ReallyRemoveItem(itemSlot, amount);
        }

        private void ReallyRemoveItem(List<ItemSlot> Slots, int Amount)
        {
            int numberRemoved = 0;
            bool dirty = false;
            foreach(ItemSlot slot in Slots)
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
                            dirty = true;
                        }
                    }
                    else
                    {
                        itemSlots.Remove(slot);
                        dirty = true;
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
            if(dirty)
            {
                OnInventoryChanged();
            }
        }
        

        public void SelectItem(Item item)
        {
            foreach(ItemSlot itemSlot in itemSlots)
            {
                if(itemSlot.ItemInSlot == item)
                {
                    selectedItem = itemSlot.ItemInSlot;
                    return;
                }
            }
        }

        public int getItemCount(Enums.ItemID itemType)
        {
            ItemSlot itemSlot = itemSlots.Find(x => x.ItemInSlot._ID == itemType);
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
                int count = itemSlots.Count(x => x.ItemInSlot._ID == itemType);
                return count;
            }
        }

        public List<string> getList()
        {
            List<string> items = new List<string>();
            foreach (ItemSlot slot in itemSlots)
            {
                string thing = string.Format("{0} {1}", (int)slot.ItemInSlot._ID, slot.Amount);
                items.Add(thing);
            }
            return items;
        }
        
        public void OnInventoryChanged()
        {
            InventoryChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
