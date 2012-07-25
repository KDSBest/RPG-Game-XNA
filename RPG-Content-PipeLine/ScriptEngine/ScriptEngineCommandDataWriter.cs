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
    public class ScriptEngineCommandDataWriter : RolePlayingGameWriter<ScriptEngineCommand>
    {
        protected override void Write(ContentWriter output, ScriptEngineCommand value)
        {
            output.Write(value.Type);
            output.WriteObject(value.Parameter);
            output.WriteObject(value.Body);
            output.WriteObject(value.Body2);
        }
    }
}
