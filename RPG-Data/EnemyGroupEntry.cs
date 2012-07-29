using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class EnemyGroupEntry
    {
        public string Name;
        public string DisplayName;
        public int Level;
        public string WeaponName;
        public string ArmourName;
        public List<ScriptEngineCommand> AI;
        public int Experience;
        public List<ItemDrop> Items;

        public EnemyGroupEntry()
        {
            AI = new List<ScriptEngineCommand>();
            Items = new List<ItemDrop>();
        }

        #region Content Type Reader

        public class EnemyGroupEntryReader : ContentTypeReader<EnemyGroupEntry>
        {
            protected override EnemyGroupEntry Read(ContentReader input,
                EnemyGroupEntry existingInstance)
            {
                EnemyGroupEntry sc = existingInstance;
                if (sc == null)
                {
                    sc = new EnemyGroupEntry();
                }

                sc.Name = input.ReadString();
                sc.DisplayName = input.ReadString();
                sc.Level = input.ReadInt32();
                sc.WeaponName = input.ReadString();
                sc.ArmourName = input.ReadString();
                sc.AI.AddRange(input.ReadObject<List<ScriptEngineCommand>>());
                sc.Experience = input.ReadInt32();
                sc.Items.AddRange(input.ReadObject<List<ItemDrop>>());
                return sc;
            }
        }
        #endregion
    }
}
