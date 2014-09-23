using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOne
{
    class UserControlledSprite : Sprite
    {
        protected Vector2 velocity;
        Vector2 friction;
        Vector2 speed;
        protected readonly Vector2 gravity = new Vector2(0, 2f);
        protected bool onGround = false;
        Vector2 oldPosition = new Vector2(-1, -1);

        public UserControlledSprite(SpriteSheet spriteSheet, Vector2 position, Point collisionOffset, Vector2 speed)
            : base(spriteSheet, position, collisionOffset, speed)
        {
        }

        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;
                

                /* keyboard input */
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    inputDirection.Y += 1;
                

                /* gamepad input */
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                /* do we need to flip the image? */
                if (inputDirection.X < 0)
                    effects = SpriteEffects.FlipHorizontally;
                else if (inputDirection.X > 0)
                    effects = SpriteEffects.None;

                return inputDirection * speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            /*
            velocity += direction * speed;
            velocity += gravity;
            velocity *= friction;

            onGround = position.Y == oldPosition.Y;
            oldPosition = position;

            position += velocity;
             */
            position += direction;

            // If sprite is off the screen, move it back within the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - spriteSheet.currentSegment.frameSize.X)
                position.X = clientBounds.Width - spriteSheet.currentSegment.frameSize.X;
            if (position.Y > clientBounds.Height - spriteSheet.currentSegment.frameSize.Y)
                position.Y = clientBounds.Height - spriteSheet.currentSegment.frameSize.Y;

            base.Update(gameTime, clientBounds);
        }
    }
}
