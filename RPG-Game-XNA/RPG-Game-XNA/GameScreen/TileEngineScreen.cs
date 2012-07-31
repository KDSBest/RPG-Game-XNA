using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using RPG_Game_XNA.TileEngine;
using RPGData;
using Microsoft.Xna.Framework.Graphics;
namespace RPG_Game_XNA.GameScreen
{
    public class TileEngineScreen : GameStateScreen
    {
        public TileEngineHelper TileEngine;
        private Vector2 userMovement;
        private Texture2D PlayerDummy;
        private Random random;

        public TileEngineScreen()
            : base()
        {
            userMovement = Vector2.Zero;
            TileEngine = Session.currentSession.TileEngine;
            TileEngine.SetMap(Session.currentSession.Map, -1);
            PlayerDummy = Globals.Instance.Content.Load<Texture2D>("Player");
            random = new Random();
        }

        public override void Draw(GameTime time)
        {
            TileEngine.DrawLayers(true, true, false);

            float elapsedSeconds = (float)time.ElapsedGameTime.TotalSeconds;

            Globals.Instance.SpriteBatch.Begin();
            {

                Character player = Session.currentSession.Party[0];
                Vector2 position = TileEngine.PartyLeaderPosition.GetScreenPosition(TileEngine);
                player.Direction = TileEngine.PartyLeaderPosition.Direction;
                player.ResetAnimation(TileEngine.PartyLeaderPosition.IsMoving);
                switch (player.AnimationState)
                {
                    case 0:
                        if (player.CharInfo.IdleSprite != null)
                        {
                            player.CharInfo.IdleSprite.UpdateAnimation(elapsedSeconds);
                            player.CharInfo.IdleSprite.Draw(Globals.Instance.SpriteBatch, position,
                                1f - position.Y / (float)TileEngine.Viewport.Height);
                        }
                        break;

                    case 1:
                        if (player.CharInfo.WalkingSprite != null)
                        {
                            player.CharInfo.WalkingSprite.UpdateAnimation(elapsedSeconds);
                            player.CharInfo.WalkingSprite.Draw(Globals.Instance.SpriteBatch, position,
                                1f - position.Y / (float)TileEngine.Viewport.Height);
                        }
                        else if (player.CharInfo.IdleSprite != null)
                        {
                            player.CharInfo.IdleSprite.UpdateAnimation(elapsedSeconds);
                            player.CharInfo.IdleSprite.Draw(Globals.Instance.SpriteBatch, position,
                                1f - position.Y / (float)TileEngine.Viewport.Height);
                        }
                        break;
                }
            }

            foreach (MapObject mapObj in TileEngine.Map.Objects)
            {
                Vector2 position = TileEngine.GetScreenPosition(mapObj.MapPosition);
                mapObj.Sprite.PlayAnimation(0);
                mapObj.Sprite.UpdateAnimation(elapsedSeconds);
                mapObj.Sprite.Draw(Globals.Instance.SpriteBatch, position,
                    1f - position.Y / (float)TileEngine.Viewport.Height);
            }
            Globals.Instance.SpriteBatch.End();

            TileEngine.DrawLayers(false, false, true);
        }

        public override bool HandleInputs(InputState input)
        {
            userMovement = TileEngine.UpdateUserMovement(input.IsUp(), input.IsDown(), input.IsLeft(), input.IsRight());

            if (input.IsMenuSelect())
                TileEngine.TriggerEvent();
            if (input.IsMenu())
                GameStateManager.Instance.AddScreen(new WorldMenuScreen(), true, false);
            return true;
        }

        public override bool Update(GameTime gameTime)
        {
            // calculate the desired position
            if (userMovement != Vector2.Zero)
            {
                Point desiredTilePosition = TileEngine.PartyLeaderPosition.TilePosition;
                Vector2 desiredTileOffset = TileEngine.PartyLeaderPosition.TileOffset;
                PlayerPosition.CalculateMovement(TileEngine,
                    Vector2.Multiply(userMovement, 15f),
                    ref desiredTilePosition, ref desiredTileOffset);
                // check for collisions or encounters in the new tile
                if ((TileEngine.PartyLeaderPosition.TilePosition != desiredTilePosition) &&
                    !TileEngine.MoveIntoTile(desiredTilePosition))
                {
                    userMovement = Vector2.Zero;
                }
            }

            // move the party
            Point oldPartyLeaderTilePosition = TileEngine.PartyLeaderPosition.TilePosition;
            TileEngine.PartyLeaderPosition.Move(TileEngine, userMovement);

            // if the tile position has changed, check for random combat
            if (TileEngine.PartyLeaderPosition.TilePosition != oldPartyLeaderTilePosition)
            {
                int fight = random.Next(1000);
                if(fight < TileEngine.Map.RandomFightPossibility)
                    GameStateManager.Instance.AddScreen(new CombatScreen(), true, false);
            }

            // adjust the map origin so that the party is at the center of the viewport
            TileEngine.MapOriginPosition += TileEngine.ViewportCenter - (TileEngine.PartyLeaderPosition.GetScreenPosition(TileEngine));

            // make sure the boundaries of the map are never inside the viewport
            TileEngine.MapOriginPosition.X = MathHelper.Min(TileEngine.MapOriginPosition.X, TileEngine.Viewport.X);
            TileEngine.MapOriginPosition.Y = MathHelper.Min(TileEngine.MapOriginPosition.Y, TileEngine.Viewport.Y);
            TileEngine.MapOriginPosition.X += MathHelper.Max(
                (TileEngine.Viewport.X + TileEngine.Viewport.Width) -
                (TileEngine.MapOriginPosition.X + TileEngine.Map.MapDimensions.X * TileEngine.Map.TileSize.X), 0f);
            TileEngine.MapOriginPosition.Y += MathHelper.Max(
                (TileEngine.Viewport.Y + TileEngine.Viewport.Height) -
                (TileEngine.MapOriginPosition.Y + TileEngine.Map.MapDimensions.Y * TileEngine.Map.TileSize.Y), 0f);
            return true;
        }



    }
}
