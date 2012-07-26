using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;

namespace RPG_Game_XNA
{
    public class ItemPool
    {
        private ArmourPool ArmourPool;
        private ConsumablePool ConsumablePool;
        private WeaponPool WeaponPool;

        private ItemPool()
        {
            ArmourPool = Globals.Instance.Content.Load<ArmourPool>("ArmourPool");
            ConsumablePool = Globals.Instance.Content.Load<ConsumablePool>("ConsumablePool");
            WeaponPool = Globals.Instance.Content.Load<WeaponPool>("WeaponPool");
        }

        public Item GetItem(string Name)
        {
            foreach(Item item in ConsumablePool.Pool)
                if(item.Name == Name)
                    return item;
            foreach(Item item in WeaponPool.Pool)
                if(item.Name == Name)
                    return item;
            foreach(Item item in ArmourPool.Pool)
                if(item.Name == Name)
                    return item;
            return null;
        }

        #region Singleton pattern
        private static ItemPool _instance;
        public static ItemPool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemPool();
                return _instance;
            }
        }
        #endregion
    }  
}
