using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class SkillPool
    {
        public List<Skill> Pool;

        public SkillPool()
        {
            Pool = new List<Skill>();
        }

        public Skill GetSkill(string Name)
        {
            foreach (Skill s in Pool)
            {
                if (s.Name == Name)
                    return s;
            }

            return null;
        }

        #region Content Type Reader

        public class SkillPoolReader : ContentTypeReader<SkillPool>
        {
            protected override SkillPool Read(ContentReader input,
                SkillPool existingInstance)
            {
                SkillPool sc = existingInstance;
                if (sc == null)
                {
                    sc = new SkillPool();
                }

                sc.Pool.AddRange(input.ReadObject<List<Skill>>());

                return sc;
            }
        }
        #endregion
    }
}
