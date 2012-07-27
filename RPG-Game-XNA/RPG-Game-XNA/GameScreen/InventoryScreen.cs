using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Menu;
using RPG_Game_XNA.AbstractGameScreen;
using RPG_Game_XNA.GameStateManagement;
using RPGData;
using Microsoft.Xna.Framework;
using RPG_Game_XNA.Menu.Items;

namespace RPG_Game_XNA.GameScreen
{
    public class InventoryScreen : MenuCategoryScreen
    {

        public InventoryScreen()
            : base()
        {
            AddCategory("Consumable");
            AddCategory("Weapon");
            AddCategory("Armour");
        }

        public override bool Update(GameTime time)
        {
            Dictionary<Consumable, int> Consumable;
            Dictionary<Weapon, int> Weapon;
            Dictionary<Armour, int> Armour;
            Session.currentSession.Inventory.GetItems(out Consumable, out Weapon, out Armour);

            Clear();
            foreach (Consumable c in Consumable.Keys)
                AddEntry("Consumable", new MenuEntryConsumable(c, Consumable[c]));
            foreach (Weapon w in Weapon.Keys)
                AddEntry("Weapon", new MenuEntryWeapon(w, Weapon[w]));
            foreach (Armour a in Armour.Keys)
                AddEntry("Armour", new MenuEntryArmour(a, Armour[a]));

            return true;
        }

        public override bool HandleInputs(InputState input)
        {
            base.HandleInputs(input);
            if (input.IsBack())
                GameStateManager.Instance.RemoveScreen(this);
            return true;
        }

    }
}
