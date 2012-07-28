#region File Description
//-----------------------------------------------------------------------------
// MapEntry.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
#endregion

namespace RPGData
{
    /// <summary>
    /// The description of where an instance of a world object is in the world.

    public class CharacterData
    {
        public CharacterData()
        {
        }

        public int VitalityBase;
        public int StrengthBase;
        public int MagicBase;
        public int SpiritBase;
        public int LuckBase;
        public int HPBase;
        public int MPBase;
        public int DexterityBase;
        public int VitalityMax;
        public int StrengthMax;
        public int MagicMax;
        public int SpiritMax;
        public int LuckMax;
        public int HPMax;
        public int MPMax;
        public int DexterityMax;

        #region Content Type Reader

        public class CharacterDataReader : ContentTypeReader<CharacterData>
        {
            protected override CharacterData Read(ContentReader input,
                CharacterData existingInstance)
            {
                CharacterData desc = existingInstance;
                if (desc == null)
                {
                    desc = new CharacterData();
                }

                desc.VitalityBase = input.ReadInt32();
                desc.StrengthBase = input.ReadInt32();
                desc.MagicBase = input.ReadInt32();
                desc.SpiritBase = input.ReadInt32();
                desc.LuckBase = input.ReadInt32();
                desc.HPBase = input.ReadInt32();
                desc.MPBase = input.ReadInt32();
                desc.DexterityBase = input.ReadInt32();
                desc.VitalityMax = input.ReadInt32();
                desc.StrengthMax = input.ReadInt32();
                desc.MagicMax = input.ReadInt32();
                desc.SpiritMax = input.ReadInt32();
                desc.LuckMax = input.ReadInt32();
                desc.HPMax = input.ReadInt32();
                desc.MPMax = input.ReadInt32();
                desc.DexterityMax = input.ReadInt32();


                return desc;
            }
        }

        #endregion
    }
}
