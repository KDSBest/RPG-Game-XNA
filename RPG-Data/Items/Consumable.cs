using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    public class Consumable : Item
    {

        public Consumable()
        {
            Action = new List<ScriptEngineCommand>();
        }

        private List<ScriptEngineCommand> action;

        public List<ScriptEngineCommand> Action
        {
            get { return action; }
            set { action = value; }
        }

        #region Content Type Reader
        public class ConsumableReader : ContentTypeReader<Consumable>
        {
            protected override Consumable Read(ContentReader input,
                Consumable existingInstance)
            {
                Consumable sc = existingInstance;
                if (sc == null)
                {
                    sc = new Consumable();
                }

                sc.Name = input.ReadString();
                sc.Action.AddRange(input.ReadObject<List<ScriptEngineCommand>>());

                return sc;
            }
        }
        #endregion  
    }
}
