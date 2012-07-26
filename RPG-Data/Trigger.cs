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
    
    public class Trigger
    {
        public Trigger()
        {
            this.Action = new List<ScriptEngineCommand>();
        }

        private Point mapPosition;

        public Point MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = value; }
        }

        [ContentSerializerIgnore]
        public Point MapMaxPosition;

        private Point size;

        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        private List<ScriptEngineCommand> action;

        public List<ScriptEngineCommand> Action
        {
            get { return action; }
            set { action = value; }
        }

        #region Content Type Reader

        public class TriggerReader : ContentTypeReader<Trigger>
        {
    
            
            
            protected override Trigger Read(ContentReader input,
                Trigger existingInstance)
            {
                Trigger desc = existingInstance;
                if (desc == null)
                {
                    desc = new Trigger();
                }

                desc.MapPosition = input.ReadObject<Point>();
                desc.Size = input.ReadObject<Point>();
                desc.MapMaxPosition = new Point(desc.MapPosition.X + desc.Size.X, desc.MapPosition.Y + desc.Size.Y);
                desc.Action.AddRange(input.ReadObject<List<ScriptEngineCommand>>());
                return desc;
            }
        }

        #endregion
    }
}
