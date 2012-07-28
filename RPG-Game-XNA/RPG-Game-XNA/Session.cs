using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;

namespace RPG_Game_XNA
{
    public class Session
    {
        public static Session currentSession;
        public List<Character> Party;
        public Inventory Inventory;
        public Map Map;

        public Session()
        {
            Party = new List<Character>();
            Inventory = new Inventory();
            Party.Add(new Character("Natsu", 0, (Weapon)ItemPool.Instance.GetItem("Bare Hands"), (Armour)ItemPool.Instance.GetItem("T-Shirt"))); 
            Map = Globals.Instance.Content.Load<Map>("Maps\\StartLevel");
        }
    }
}
