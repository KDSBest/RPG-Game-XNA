using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;
using RPG_Game_XNA.Combat;

namespace RPG_Game_XNA.ScriptEngine
{
    public class ScriptEngine
    {
        public Dictionary<string, object> Context;

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

        public void Reset()
        {
            Context.Clear();
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
                    break;
                case "TakeItem":
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
                    break;
                case "GiveMPSelected":
                    {
                        //Has ScriptEngine.ScriptEngine.Instance.SetVar("Consumeable", Item.Name);
                        Character cha = Session.currentSession.Party[(int)GetVar("PartySelect")];
                        cha.MP += int.Parse(command.Parameter[0].Value);
                        if (cha.MP < cha.MaxMP)
                            cha.MP = cha.MaxMP;
                    }
                    break;
                case "GiveHPSelected":
                    {
                        //Has ScriptEngine.ScriptEngine.Instance.SetVar("Consumeable", Item.Name);
                        Character cha = Session.currentSession.Party[(int)GetVar("PartySelect")];
                        cha.HP += int.Parse(command.Parameter[0].Value);
                        if (cha.HP > cha.MaxHP)
                            cha.HP = cha.MaxHP;
                    }
                    break;
                case "IfBoolVar":
                    {
                        object var = (object)GetVar(command.Parameter[0].Value);
                        if(var == null || ((string) var) != "true")
                            ScriptEngine.Instance.Execute(command.Body2);
                        else
                            ScriptEngine.Instance.Execute(command.Body);
                    }
                    break;
                case "SetVarBool":
                    SetVar(command.Parameter[0].Value, command.Parameter[1].Value);
                    break;
                case "UseSkillOnRandom":
                    {
                        int MaxRandom = 0;
                        foreach (Character c in Session.currentSession.Party)
                            if (c.HP > 0)
                                MaxRandom++;
                        int Random = Globals.Instance.Random.Next(MaxRandom);
                        int Choosen = 0;
                        for (int i = 0; i <= Random; Choosen++)
                        {
                            if (Session.currentSession.Party[i].HP > 0)
                                i++;
                        }
                        Character Attacker = (Character)GetVar("Attacker");
                        Character Victim = Session.currentSession.Party[Random];
                        CombatEngine.DoDamage(Attacker, Victim, command.Parameter[0].Value);
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
