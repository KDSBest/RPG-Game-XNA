using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class Armour : Item
    {
        public int Defense;
        public int MagicDefense;

        #region Content Type Reader
        public class ArmourReader : ContentTypeReader<Armour>
        {
            protected override Armour Read(ContentReader input,
                Armour existingInstance)
            {
                Armour sc = existingInstance;
                if (sc == null)
                {
                    sc = new Armour();
                }

                sc.Name = input.ReadString();
                sc.Defense = input.ReadInt32();
                sc.MagicDefense = input.ReadInt32();

                return sc;
            }
        }
        #endregion  
    }
}
