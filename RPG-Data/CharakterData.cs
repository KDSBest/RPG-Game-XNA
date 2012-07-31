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
using RPGData.Animation;
using Microsoft.Xna.Framework.Graphics;
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

        [ContentSerializerIgnore]
        public Texture2D CharacterPicture;
        [ContentSerializer(Optional = true)]
        public string CharacterImage;

        /// <summary>
        /// The animating sprite for the map view of this character.
        /// </summary>
        private AnimatingSprite idleSprite;

        /// <summary>
        /// The animating sprite for the map view of this character.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public AnimatingSprite IdleSprite
        {
            get { return idleSprite; }
            set { idleSprite = value; }
        }


        /// <summary>
        /// The animating sprite for the map view of this character as it walks.
        /// </summary>
        /// <remarks>
        /// If this object is null, then the animations are taken from MapSprite.
        /// </remarks>
        private AnimatingSprite walkingSprite;

        /// <summary>
        /// The animating sprite for the map view of this character as it walks.
        /// </summary>
        /// <remarks>
        /// If this object is null, then the animations are taken from MapSprite.
        /// </remarks>
        [ContentSerializer(Optional = true)]
        public AnimatingSprite WalkingSprite
        {
            get { return walkingSprite; }
            set { walkingSprite = value; }
        }

        /// <summary>
        /// The default walk-animation interval for the animating map sprite.
        /// </summary>
        private int mapWalkingAnimationInterval = 80;

        /// <summary>
        /// The default walk-animation interval for the animating map sprite.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public int MapWalkingAnimationInterval
        {
            get { return mapWalkingAnimationInterval; }
            set { mapWalkingAnimationInterval = value; }
        }

        /// <summary>
        /// The default idle-animation interval for the animating map sprite.
        /// </summary>
        private int mapIdleAnimationInterval = 200;

        /// <summary>
        /// The default idle-animation interval for the animating map sprite.
        /// </summary>
        [ContentSerializer(Optional=true)]
        public int MapIdleAnimationInterval
        {
            get { return mapIdleAnimationInterval; }
            set { mapIdleAnimationInterval = value; }
        }
        
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
                desc.MapIdleAnimationInterval = input.ReadInt32();
                desc.IdleSprite = input.ReadObject<AnimatingSprite>();
                if (desc.IdleSprite != null)
                {
                    desc.IdleSprite.SourceOffset =
                        new Vector2(
                        desc.IdleSprite.SourceOffset.X - 32,
                        desc.IdleSprite.SourceOffset.Y - 32);
                }

                desc.MapWalkingAnimationInterval = input.ReadInt32();
                desc.WalkingSprite = input.ReadObject<AnimatingSprite>();
                if (desc.WalkingSprite != null)
                {
                    desc.WalkingSprite.SourceOffset =
                        new Vector2(
                        desc.WalkingSprite.SourceOffset.X - 32,
                        desc.WalkingSprite.SourceOffset.Y - 32);
                }

                desc.CharacterImage = input.ReadString();
                if(desc.CharacterImage != null && desc.CharacterImage != "")
                    desc.CharacterPicture = input.ContentManager.Load<Texture2D>(desc.CharacterImage);
                return desc;
            }
        }

        #endregion
    }
}
