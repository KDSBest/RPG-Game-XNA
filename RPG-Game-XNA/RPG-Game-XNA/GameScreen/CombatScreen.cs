using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RPG_Game_XNA.Combat;
using RPGData;

namespace RPG_Game_XNA.GameScreen
{
    public class CombatScreen : GameStateScreen
    {
        protected List<Character> Entries;
        protected List<Character> Enemies;
        private int selected;
        public float Space;
        private EnemyGroup Group;
        private int CombatExp;
        private List<ItemDrop> Items;

        public CombatScreen()
        {
            Entries = Session.currentSession.Party;
            this.Group = CombatEngine.CreateRandomForMap(Session.currentSession.Map);
            Enemies = new List<Character>();
            CombatExp = 0;
            Items = new List<ItemDrop>();
            foreach (EnemyGroupEntry gEntry in Group.Group)
            {
                Character Char = new Character(gEntry.Name, (Weapon)ItemPool.Instance.GetItem(gEntry.WeaponName), (Armour)ItemPool.Instance.GetItem(gEntry.ArmourName));
                Char.AI = gEntry.AI;
                Char.SetLevel(gEntry.Level);
                Char.DisplayName = gEntry.DisplayName;
                Enemies.Add(Char);
                CombatExp += gEntry.Experience;
                Items.AddRange(gEntry.Items);
            }
        }

        public override bool Update(GameTime time)
        {
            bool SomeOneAlive = false;
            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].IsAlive)
                {
                    Entries[i].ATB = CombatEngine.CalcATB(Entries[i], time);
                    SomeOneAlive = true;
                }
                else
                    Entries[i].ATB = 0;
            }

            if (!SomeOneAlive)
            {
                GameStateManager.Instance.GameOver();
            }

            SomeOneAlive = false;
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].IsAlive)
                {
                    Enemies[i].ATB = CombatEngine.CalcATB(Enemies[i], time);
                    SomeOneAlive = true;
                }
                else
                    Enemies[i].ATB = 0;
            }

            if (!SomeOneAlive)
            {
                GameStateManager.Instance.AddScreen(new CombatRewardScreen(CombatExp, Items), true, false);
            }

            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].ATB == CombatEngine.MAXATB)
                {
                    GameStateManager.Instance.AddScreen(new CombatMenuScreen(Entries[i], Enemies, Entries), true, false);
                    return true;
                }
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].ATB == CombatEngine.MAXATB)
                {
                    ScriptEngine.ScriptEngine.Instance.SetVar("Attacker", Enemies[i]);
                    ScriptEngine.ScriptEngine.Instance.Execute(Enemies[i].AI);
                    Enemies[i].ATB = 0;
                    return true;
                }
            }
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Vector2 size = Globals.Instance.SpriteFont.MeasureString("Name\nHP\nMP\nATB");
            float MenuIncrementY = size.Y + Space;
            float MenuSize = MenuIncrementY * Entries.Count;
            Vector2 drawPosition = Vector2.Zero;

            Globals.Instance.SpriteBatch.Begin();
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, Globals.Instance.FullScreenRectangle, Color.Black);

            string text = "";
            Vector2 textSize;
            drawPosition.Y = 0;
            drawPosition.X = Globals.Instance.ScreenWidth - 300;
            for (int i = 0; i < Entries.Count; i++)
            {
                text = "Name: " + Entries[i].DisplayName + " (" + Entries[i].Level + ")\n";
                text += "HP: " + Entries[i].HP + "/" + Entries[i].MaxHP + "\n";
                text += "MP: " + Entries[i].MP + "/" + Entries[i].MaxMP + "\n";
                text += "ATB: " + Entries[i].ATB + "/" + CombatEngine.MAXATB;
                textSize = Globals.Instance.SpriteFont.MeasureString(text);
                Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.White);
                drawPosition.Y += MenuIncrementY;
            }
            drawPosition.Y = 0;
            drawPosition.X = 0;
            for (int i = 0; i < Enemies.Count; i++)
            {
                text = "Name: " + Enemies[i].DisplayName + " (" + Enemies[i].Level + ")\n";
                text += "HP: " + Enemies[i].HP + "/" + Enemies[i].MaxHP + "\n";
                text += "MP: " + "--/--" + "\n";
                text += "ATB: " + Enemies[i].ATB + "/" + CombatEngine.MAXATB;
                textSize = Globals.Instance.SpriteFont.MeasureString(text);
                Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.White);
                drawPosition.Y += MenuIncrementY;
            }

            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Entries.Count)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Entries.Count - 1;
            }

            if (input.IsMenuSelect())
            {

            }

            if (input.IsBack())
                GameStateManager.Instance.RemoveScreen(this);

            return true;
        }
    }
}
