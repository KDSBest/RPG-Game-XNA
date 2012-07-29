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
    public class MapWriter : RolePlayingGameWriter<Map>
    {
        protected override void Write(ContentWriter output, Map value)
        {
            // validate the map first
            if ((value.MapDimensions.X <= 0) ||
                (value.MapDimensions.Y <= 0))
            {
                throw new InvalidContentException("Invalid map dimensions.");
            }
            int totalTiles = value.MapDimensions.X * value.MapDimensions.Y;
            if (value.BaseLayer.Length != totalTiles)
            {
                throw new InvalidContentException("Base layer was " +
                    value.BaseLayer.Length.ToString() +
                    " tiles, but the dimensions specify " +
                    totalTiles.ToString() + ".");
            }
            if (value.FringeLayer.Length != totalTiles)
            {
                throw new InvalidContentException("Fringe layer was " +
                    value.FringeLayer.Length.ToString() +
                    " tiles, but the dimensions specify " +
                    totalTiles.ToString() + ".");
            }

            if (value.CollisionLayer.Length != totalTiles)
            {
                throw new InvalidContentException("Collision layer was " +
                    value.CollisionLayer.Length.ToString() +
                    " tiles, but the dimensions specify " +
                    totalTiles.ToString() + ".");
            }

            output.Write(value.Name);
            output.WriteObject(value.MapDimensions);
            output.WriteObject(value.TileSize);
            output.WriteObject(value.SpawnMapPosition);
            output.Write(value.TextureName);
            output.WriteObject(value.BaseLayer);
            output.WriteObject(value.FringeLayer);
            output.WriteObject(value.ObjectLayer);
            output.WriteObject(value.CollisionLayer);
            output.WriteObject(value.Portals);
            output.WriteObject(value.Trigger);
            output.Write(value.RandomFightPossibility);
            output.WriteObject(value.Enemies);
        }
    }
}
