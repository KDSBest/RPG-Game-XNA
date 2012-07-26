using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;

namespace RPG_Game_XNA
{
    public class Inventory
    {
        private Dictionary<Item, int> Items;
        public const int MaxItemStack = 99;

        public Inventory()
        {
            Items = new Dictionary<Item, int>();
        }

        public void AddItem(string ItemName, int count)
        {
            AddItem(ItemPool.Instance.GetItem(ItemName), count);
        }

        public void AddItem(Item item, int count)
        {
            if (Items.ContainsKey(item))
                Items[item] += count;
            else
                Items.Add(item, count);
            if (Items[item] > MaxItemStack)
                Items[item] = MaxItemStack;
        }

        public bool RemoveItem(string ItemName, int count)
        {
            return RemoveItem(ItemPool.Instance.GetItem(ItemName), count);
        }

        public bool RemoveItem(Item item, int count)
        {
            if (Items.ContainsKey(item))
            {
                if (Items[item] < count)
                    return false;
                if(Items[item] == count)
                {
                    Items.Remove(item);
                    return true;
                }
                Items[item] -= count;
                return true;
            }
            return false;
        }
    }
}
