using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagey.Managers
{
    public class BankManager
    {
        public event Delegates.GameEvent ItemBankedEvent;
        public event Delegates.GameEvent ItemRemovedEvent;

        public ItemManager _ItemManager;
        public List<ItemSlot> itemSlots;
        public Item _SelectedItem;
        public int _Capacity = 500;

        

        public BankManager(ItemManager IM)
        {
            _ItemManager = IM;
            itemSlots = new List<ItemSlot>();
        }

        public void AddItem(Enums.ItemID itemType, int amount = 1)
        {
            //find if the item already exists in a slot
            if (itemType == Enums.ItemID.kItemNone)
            {
                return;
            }
            Item itemToAdd = _ItemManager.GetItem(itemType);

            if (itemToAdd == null)
            {
                Console.WriteLine("Error finding itemtype: " + itemType);
                return;
            }


            ItemSlot itemSlot = itemSlots.Find(x => x.ItemInSlot._ID == itemToAdd._ID);
            if (itemSlot != null) //All items stack in the bank. Is there a slot for this one?
            {
                itemSlot.Amount += amount;
            }
            else //Slot not found. crate it
            {
                if (itemSlots.Count < _Capacity) //Bag can only be so full...
                {
                    // create new slot for the item
                    itemSlot = new ItemSlot();
                    //create the item
                    //put item in slot
                    itemToAdd.itemtexture = _ItemManager.GetTexture(itemToAdd);
                    itemSlot.ItemInSlot = itemToAdd;
                    itemSlot.Amount = amount;
                    itemSlots.Add(itemSlot);
                    OnItemBanked(itemToAdd._Name);

                }
                else
                {
                    //error adding item message;
                }
            }
        }

        //public void SelectItem(Vector2 pos)
        //{
        //    foreach (InventorySlot item in itemSlots)
        //    {
        //        if (item.myRect.Contains(pos))
        //        {
        //            _SelectedItem = item.ItemInSlot;
        //        }
        //    }
        //}

        //public void SelectItem(Item item)
        //{
        //    foreach (InventorySlot itemSlot in itemSlots)
        //    {
        //        if (itemSlot.ItemInSlot == item)
        //        {
        //            _SelectedItem = itemSlot.ItemInSlot;
        //            return;
        //        }
        //    }
        //}

        //public Item checkClicks(Vector2 pos)
        //{
        //    foreach (InventorySlot item in itemSlots)
        //    {
        //        if (item.myRect.Contains(pos))
        //        {
        //            return item.ItemInSlot;
        //        }
        //    }
        //    return null;
        //}

        public void RemoveItem(Enums.ItemID itemType, int amount = 1)
        {
            List<ItemSlot> itemSlot = itemSlots.FindAll(x => x.ItemInSlot._ID == itemType);
            ReallyRemoveItem(itemSlot, amount);

        }

        internal void RemoveItem(string name, int amount = 1)
        {
            List<ItemSlot> itemSlot = itemSlots.FindAll(x => x.ItemInSlot._Name == name);

            ReallyRemoveItem(itemSlot, amount);
        }

        private void ReallyRemoveItem(List<ItemSlot> Slots, int Amount)
        {
            int numberRemoved = 0;
            foreach (ItemSlot slot in Slots)
            {
                if (slot != null)
                {
                    slot.Amount -= Amount;
                    numberRemoved = Amount;
                    OnItemRemoved(slot.ItemInSlot._Name);
                    if (slot.Amount <= 0)
                    {
                        itemSlots.Remove(slot);
                    }

                    if (slot.ItemInSlot == _SelectedItem)
                    {
                        _SelectedItem = null;
                    }
                }
                if (numberRemoved >= Amount)
                {
                    break;
                }
            }
        }

        public List<string> getList()
        {
            List<string> items = new List<string>();
            foreach(ItemSlot slot in itemSlots)
            {
                string thing = string.Format("{0} {1}", (int)slot.ItemInSlot._ID, slot.Amount);
                items.Add(thing);
            }
            return items;
        }
        
        public void AttachEvents(EventManager em)
        {
            ItemBankedEvent += em.HandleEvent;
            ItemRemovedEvent += em.HandleEvent;
        }

        private void OnItemBanked(string itemName)
        {
            ItemBankedEvent?.Invoke(Enums.EventTypes.kEventItemBanked, itemName);
        }

        private void OnItemRemoved(string itemName)
        {
            ItemRemovedEvent?.Invoke(Enums.EventTypes.kEventItemRemoved, itemName);
        }
    }
}
