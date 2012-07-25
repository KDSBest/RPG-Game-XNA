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
    
    public class ScriptEngineCommand
    {
        public ScriptEngineCommand()
        {
            Body = new List<ScriptEngineCommand>();
            Body2 = new List<ScriptEngineCommand>();
            Parameter = new List<ScriptEngineParams>();
        }

        public string Type;

        private List<ScriptEngineParams> parameter;
        [ContentSerializer(Optional = true)]
        public List<ScriptEngineParams> Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        #region Childs
        private List<ScriptEngineCommand> body;

        [ContentSerializer(Optional=true)]
        public List<ScriptEngineCommand> Body
        {
            get { return body; }
            set { body = value; }
        }

        private List<ScriptEngineCommand> body2;

        [ContentSerializer(Optional = true)]
        public List<ScriptEngineCommand> Body2
        {
            get { return body2; }
            set { body2 = value; }
        }
        #endregion

        #region Content Type Reader
        
        public class ScriptEngineCommandReader : ContentTypeReader<ScriptEngineCommand>
        {
            protected override ScriptEngineCommand Read(ContentReader input,
                ScriptEngineCommand existingInstance)
            {
                ScriptEngineCommand sc = existingInstance;
                if (sc == null)
                {
                    sc = new ScriptEngineCommand();
                }

                sc.Type = input.ReadString();
                sc.Parameter.AddRange(input.ReadObject<List<ScriptEngineParams>>());
                sc.Body.AddRange(input.ReadObject<List<ScriptEngineCommand>>());
                sc.Body2.AddRange(input.ReadObject<List<ScriptEngineCommand>>());

                return sc;
            }
        }


        #endregion
    }
}
