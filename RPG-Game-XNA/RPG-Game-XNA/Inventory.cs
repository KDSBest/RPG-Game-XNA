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

        public Dictionary<Consumable, int> GetConsumables()
        {
            Dictionary<Consumable, int> Consumable = new Dictionary<Consumable, int>();
            foreach (Item Item in Items.Keys)
            {
                if (Item is Consumable)
                    Consumable.Add((Consumable)Item, Items[Item]);
            }
            return Consumable;
        }

        public void GetItems(out Dictionary<Consumable, int> Consumable, out Dictionary<Weapon, int> Weapon, out Dictionary<Armour, int> Armour)
        {
            Consumable = new Dictionary<Consumable, int>();
            Weapon = new Dictionary<Weapon, int>();
            Armour = new Dictionary<Armour, int>();
            foreach (Item Item in Items.Keys)
            {
                if (Item is Consumable)
                    Consumable.Add((Consumable)Item, Items[Item]);
                if (Item is Weapon)
                    Weapon.Add((Weapon)Item, Items[Item]);
                if (Item is Armour)
                    Armour.Add((Armour)Item, Items[Item]);
            }
        }

        public bool IsUseable(Consumable Consumable, Character Char)
        {
            foreach (string check in Consumable.UseableChecks)
            {
                switch (check)
                {
                    case "IsAlive":
                        if (!Char.IsAlive)
                            return false;
                        break;
                    case "NotFullMP":
                        if (Char.MP == Char.MaxMP)
                            return false;
                        break;
                    case "NotFullHP":
                        if (Char.HP == Char.MaxHP)
                            return false;
                        break;
                }
            }
            return true;
        }
    }
}
