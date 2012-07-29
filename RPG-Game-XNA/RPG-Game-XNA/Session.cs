using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;
using System.IO;
using RPG_Game_XNA.TileEngine;
using RPG_Game_XNA.GameScreen;
using RPG_Game_XNA.GameStateManagement;

namespace RPG_Game_XNA
{
    public class Session
    {
        public static Session currentSession;
        public TileEngineHelper TileEngine;
        public List<Character> Party;
        public Inventory Inventory;
        public Map Map;
        public SkillPool SkillPool;

        public Session()
        {
            Inventory = new Inventory();
            TileEngine = new TileEngineHelper();
            Map = Globals.Instance.Content.Load<Map>("Maps\\StartLevel");
            SkillPool = Globals.Instance.Content.Load<SkillPool>("SkillPool");
            ScriptEngine.ScriptEngine.Instance.Reset();
            Party = new List<Character>();
            Party.Add(new Character("Natsu", 0, (Weapon)ItemPool.Instance.GetItem("Bare Hands"), (Armour)ItemPool.Instance.GetItem("T-Shirt")));
            Party[0].Skills.Add(SkillPool.GetSkill("AoE Attack"));
            Party[0].Skills.Add(SkillPool.GetSkill("Fire"));
            Party[0].Skills.Add(SkillPool.GetSkill("Vita"));
        }

        public bool LoadGame(string FileName)
        {
            if (Globals.Instance.StorageContainer.FileExists(FileName))
            {
                BinaryReader stream = new BinaryReader(Globals.Instance.StorageContainer.OpenFile(FileName, FileMode.Open));
                Session session = new Session();
                Session.currentSession = session;
                //Inventory
                int Count = stream.ReadInt32();
                for (int i = 0; i < Count; i++)
                {
                    session.Inventory.AddItem(stream.ReadString(), stream.ReadInt32());
                }

                //Map
                session.Map = Globals.Instance.Content.Load<Map>(stream.ReadString());
                GameStateManager.Instance.AddScreen(new TileEngineScreen(), true, false);


                session.TileEngine.PartyLeaderPosition.TilePosition.X = stream.ReadInt32();
                session.TileEngine.PartyLeaderPosition.TilePosition.Y = stream.ReadInt32();
                session.TileEngine.PartyLeaderPosition.TileOffset.X = stream.ReadSingle();
                session.TileEngine.PartyLeaderPosition.TileOffset.Y = stream.ReadSingle();
                //Script Engine
                Count = stream.ReadInt32();
                ScriptEngine.ScriptEngine.Instance.Reset();
                for (int i = 0; i < Count; i++)
                {
                    ScriptEngine.ScriptEngine.Instance.Context.Add(stream.ReadString(), stream.ReadString());
                }

                //Party
                Count = stream.ReadInt32();
                session.Party.Clear();
                for (int i = 0; i < Count; i++)
                {
                    session.Party.Add(new Character(stream.ReadString(), stream.ReadInt32(), (Weapon)ItemPool.Instance.GetItem(stream.ReadString()), (Armour)ItemPool.Instance.GetItem(stream.ReadString())));
                    session.Party[i].HP = stream.ReadInt32();
                    session.Party[i].MP = stream.ReadInt32();
                    int SkillCount = stream.ReadInt32();
                    for(int ii = 0; ii < SkillCount; ii++)
                        session.Party[i].Skills.Add(SkillPool.GetSkill(stream.ReadString()));
                }
                stream.Close();
                Globals.Instance.CloseContainer();
                Globals.Instance.OpenContainer();
                return true;
            }
            return false;
        }

        public bool SaveGame(string FileName)
        {
            if (Globals.Instance.StorageContainer.FileExists(FileName))
                Globals.Instance.StorageContainer.DeleteFile(FileName);
            BinaryWriter stream = new BinaryWriter(Globals.Instance.StorageContainer.CreateFile(FileName));

            //Inventory
            Dictionary<Consumable, int> Consumable;
            Dictionary<Weapon, int> Weapon;
            Dictionary<Armour, int> Armour;
            Inventory.GetItems(out Consumable, out Weapon, out Armour);
            int Count = Consumable.Count + Weapon.Count + Armour.Count;
            stream.Write(Count);
            foreach (Consumable c in Consumable.Keys)
            {
                stream.Write(c.Name);
                stream.Write(Consumable[c]);
            }
            foreach (Weapon w in Weapon.Keys)
            {
                stream.Write(w.Name);
                stream.Write(Weapon[w]);
            }
            foreach (Armour a in Armour.Keys)
            {
                stream.Write(a.Name);
                stream.Write(Armour[a]);
            }

            //Map
            stream.Write(Map.AssetName);
            stream.Write(TileEngine.PartyLeaderPosition.TilePosition.X);
            stream.Write(TileEngine.PartyLeaderPosition.TilePosition.Y);
            stream.Write(TileEngine.PartyLeaderPosition.TileOffset.X);
            stream.Write(TileEngine.PartyLeaderPosition.TileOffset.Y);

            //Script Engine
            stream.Write(ScriptEngine.ScriptEngine.Instance.Context.Count);
            foreach (string VarName in ScriptEngine.ScriptEngine.Instance.Context.Keys)
            {
                stream.Write(VarName);
                stream.Write(ScriptEngine.ScriptEngine.Instance.Context[VarName].ToString());
            }

            //Party
            stream.Write(Party.Count);
            for (int i = 0; i < Party.Count; i++)
            {
                stream.Write(Party[i].Name);
                stream.Write(Party[i].Experience);
                stream.Write(Party[i].Weapon.Name);
                stream.Write(Party[i].Armour.Name);
                stream.Write(Party[i].HP);
                stream.Write(Party[i].MP);
                stream.Write(Party[i].Skills.Count);
                foreach (Skill s in Party[i].Skills)
                    stream.Write(s.Name);
            }
            stream.Close();
            Globals.Instance.CloseContainer();
            Globals.Instance.OpenContainer();
            return true;
        }
    }
}
