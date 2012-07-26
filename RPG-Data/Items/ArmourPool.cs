using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class ArmourPool
    {
        public List<Armour> Pool;

        public ArmourPool()
        {
            Pool = new List<Armour>();
        }

        #region Content Type Reader

        public class ArmourPoolReader : ContentTypeReader<ArmourPool>
        {
            protected override ArmourPool Read(ContentReader input,
                ArmourPool existingInstance)
            {
                ArmourPool sc = existingInstance;
                if (sc == null)
                {
                    sc = new ArmourPool();
                }

                sc.Pool.AddRange(input.ReadObject<List<Armour>>());

                return sc;
            }
        }
        #endregion
    }
}
