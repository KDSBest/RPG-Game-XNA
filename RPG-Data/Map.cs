﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RPGData
{
    /// <summary>
    /// One section of the world, and all of the data in it.
    
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

        #region Map Layers


        #region Base Layer



        /// Spatial array for the ground tiles for this map.
        
        private int[] baseLayer;


        /// Spatial array for the ground tiles for this map.
        
        public int[] BaseLayer
        {
            get { return baseLayer; }
            set { baseLayer = value; }
        }



        /// Retrieves the base layer value for the given map position.
        
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
        


        /// Retrieves the source rectangle for the tile in the given position
        /// in the base layer.
        
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


        /// Spatial array for the fringe tiles for this map.
        
        private int[] fringeLayer;


        /// Spatial array for the fringe tiles for this map.
        
        public int[] FringeLayer
        {
            get { return fringeLayer; }
            set { fringeLayer = value; }
        }



        /// Retrieves the fringe layer value for the given map position.
        
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



        /// Retrieves the source rectangle for the tile in the given position
        /// in the fringe layer.
        
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

        /// Spatial array for the fringe tiles for this map.
        
        private int[] objectLayer;


        /// Spatial array for the fringe tiles for this map.
        
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



        /// Spatial array for the collision properties of this map.
        
        private int[] collisionLayer;


        /// Spatial array for the collision properties of this map.
        
        public int[] CollisionLayer
        {
            get { return collisionLayer; }
            set { collisionLayer = value; }
        }



        /// Retrieves the collision layer value for the given map position.
        
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



        /// Returns true if the given map position is blocked.
        
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



        /// Portals to other maps.
        
        private List<Point> portals = new List<Point>();


        /// Portals to other maps.
        
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
        private int randomFightPossibility;
        public int RandomFightPossibility
        {
            get
            {
                return randomFightPossibility;
            }
            set
            {
                randomFightPossibility = value;
            }
        }

        public List<EnemyGroup> Enemies = new List<EnemyGroup>();

        public List<MapObject> Objects = new List<MapObject>();

        #region Content Type Reader

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
                map.RandomFightPossibility = input.ReadInt32();
                map.Enemies.AddRange(input.ReadObject<List<EnemyGroup>>());
                map.Objects.AddRange(input.ReadObject<List<MapObject>>());
                return map;
            }
        }


        #endregion

    }
}
