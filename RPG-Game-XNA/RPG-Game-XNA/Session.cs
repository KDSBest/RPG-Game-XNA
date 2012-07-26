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
            Map = Globals.Instance.Content.Load<Map>("Maps\\StartLevel");
        }
    }
}
