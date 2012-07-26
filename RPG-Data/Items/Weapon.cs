using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class Weapon : Item
    {
        public int Damage;
        public int MagicDamage;


        #region Content Type Reader
        public class WeaponReader : ContentTypeReader<Weapon>
        {
            protected override Weapon Read(ContentReader input,
                Weapon existingInstance)
            {
                Weapon sc = existingInstance;
                if (sc == null)
                {
                    sc = new Weapon();
                }

                sc.Name = input.ReadString();
                sc.Damage = input.ReadInt32();
                sc.MagicDamage = input.ReadInt32();

                return sc;
            }
        }
        #endregion  
    }
}
