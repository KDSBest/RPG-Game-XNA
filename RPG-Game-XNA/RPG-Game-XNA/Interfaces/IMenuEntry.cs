using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Game_XNA.Interfaces
{
    public interface IMenuEntry
    {
        string GetText();
        void Select();
    }
}
