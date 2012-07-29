using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class EnemyGroup
    {
        public int Random;
        public List<EnemyGroupEntry> Group;

        public EnemyGroup()
        {
            Group = new List<EnemyGroupEntry>();
        }

        #region Content Type Reader

        public class EnemyGroupReader : ContentTypeReader<EnemyGroup>
        {
            protected override EnemyGroup Read(ContentReader input,
                EnemyGroup existingInstance)
            {
                EnemyGroup sc = existingInstance;
                if (sc == null)
                {
                    sc = new EnemyGroup();
                }

                sc.Random = input.ReadInt32();
                sc.Group.AddRange(input.ReadObject<List<EnemyGroupEntry>>());
                return sc;
            }
        }
        #endregion
    }
}
