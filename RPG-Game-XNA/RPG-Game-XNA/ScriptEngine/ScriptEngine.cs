using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;

namespace RPG_Game_XNA.ScriptEngine
{
    public class ScriptEngine
    {
        private Dictionary<string, object> Context;

        public object GetVar(string Name)
        {
            if(Context.ContainsKey(Name))
                return Context[Name];
            return null;
        }

        public void SetVar(string Name, object Var)
        {
            if(Context.ContainsKey(Name))
                Context[Name] = Var;
            else
                Context.Add(Name, Var);
        }

        public ScriptEngine()
        {
            Context = new Dictionary<string, object>();
        }

        public void Execute(ScriptEngineCommand command)
        {
            switch (command.Type)
            {
                case "ShowText":
                    {
                        string[] Parameter = new string[command.Parameter.Count];
                        for (int i = 0; i < Parameter.Length; i++)
                            Parameter[i] = command.Parameter[i].Value;
                        GameStateManager.Instance.AddScreen(new PopUpScreen(Parameter), true, false);
                    }
                    break;
                case "GiveItem":
                    {
                        if (command.Parameter.Count == 2)
                        {
                            int count;
                            int.TryParse(command.Parameter[1].Value, out count);
                            Session.currentSession.Inventory.AddItem(command.Parameter[0].Value, count);
                        }
                        else if (command.Parameter.Count == 1)
                        {
                            Session.currentSession.Inventory.AddItem(command.Parameter[0].Value, 1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    break;
                case "TakeItem":
                    {
                        if (command.Parameter.Count == 2)
                        {
                            int count;
                            int.TryParse(command.Parameter[1].Value, out count);
                            Session.currentSession.Inventory.RemoveItem(command.Parameter[0].Value, count);
                        }
                        else if (command.Parameter.Count == 1)
                        {
                            Session.currentSession.Inventory.RemoveItem(command.Parameter[0].Value, 1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    break;
            }
        }

        public void Execute(List<ScriptEngineCommand> commands)
        {
            foreach (ScriptEngineCommand command in commands)
                Execute(command);
        }

        #region Singleton pattern
        private static ScriptEngine _instance;
        public static ScriptEngine Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScriptEngine();
                return _instance;
            }
        }
        #endregion
    }
}
