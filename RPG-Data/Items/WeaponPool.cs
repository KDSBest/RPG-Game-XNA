using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class WeaponPool
    {
        public List<Weapon> Pool;

        public WeaponPool()
        {
            Pool = new List<Weapon>();
        }

        #region Content Type Reader
        public class WeaponPoolReader : ContentTypeReader<WeaponPool>
        {
            protected override WeaponPool Read(ContentReader input,
                WeaponPool existingInstance)
            {
                WeaponPool sc = existingInstance;
                if (sc == null)
                {
                    sc = new WeaponPool();
                }

                sc.Pool.AddRange(input.ReadObject<List<Weapon>>());

                return sc;
            }
        }
        #endregion
    }
}
