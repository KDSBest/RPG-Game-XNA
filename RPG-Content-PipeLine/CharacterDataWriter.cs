#region File Description
//-----------------------------------------------------------------------------
// MapWriter.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using RPGData;
#endregion

namespace RPGContentPipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    
    [ContentTypeWriter]
    public class CharacterDataWriter : RolePlayingGameWriter<CharacterData>
    {
        protected override void Write(ContentWriter output, CharacterData value)
        {
            output.Write(value.VitalityBase);
            output.Write(value.StrengthBase);
            output.Write(value.MagicBase);
            output.Write(value.SpiritBase);
            output.Write(value.LuckBase);
            output.Write(value.HPBase);
            output.Write(value.MPBase);
            output.Write(value.DexterityBase);
            output.Write(value.VitalityMax);
            output.Write(value.StrengthMax);
            output.Write(value.MagicMax);
            output.Write(value.SpiritMax);
            output.Write(value.LuckMax);
            output.Write(value.HPMax);
            output.Write(value.MPMax);
            output.Write(value.DexterityMax);
            output.Write(value.MapIdleAnimationInterval);
            output.WriteObject(value.IdleSprite);
            output.Write(value.MapWalkingAnimationInterval);
            output.WriteObject(value.WalkingSprite);
            output.Write(String.IsNullOrEmpty(value.CharacterImage) ? String.Empty : value.CharacterImage);
        }
    }
}
