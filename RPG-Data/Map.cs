using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    /// <summary>
    /// One section of the world, and all of the data in it.
    /// </summary>
    public class Map
    {
        public string Name;
        public Point MapDimensions;
        public Point TileSize;
        [ContentSerializerIgnore]
        public int TilesPerRow;
        public Point SpawnMapPosition;
        [ContentSerializerIgnore]
        public Texture2D Texture;

/*        public Map(string Name, Point MapDimensions, Point TileSize, Point SpawnMapPosition, Texture2D Texture, int[] BaseLayer, int[] FringeLayer, int[] CollisionLayer, List<Portal> Portals)
        {
                this.Name = Name;
                this.MapDimensions = MapDimensions;
                this.TileSize = TileSize;
                this.SpawnMapPosition = SpawnMapPosition;

                this.Texture = Texture;
                this.TilesPerRow = this.Texture.Width / this.TileSize.X;

                this.BaseLayer = BaseLayer;
                this.FringeLayer = FringeLayer;
                this.CollisionLayer = CollisionLayer;
                this.Portals = Portals;
        }*/

        #region Map Layers


        #region Base Layer


        /// <summary>
        /// Spatial array for the ground tiles for this map.
        /// </summary>
        private int[] baseLayer;

        /// <summary>
        /// Spatial array for the ground tiles for this map.
        /// </summary>
        public int[] BaseLayer
        {
            get { return baseLayer; }
            set { baseLayer = value; }
        }


        /// <summary>
        /// Retrieves the base layer value for the given map position.
        /// </summary>
        public int GetBaseLayerValue(Point mapPosition)
        {
            // check the parameter
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                throw new ArgumentOutOfRangeException("mapPosition");
            }

            return baseLayer[mapPosition.Y * MapDimensions.X + mapPosition.X];
        }
        

        /// <summary>
        /// Retrieves the source rectangle for the tile in the given position
        /// in the base layer.
        /// </summary>
        /// <remarks>This method allows out-of-bound (blocked) positions.</remarks>
        public Rectangle GetBaseLayerSourceRectangle(Point mapPosition)
        {
            // check the parameter, but out-of-bounds is nonfatal
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                return Rectangle.Empty;
            }

            int baseLayerValue = GetBaseLayerValue(mapPosition);
            if (baseLayerValue < 0)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                (baseLayerValue % TilesPerRow) * TileSize.X,
                (baseLayerValue / TilesPerRow) * TileSize.Y,
                TileSize.X, TileSize.Y);
        }


        #endregion


        #region Fringe Layer

        /// <summary>
        /// Spatial array for the fringe tiles for this map.
        /// </summary>
        private int[] fringeLayer;

        /// <summary>
        /// Spatial array for the fringe tiles for this map.
        /// </summary>
        public int[] FringeLayer
        {
            get { return fringeLayer; }
            set { fringeLayer = value; }
        }


        /// <summary>
        /// Retrieves the fringe layer value for the given map position.
        /// </summary>
        public int GetFringeLayerValue(Point mapPosition)
        {
            // check the parameter
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                throw new ArgumentOutOfRangeException("mapPosition");
            }

            return fringeLayer[mapPosition.Y * MapDimensions.X + mapPosition.X];
        }


        /// <summary>
        /// Retrieves the source rectangle for the tile in the given position
        /// in the fringe layer.
        /// </summary>
        /// <remarks>This method allows out-of-bound (blocked) positions.</remarks>
        public Rectangle GetFringeLayerSourceRectangle(Point mapPosition)
        {
            // check the parameter, but out-of-bounds is nonfatal
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                return Rectangle.Empty;
            }

            int fringeLayerValue = GetFringeLayerValue(mapPosition);
            if (fringeLayerValue < 0)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                (fringeLayerValue % TilesPerRow) * TileSize.X,
                (fringeLayerValue / TilesPerRow) * TileSize.Y,
                TileSize.X, TileSize.Y);
        }

        #endregion

        #region Object Layer
        /// <summary>
        /// Spatial array for the fringe tiles for this map.
        /// </summary>
        private int[] objectLayer;

        /// <summary>
        /// Spatial array for the fringe tiles for this map.
        /// </summary>
        public int[] ObjectLayer
        {
            get { return objectLayer; }
            set { objectLayer = value; }
        }

        public int GetObjectLayerValue(Point mapPosition)
        {
            // check the parameter
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                throw new ArgumentOutOfRangeException("mapPosition");
            }

            return objectLayer[mapPosition.Y * MapDimensions.X + mapPosition.X];
        }

        public Rectangle GetObjectLayerSourceRectangle(Point mapPosition)
        {
            // check the parameter, but out-of-bounds is nonfatal
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                return Rectangle.Empty;
            }

            int objectLayerValue = GetObjectLayerValue(mapPosition);
            if (objectLayerValue < 0)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                (objectLayerValue % TilesPerRow) * TileSize.X,
                (objectLayerValue / TilesPerRow) * TileSize.Y,
                TileSize.X, TileSize.Y);
        }

        #endregion


        #region Collision Layer


        /// <summary>
        /// Spatial array for the collision properties of this map.
        /// </summary>
        private int[] collisionLayer;

        /// <summary>
        /// Spatial array for the collision properties of this map.
        /// </summary>
        public int[] CollisionLayer
        {
            get { return collisionLayer; }
            set { collisionLayer = value; }
        }


        /// <summary>
        /// Retrieves the collision layer value for the given map position.
        /// </summary>
        public int GetCollisionLayerValue(Point mapPosition)
        {
            // check the parameter
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                throw new ArgumentOutOfRangeException("mapPosition");
            }

            return collisionLayer[mapPosition.Y * MapDimensions.X + mapPosition.X];
        }


        /// <summary>
        /// Returns true if the given map position is blocked.
        /// </summary>
        /// <remarks>This method allows out-of-bound (blocked) positions.</remarks>
        public bool IsBlocked(Point mapPosition)
        {
            // check the parameter, but out-of-bounds is nonfatal
            if ((mapPosition.X < 0) || (mapPosition.X >= MapDimensions.X) ||
                (mapPosition.Y < 0) || (mapPosition.Y >= MapDimensions.Y))
            {
                return true;
            }

            return (GetCollisionLayerValue(mapPosition) != 0);
        }


        #endregion


        #endregion

        #region Portals


        /// <summary>
        /// Portals to other maps.
        /// </summary>
        private List<Point> portals = new List<Point>();

        /// <summary>
        /// Portals to other maps.
        /// </summary>
        public List<Point> Portals
        {
            get { return portals; }
            set { portals = value; }
        }


        #endregion
        [ContentSerializerIgnore]
        public string AssetName;
        private string textureName;
        public string TextureName
        {
            get
            {
                return textureName;
            }
            set
            {
                textureName = value;
            }
        }

        public List<Trigger> Trigger = new List<Trigger>();

        #region Content Type Reader


        /// <summary>
        /// Read a Map object from the content pipeline.
        /// </summary>
        public class MapReader : ContentTypeReader<Map>
        {
            protected override Map Read(ContentReader input, Map existingInstance)
            {
                Map map = existingInstance;
                if (map == null)
                {
                    map = new Map();
                }

                map.AssetName = input.AssetName;

                map.Name = input.ReadString();
                map.MapDimensions = input.ReadObject<Point>();
                map.TileSize = input.ReadObject<Point>();
                map.SpawnMapPosition = input.ReadObject<Point>();

                map.TextureName = input.ReadString();
                map.Texture = input.ContentManager.Load<Texture2D>(map.TextureName);
                map.TilesPerRow = map.Texture.Width / map.TileSize.X;

                map.BaseLayer = input.ReadObject<int[]>();
                map.FringeLayer = input.ReadObject<int[]>();
                map.ObjectLayer = input.ReadObject<int[]>();
                map.CollisionLayer = input.ReadObject<int[]>();
                map.Portals.AddRange(input.ReadObject<List<Point>>());
                map.Trigger.AddRange(input.ReadObject<List<Trigger>>());
                return map;
            }
        }


        #endregion

    }
}
