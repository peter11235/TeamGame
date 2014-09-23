using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    class UserControlledSprite : Sprite
    {
        public UserControlledSprite(SpriteSheet aSpriteSheet, Vector2 aPosition, int aCollisionOffset, Vector2 aSpeed)
            : base(aSpriteSheet, aPosition, aCollisionOffset, aSpeed)
        {}

        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    inputDirection.X -= 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    inputDirection.X += 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    inputDirection.Y -= 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    inputDirection.Y += 1;
                }

                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
                if (gamePadState.ThumbSticks.Left.X != 0)
                {
                    inputDirection.X += gamePadState.ThumbSticks.Left.X;
                }
                if (gamePadState.ThumbSticks.Left.Y != 0)
                {
                    inputDirection.Y -= gamePadState.ThumbSticks.Left.Y;
                }

                if (inputDirection.X < 0)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                else if (inputDirection.X > 0)
                {
                    effects = SpriteEffects.None;
                }

                return inputDirection * speed;
            } 
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;
            //this is where window edges should be bounded
            
            base.Update(gameTime, clientBounds);
        }
    }
}
