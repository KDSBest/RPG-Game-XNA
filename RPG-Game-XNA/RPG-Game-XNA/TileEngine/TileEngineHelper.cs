using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGData;
using RPG_Game_XNA.GameScreen;
namespace RPG_Game_XNA.TileEngine
{
    /// <summary>
    /// Static class for a tileable map
    /// </summary>
    public class TileEngineHelper
    {
        public TileEngineHelper()
        {
            Viewport = Globals.Instance.Graphics.Viewport;
        }


        /// <summary>
        /// The map being used by the tile engine.
        /// </summary>
        private Map map = null;

        /// <summary>
        /// The map being used by the tile engine.
        /// </summary>
        public Map Map
        {
            get { return map; }
        }

        /// <summary>
        /// The position of the outside 0,0 corner of the map, in pixels.
        /// </summary>
        public Vector2 MapOriginPosition;


        /// <summary>
        /// Calculate the screen position of a given map location (in tiles).
        /// </summary>
        /// <param name="mapPosition">A map location, in tiles.</param>
        /// <returns>The current screen position of that location.</returns>
        public Vector2 GetScreenPosition(Point mapPosition)
        {
            return new Vector2(
                MapOriginPosition.X + mapPosition.X * map.TileSize.X,
                MapOriginPosition.Y + mapPosition.Y * map.TileSize.Y);
        }


        /// <summary>
        /// Set the map in use by the tile engine.
        /// </summary>
        /// <param name="map">The new map for the tile engine.</param>
        /// <param name="portal">The portal the party is entering on, if any.</param>
        public void SetMap(Map newMap, int portalEntry)
        {
            // check the parameter
            if (newMap == null)
            {
                throw new ArgumentNullException("newMap");
            }

            // assign the new map
            map = newMap;

            // reset the map origin, which will be recalculate on the first update
            MapOriginPosition = Vector2.Zero;

            // move the party to its initial position
            if (portalEntry == -1)
            {
                // no portal - use the spawn position
                partyLeaderPosition.TilePosition = map.SpawnMapPosition;
                partyLeaderPosition.TileOffset = Vector2.Zero;
                partyLeaderPosition.Direction = Direction.South;
            }
            else
            {
                // use the portal provided, which may include automatic movement
                partyLeaderPosition.TilePosition = map.Portals[portalEntry];
                partyLeaderPosition.TileOffset = Vector2.Zero;
//                partyLeaderPosition.Direction = map.Portals[portalEntry].Direction;
            }
        }

        /// <summary>
        /// The viewport that the tile engine is rendering within.
        /// </summary>
        private Viewport viewport;

        /// <summary>
        /// The viewport that the tile engine is rendering within.
        /// </summary>
        public Viewport Viewport
        {
            get { return viewport; }
            set
            {
                viewport = value;
                viewportCenter = new Vector2(
                    viewport.X + viewport.Width / 2f,
                    viewport.Y + viewport.Height / 2f);
            }
        }


        /// <summary>
        /// The center of the current viewport.
        /// </summary>
        private Vector2 viewportCenter;
        public Vector2 ViewportCenter
        {
            get { return viewportCenter; }
            set
            {
                viewportCenter = value;
            }
        }
        /// <summary>
        /// The speed of the party leader, in units per second.
        /// </summary>
        /// <remarks>
        /// The movementCollisionTolerance constant should be a multiple of this number.
        /// </remarks>
        private const float partyLeaderMovementSpeed = 3f;


        /// <summary>
        /// The current position of the party leader.
        /// </summary>
        private PlayerPosition partyLeaderPosition = new PlayerPosition();
        public PlayerPosition PartyLeaderPosition
        {
            get { return partyLeaderPosition; }
            set { partyLeaderPosition = value; }
        }

        /// <summary>
        /// Update the user-controlled movement of the party.
        /// </summary>
        /// <returns>The controlled movement for this update.</returns>
        public Vector2 UpdateUserMovement(bool up, bool down, bool left, bool right)
        {
            Vector2 desiredMovement = Vector2.Zero;

            // accumulate the desired direction from user input
            if (up)
            {
                if (CanPartyLeaderMoveUp())
                {
                    desiredMovement.Y -= partyLeaderMovementSpeed;
                }
            }
            if (down)
            {
                if (CanPartyLeaderMoveDown())
                {
                    desiredMovement.Y += partyLeaderMovementSpeed;
                }
            }
            if (left)
            {
                if (CanPartyLeaderMoveLeft())
                {
                    desiredMovement.X -= partyLeaderMovementSpeed;
                }
            }
            if (right)
            {
                if (CanPartyLeaderMoveRight())
                {
                    desiredMovement.X += partyLeaderMovementSpeed;
                }
            }

            // if there is no desired movement, then we can't determine a direction
            if (desiredMovement == Vector2.Zero)
            {
                return Vector2.Zero;
            }

            return desiredMovement;
        }

        /// <summary>
        /// The number of pixels that characters should be allowed to move into 
        /// blocking tiles.
        /// </summary>
        /// <remarks>
        /// The partyMovementSpeed constant should cleanly divide this number.
        /// </remarks>
        const int movementCollisionTolerance = 12;


        /// <summary>
        /// Returns true if the player can move up from their current position.
        /// </summary>
        private bool CanPartyLeaderMoveUp()
        {
            // if they're not within the tolerance of the next tile, then this is moot
            if (partyLeaderPosition.TileOffset.Y > -movementCollisionTolerance)
            {
                return true;
            }

            // if the player is at the outside left and right edges, 
            // then check the diagonal tiles
            if (partyLeaderPosition.TileOffset.X < -movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X - 1,
                    partyLeaderPosition.TilePosition.Y - 1)))
                {
                    return false;
                }
            }
            else if (partyLeaderPosition.TileOffset.X > movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X + 1,
                    partyLeaderPosition.TilePosition.Y - 1)))
                {
                    return false;
                }
            }

            // check the tile above the current one
            return !map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X,
                    partyLeaderPosition.TilePosition.Y - 1));
        }


        /// <summary>
        /// Returns true if the player can move down from their current position.
        /// </summary>
        private bool CanPartyLeaderMoveDown()
        {
            // if they're not within the tolerance of the next tile, then this is moot
            if (partyLeaderPosition.TileOffset.Y < movementCollisionTolerance)
            {
                return true;
            }

            // if the player is at the outside left and right edges, 
            // then check the diagonal tiles
            if (partyLeaderPosition.TileOffset.X < -movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X - 1,
                    partyLeaderPosition.TilePosition.Y + 1)))
                {
                    return false;
                }
            }
            else if (partyLeaderPosition.TileOffset.X > movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X + 1,
                    partyLeaderPosition.TilePosition.Y + 1)))
                {
                    return false;
                }
            }

            // check the tile below the current one
            return !map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X,
                    partyLeaderPosition.TilePosition.Y + 1));
        }


        /// <summary>
        /// Returns true if the player can move left from their current position.
        /// </summary>
        private bool CanPartyLeaderMoveLeft()
        {
            // if they're not within the tolerance of the next tile, then this is moot
            if (partyLeaderPosition.TileOffset.X > -movementCollisionTolerance)
            {
                return true;
            }

            // if the player is at the outside left and right edges, 
            // then check the diagonal tiles
            if (partyLeaderPosition.TileOffset.Y < -movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X - 1,
                    partyLeaderPosition.TilePosition.Y - 1)))
                {
                    return false;
                }
            }
            else if (partyLeaderPosition.TileOffset.Y > movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X - 1,
                    partyLeaderPosition.TilePosition.Y + 1)))
                {
                    return false;
                }
            }

            // check the tile to the left of the current one
            return !map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X - 1,
                    partyLeaderPosition.TilePosition.Y));
        }


        /// <summary>
        /// Returns true if the player can move right from their current position.
        /// </summary>
        private bool CanPartyLeaderMoveRight()
        {
            // if they're not within the tolerance of the next tile, then this is moot
            if (partyLeaderPosition.TileOffset.X < movementCollisionTolerance)
            {
                return true;
            }

            // if the player is at the outside left and right edges, 
            // then check the diagonal tiles
            if (partyLeaderPosition.TileOffset.Y < -movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X + 1,
                    partyLeaderPosition.TilePosition.Y - 1)))
                {
                    return false;
                }
            }
            else if (partyLeaderPosition.TileOffset.Y > movementCollisionTolerance)
            {
                if (map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X + 1,
                    partyLeaderPosition.TilePosition.Y + 1)))
                {
                    return false;
                }
            }

            // check the tile to the right of the current one
            return !map.IsBlocked(new Point(
                    partyLeaderPosition.TilePosition.X + 1,
                    partyLeaderPosition.TilePosition.Y));
        }


        /// <summary>
        /// Performs any actions associated with moving into a new tile.
        /// </summary>
        /// <returns>True if the character can move into the tile.</returns>
        public bool MoveIntoTile(Point mapPosition)
        {
            // if the tile is blocked, then this is simple
            if (map.IsBlocked(mapPosition))
            {
                return false;
            }

            // nothing stops the party from moving into the tile
            return true;
        }

        /// <summary>
        /// Draw the visible tiles in the given map layers.
        /// </summary>
        public void DrawLayers(bool drawBase,
            bool drawFringe, bool drawObjects)
        {
            Rectangle destinationRectangle =
                new Rectangle(0, 0, map.TileSize.X, map.TileSize.Y);
            Globals.Instance.SpriteBatch.Begin();
            for (int y = 0; y < map.MapDimensions.Y; y++)
            {
                for (int x = 0; x < map.MapDimensions.X; x++)
                {
                    destinationRectangle.X =
                        (int)MapOriginPosition.X + x * map.TileSize.X;
                    destinationRectangle.Y =
                        (int)MapOriginPosition.Y + y * map.TileSize.Y;

                    // If the tile is inside the screen
                    if (CheckVisibility(destinationRectangle))
                    {
                        Point mapPosition = new Point(x, y);
                        if (drawBase)
                        {
                            Rectangle sourceRectangle =
                                map.GetBaseLayerSourceRectangle(mapPosition);
                            if (sourceRectangle != Rectangle.Empty)
                            {
                                Globals.Instance.SpriteBatch.Draw(map.Texture, destinationRectangle,
                                    sourceRectangle, Color.White);
                            }
                        }
                        if (drawFringe)
                        {
                            Rectangle sourceRectangle =
                                map.GetFringeLayerSourceRectangle(mapPosition);
                            if (sourceRectangle != Rectangle.Empty)
                            {
                                Globals.Instance.SpriteBatch.Draw(map.Texture, destinationRectangle,
                                    sourceRectangle, Color.White);
                            }
                        }
                        if (drawObjects)
                        {
                            Rectangle sourceRectangle =
                                map.GetObjectLayerSourceRectangle(mapPosition);
                            if (sourceRectangle != Rectangle.Empty)
                            {
                                Globals.Instance.SpriteBatch.Draw(map.Texture, destinationRectangle,
                                    sourceRectangle, Color.White);
                            }
                        }
                    }
                }
            }
            Globals.Instance.SpriteBatch.End();
        }


        /// <summary>
        /// Returns true if the given rectangle is within the viewport.
        /// </summary>
        public bool CheckVisibility(Rectangle screenRectangle)
        {
            return ((screenRectangle.X > viewport.X - screenRectangle.Width) &&
                (screenRectangle.Y > viewport.Y - screenRectangle.Height) &&
                (screenRectangle.X < viewport.X + viewport.Width) &&
                (screenRectangle.Y < viewport.Y + viewport.Height));
        }

        public void TriggerEvent()
        {
            foreach(Trigger trigger in Map.Trigger)
            {
                if (trigger.MapPosition.X <= PartyLeaderPosition.TilePosition.X && trigger.MapPosition.Y <= PartyLeaderPosition.TilePosition.Y &&
                    trigger.MapMaxPosition.X >= PartyLeaderPosition.TilePosition.X && trigger.MapMaxPosition.Y >= PartyLeaderPosition.TilePosition.Y)
                {
                    GameStateManagement.GameStateManager.Instance.AddScreen(new PopUpScreen(trigger.Action.Split(';')), true, false);
                }
            }
        }
    }
}