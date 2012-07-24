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
#endregion

namespace RPGData
{
    /// <summary>
    /// The description of where an instance of a world object is in the world.
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        private Point mapPosition;

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        public Point MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = value; }
        }

        [ContentSerializerIgnore]
        public Point MapMaxPosition;

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        private Point size;

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        private string action;

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        public string Action
        {
            get { return action; }
            set { action = value; }
        }


        #region Content Type Reader


        /// <summary>
        /// Read a MapEntry object from the content pipeline.
        /// </summary>
        public class TriggerReader : ContentTypeReader<Trigger>
        {
            /// <summary>
            /// Read a MapEntry object from the content pipeline.
            /// </summary>
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
                desc.Action = input.ReadString();
                return desc;
            }
        }


        #endregion
    }
}
