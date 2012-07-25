using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGData
{
    public class Consumable : Item
    {
        private List<ScriptEngineCommand> action;

        public List<ScriptEngineCommand> Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}
