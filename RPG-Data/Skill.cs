using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class Skill
    {
        public string Name;
        public int ConsumeMP;
        public float Power;
        public bool AoE;

        // 0 Physical
        // 1 Magical
        // 2 Heal
        public int DamageType;

        // Cast skill on Enemy or on the Team
        public bool CastOnTeam;

        #region Content Type Reader

        public class SkillReader : ContentTypeReader<Skill>
        {
            protected override Skill Read(ContentReader input,
                Skill existingInstance)
            {
                Skill sc = existingInstance;
                if (sc == null)
                {
                    sc = new Skill();
                }

                sc.Name = input.ReadString();
                sc.ConsumeMP = input.ReadInt32();
                sc.Power = input.ReadSingle();
                sc.AoE = input.ReadBoolean();
                sc.DamageType = input.ReadInt32();
                sc.CastOnTeam = input.ReadBoolean();
                return sc;
            }
        }
        #endregion
    }
}
