﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;
using RPG_Game_XNA.TileEngine;
using RPGData;
namespace RPG_Game_XNA.Menu.Items
{
    public class MenuEntryWeapon : MenuEntryItem
    {
        public MenuEntryWeapon(Weapon Item, int Count)
            : base((Item)Item, Count)
        {
        }

        public override void Select()
        {
            GameStateManager.Instance.AddScreen(new WeaponPartySelectScreen(((Weapon)Item)), true, false);
        }
    }
}
