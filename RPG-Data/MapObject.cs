using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using RPGData.Animation;
using Microsoft.Xna.Framework;

namespace RPGData
{
    public class MapObject
    {
        public AnimatingSprite Sprite;
        public Animation.Animation Animation;
        public Point MapPosition;

        #region Content Type Reader

        public class MapObjectReader : ContentTypeReader<MapObject>
        {
            protected override MapObject Read(ContentReader input,
                MapObject existingInstance)
            {
                MapObject desc = existingInstance;
                if (desc == null)
                {
                    desc = new MapObject();
                }

                desc.Sprite = input.ReadObject<AnimatingSprite>();
                if (desc.Sprite != null)
                {
                    desc.Sprite.SourceOffset =
                        new Vector2(
                        desc.Sprite.SourceOffset.X - 32,
                        desc.Sprite.SourceOffset.Y - 32);
                    desc.Animation = input.ReadObject<Animation.Animation>();
                    desc.Animation.Name = "Object";
                    desc.Sprite.AddAnimation(desc.Animation);
                }
                desc.MapPosition = input.ReadObject<Point>();
                return desc;
            }
        }

        #endregion
    }
}
