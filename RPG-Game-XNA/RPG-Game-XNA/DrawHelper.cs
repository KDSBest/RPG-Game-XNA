using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RPGData;

namespace RPG_Game_XNA
{
    public class DrawHelper
    {
        private Texture2D UL, U, UR, L, BG, R, BL, B, BR;

        private DrawHelper()
        {
            UL = Globals.Instance.Content.Load<Texture2D>("Window\\UL");
            U = Globals.Instance.Content.Load<Texture2D>("Window\\U");
            UR = Globals.Instance.Content.Load<Texture2D>("Window\\UR");
            L = Globals.Instance.Content.Load<Texture2D>("Window\\L");
            BG = Globals.Instance.Content.Load<Texture2D>("Window\\BG");
            R = Globals.Instance.Content.Load<Texture2D>("Window\\R");
            BL = Globals.Instance.Content.Load<Texture2D>("Window\\BL");
            B = Globals.Instance.Content.Load<Texture2D>("Window\\B");
            BR = Globals.Instance.Content.Load<Texture2D>("Window\\BR");
        }

        public void DrawFullScreenColor(Color Color)
        {
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, Globals.Instance.FullScreenRectangle, Color);
        }

        public void DrawWindow(Point Position, Point Size, Color Color)
        {
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 5, Position.Y + 5, Size.X - 10, Size.Y - 10), Color * 0.5f);

            Globals.Instance.SpriteBatch.Draw(UL, new Rectangle(Position.X, Position.Y, UL.Width, UL.Height), Color);
            Globals.Instance.SpriteBatch.Draw(U, new Rectangle(Position.X + UL.Width, Position.Y, Size.X - UL.Width - UR.Width, U.Height), Color);
            Globals.Instance.SpriteBatch.Draw(UR, new Rectangle(Position.X + Size.X - UR.Width, Position.Y, UR.Width, UR.Height), Color);

            Globals.Instance.SpriteBatch.Draw(L, new Rectangle(Position.X, Position.Y + UL.Height, L.Width, Size.Y - UL.Height - BL.Height), Color);
            Globals.Instance.SpriteBatch.Draw(BG, new Rectangle(Position.X + L.Width, Position.Y + U.Height, Size.X - L.Width - R.Width, Size.Y - U.Height - B.Height), Color);
            Globals.Instance.SpriteBatch.Draw(R, new Rectangle(Position.X + Size.X - UR.Width, Position.Y + UR.Height, R.Width, Size.Y - UR.Height - BR.Height), Color);

            Globals.Instance.SpriteBatch.Draw(BL, new Rectangle(Position.X, Position.Y + Size.Y - BL.Height, BL.Width, BL.Height), Color);
            Globals.Instance.SpriteBatch.Draw(B, new Rectangle(Position.X + BL.Width, Position.Y + Size.Y - BR.Height, Size.X - BL.Width - BR.Width, B.Height), Color);
            Globals.Instance.SpriteBatch.Draw(BR, new Rectangle(Position.X + Size.X - UR.Width, Position.Y + Size.Y - BR.Height, BR.Width, BR.Height), Color);
        }

        public void DrawText(string Text, Vector2 Position, Color Color, bool Shadow, Color ShadowColor)
        {
            if (Shadow)
            {
                Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Text, Position + (Vector2.UnitY + Vector2.UnitX) * 2, ShadowColor);
            }
            Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Text, Position, Color);
        }

        public void DrawProgressBar(float Percent, Point Position, Point Size, Point Border, Color BorderColor, Color Background, Color Bar)
        {
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X, Position.Y, Size.X, Size.Y), BorderColor);
            int X = Position.X + Border.X;
            int Y = Position.Y + Border.Y;
            int Sx = Size.X - Border.X * 2;
            int Sy = Size.Y - Border.Y * 2;
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(X, Y, Sx, Sy), Background);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + Border.X, Position.Y + Border.Y, (int)((float) Sx * Percent), Sy), Bar);
        }

        public void DrawCharacterInfo(Point Position, Point Size, Color BackgroundColor, Character Info)
        {
            Vector2 Pos = new Vector2(Position.X + 15, Position.Y + 315);
            Point BarBorder = new Point(2, 2);
            Point BarSize = new Point(150, 10);
            DrawWindow(Position, Size, BackgroundColor);
            if (Info.CharInfo.CharacterPicture == null)
            {
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 13, Position.Y + 13, 400, 300), Color.Black);
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 15, Position.Y + 15, 396, 296), Color.White);
            }
            else
            {
                Globals.Instance.SpriteBatch.Draw(Info.CharInfo.CharacterPicture, new Rectangle(Position.X + 13, Position.Y + 13, 400, 300), Color.White);
            }
            DrawText(Info.DisplayName, Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText("Lv. " + Info.Level, Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            DrawText(Info.Armour.Name, Pos, Color.White, true, Color.Black);
            Pos.X -= 200;
            DrawText("Armour: ", Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            DrawText("Weapon: ", Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText(Info.Weapon.Name, Pos, Color.White, true, Color.Black);
            Pos.X -= 200;
            Pos.Y += 25;
            DrawText("HP: ", Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText(Info.HP + " / " + Info.MaxHP, Pos, Color.White, true, Color.Black);
            Pos.Y += 22;
            DrawProgressBar((float)Info.HP / (float)Info.MaxHP, new Point((int)Pos.X, (int)Pos.Y), BarSize, BarBorder, Color.Black, Color.LightGray, Color.Red);
            Pos.Y += 10;
            Pos.X -= 200;
            DrawText("MP: ", Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText(Info.MP + " / " + Info.MaxMP, Pos, Color.White, true, Color.Black);
            Pos.Y += 22;
            DrawProgressBar((float)Info.MP / (float)Info.MaxMP, new Point((int)Pos.X, (int)Pos.Y), BarSize, BarBorder, Color.Black, Color.LightGray, Color.Blue);
            Pos.Y += 10;
            Pos.X -= 200;
            DrawText("Exp: ", Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            int ExpNextLevel = Info.ExperienceForNextLevel();
            int ExpCurrentLevel = Info.ExperienceForCurrentLevel();
            float Exp = Info.Experience - ExpCurrentLevel;
            float ExpCurrentToNextLevel = ExpNextLevel - ExpCurrentLevel;
            DrawText(Info.Experience + " / " + ExpNextLevel, Pos, Color.White, true, Color.Black);
            Pos.Y += 22;
            DrawProgressBar(Exp / ExpCurrentToNextLevel, new Point((int)Pos.X, (int)Pos.Y), BarSize, BarBorder, Color.Black, Color.LightGray, Color.Orange);
            Pos.Y += 10;
            DrawText("MAtk: " + Info.MagicAttack, Pos, Color.White, true, Color.Black);
            Pos.X -= 200;
            DrawText("Atk: " + Info.Attack, Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            DrawText("Def: " + Info.Defense, Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText("MDef: " + Info.MagicDefense, Pos, Color.White, true, Color.Black);
        }

        public void DrawSelectCharacterInfo(Point Position, Point Size, Color BackgroundColor, Character Info)
        {
            Vector2 Pos = new Vector2(Position.X + 215, Position.Y + 15);
            Point BarBorder = new Point(2, 2);
            Point BarSize = new Point(150, 10);
            DrawWindow(Position, Size, BackgroundColor);
            if (Info.CharInfo.CharacterPicture == null)
            {
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 13, Position.Y + 13, 200, 150), Color.Black);
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 15, Position.Y + 15, 196, 146), Color.White);
            }
            else
            {
                Globals.Instance.SpriteBatch.Draw(Info.CharInfo.CharacterPicture, new Rectangle(Position.X + 13, Position.Y + 13, 200, 150), Color.White);
            }
            DrawText(Info.DisplayName, Pos, Color.White, true, Color.Black);
            Pos.X += 100;
            DrawText("Lv. " + Info.Level, Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            DrawText(Info.Armour.Name, Pos, Color.White, true, Color.Black);
            Pos.X -= 100;
            Pos.Y += 25;
            DrawText("HP: ", Pos, Color.White, true, Color.Black);
            Pos.X += 100;
            DrawText(Info.HP + " / " + Info.MaxHP, Pos, Color.White, true, Color.Black);
            Pos.Y += 22;
            DrawProgressBar((float)Info.HP / (float)Info.MaxHP, new Point((int)Pos.X, (int)Pos.Y), BarSize, BarBorder, Color.Black, Color.LightGray, Color.Red);
            Pos.Y += 10;
            Pos.X -= 100;
            DrawText("MP: ", Pos, Color.White, true, Color.Black);
            Pos.X += 100;
            DrawText(Info.MP + " / " + Info.MaxMP, Pos, Color.White, true, Color.Black);
            Pos.Y += 22;
            DrawProgressBar((float)Info.MP / (float)Info.MaxMP, new Point((int)Pos.X, (int)Pos.Y), BarSize, BarBorder, Color.Black, Color.LightGray, Color.Blue);
        }

        public void DrawSelectWeaponCharacterInfo(Point Position, Point Size, Color BackgroundColor, Character Info, Weapon Weapon)
        {
            int MAtkChange = Info.MagicAttack;
            int AtkChange = Info.Attack;
            Weapon backup = Info.Weapon;
            Info.Weapon = Weapon;
            MAtkChange = Info.MagicAttack - MAtkChange;
            AtkChange = Info.Attack - AtkChange;
            Info.Weapon = backup;

            Vector2 Pos = new Vector2(Position.X + 215, Position.Y + 15);
            Point BarBorder = new Point(2, 2);
            Point BarSize = new Point(150, 10);
            DrawWindow(Position, Size, BackgroundColor);
            if (Info.CharInfo.CharacterPicture == null)
            {
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 13, Position.Y + 13, 200, 150), Color.Black);
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 15, Position.Y + 15, 196, 146), Color.White);
            }
            else
            {
                Globals.Instance.SpriteBatch.Draw(Info.CharInfo.CharacterPicture, new Rectangle(Position.X + 13, Position.Y + 13, 200, 150), Color.White);
            }
            DrawText(Info.DisplayName, Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText("Lv. " + Info.Level, Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            string MAtk = "MAtk: " + Info.MagicAttack + " ";
            int w = (int) Globals.Instance.SpriteFont.MeasureString(MAtk).X;
            DrawText(MAtk, Pos, Color.White, true, Color.Black);
            Pos.X += w;
            if(MAtkChange > 0)
                DrawText("(+" + MAtkChange + ")", Pos, Color.Green, true, Color.Black);
            else if(MAtkChange < 0)
                DrawText("(" + MAtkChange + ")", Pos, Color.Red, true, Color.Black);
            Pos.X -= w;
            Pos.X -= 200;
            string Atk = "Atk: " + Info.Attack + " ";
            w = (int)Globals.Instance.SpriteFont.MeasureString(Atk).X;
            DrawText(Atk, Pos, Color.White, true, Color.Black);
            Pos.X += w;
            if (AtkChange > 0)
                DrawText("(+" + AtkChange + ")", Pos, Color.Green, true, Color.Black);
            else if (AtkChange < 0)
                DrawText("(" + AtkChange + ")", Pos, Color.Red, true, Color.Black);
            Pos.X -= w;
            Pos.Y += 25;
            DrawText("Def: " + Info.Defense, Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText("MDef: " + Info.MagicDefense, Pos, Color.White, true, Color.Black);
        }

        public void DrawSelectArmourCharacterInfo(Point Position, Point Size, Color BackgroundColor, Character Info, Armour Armour)
        {
            int MDefChange = Info.MagicDefense;
            int DefChange = Info.Defense;
            Armour backup = Info.Armour;
            Info.Armour = Armour;
            MDefChange = Info.MagicDefense - MDefChange;
            DefChange = Info.Defense - DefChange;
            Info.Armour = backup;

            Vector2 Pos = new Vector2(Position.X + 215, Position.Y + 15);
            Point BarBorder = new Point(2, 2);
            Point BarSize = new Point(150, 10);
            DrawWindow(Position, Size, BackgroundColor);
            if (Info.CharInfo.CharacterPicture == null)
            {
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 13, Position.Y + 13, 200, 150), Color.Black);
                Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, new Rectangle(Position.X + 15, Position.Y + 15, 196, 146), Color.White);
            }
            else
            {
                Globals.Instance.SpriteBatch.Draw(Info.CharInfo.CharacterPicture, new Rectangle(Position.X + 13, Position.Y + 13, 200, 150), Color.White);
            }
            DrawText(Info.DisplayName, Pos, Color.White, true, Color.Black);
            Pos.X += 200;
            DrawText("Lv. " + Info.Level, Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            DrawText("MAtk: " + Info.MagicAttack, Pos, Color.White, true, Color.Black);
            Pos.X -= 200;
            DrawText("Atk: " + Info.Attack, Pos, Color.White, true, Color.Black);
            Pos.Y += 25;
            string Def = "Def: " + Info.Defense + " ";
            int w = (int)Globals.Instance.SpriteFont.MeasureString(Def).X;
            DrawText(Def, Pos, Color.White, true, Color.Black);
            Pos.X += w;
            if (DefChange > 0)
                DrawText("(+" + DefChange + ")", Pos, Color.Green, true, Color.Black);
            else if (DefChange < 0)
                DrawText("(" + DefChange + ")", Pos, Color.Red, true, Color.Black);
            Pos.X -= w;
            Pos.X += 200;
            string MDef = "MDef: " + Info.MagicDefense + " ";
            w = (int)Globals.Instance.SpriteFont.MeasureString(MDef).X;
            DrawText(MDef, Pos, Color.White, true, Color.Black);
            Pos.X += w;
            if (MDefChange > 0)
                DrawText("(+" + MDefChange + ")", Pos, Color.Green, true, Color.Black);
            else if (MDefChange < 0)
                DrawText("(" + MDefChange + ")", Pos, Color.Red, true, Color.Black);
            Pos.X -= w;
        }

        #region Singleton pattern
        private static DrawHelper _instance;
        public static DrawHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DrawHelper();
                return _instance;
            }
        }
        #endregion


    }
}
