using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class ItemDrop
    {
        public string ItemName;
        public int Chance;

        #region Content Type Reader

        public class ItemDropReader : ContentTypeReader<ItemDrop>
        {
            protected override ItemDrop Read(ContentReader input,
                ItemDrop existingInstance)
            {
                ItemDrop sc = existingInstance;
                if (sc == null)
                {
                    sc = new ItemDrop();
                }

                sc.ItemName = input.ReadString();
                sc.Chance = input.ReadInt32();
                return sc;
            }
        }
        #endregion
    }
}
