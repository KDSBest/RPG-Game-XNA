using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class ConsumablePool
    {
        public List<Consumable> Pool;

        public ConsumablePool()
        {
            Pool = new List<Consumable>();
        }

        #region Content Type Reader

        public class ConsumablePoolReader : ContentTypeReader<ConsumablePool>
        {
            protected override ConsumablePool Read(ContentReader input,
                ConsumablePool existingInstance)
            {
                ConsumablePool sc = existingInstance;
                if (sc == null)
                {
                    sc = new ConsumablePool();
                }

                sc.Pool.AddRange(input.ReadObject<List<Consumable>>());

                return sc;
            }
        }
        #endregion
    }
}
