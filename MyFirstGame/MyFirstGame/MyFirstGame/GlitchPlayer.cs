using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    class GlitchPlayer : UserControlledSprite
    {
        const int numberStates = 4;
        enum GlitchPlayerState
        {
            Walking, 
            Climbing,
            Jumping,
            Sleeping
        }
        GlitchPlayerState currentState;
        AbstractState[] states;

        public GlitchPlayer(Texture2D image) : base(new SpriteSheet(image, new Point(21,6)), Vector2.Zero,
            10, new Vector2(2,2))
        {
            Point frameSize = new Point(192, 160);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(11, 0), 50);
            spriteSheet.addSegment(frameSize, new Point(0, 1), new Point(18, 1), 50);
            spriteSheet.addSegment(frameSize, new Point(0, 2), new Point(11, 3), 50);
            spriteSheet.addSegment(frameSize, new Point(0, 4), new Point(20, 5), 50);

            states = new AbstractState[numberStates];
            states[(Int32)GlitchPlayerState.Walking] = new WalkingState(this);
            states[(Int32)GlitchPlayerState.Climbing] = new ClimbingState(this);
            states[(Int32)GlitchPlayerState.Jumping] = new JumpingState(this);
            states[(Int32)GlitchPlayerState.Sleeping] = new SleepingState(this);
       }
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            states[(Int32)currentState].Update(gameTime, clientBounds);
            base.Update(gameTime, clientBounds);
        }
        private abstract class AbstractState
        {
            protected readonly GlitchPlayer player;

            protected AbstractState(GlitchPlayer aPlayer)
            {
                this.player = aPlayer;
            }

            public virtual void Update(GameTime gameTime, Rectangle clientBounds)
            {}
        }

        private class WalkingState : AbstractState
        {
            Point stillFrame;
            int timeSinceLastMove = 0;
            const int timeForSleep = 3000;
            public WalkingState(GlitchPlayer aPlayer) : base(aPlayer)
            {
                stillFrame = new Point(14, 0);
            }

            public override void Update(GameTime gameTime, Rectangle clientBounds)
            {
                //pause animation if sprite is not moving
                if (player.direction.X == 0)
                {
                    player.pauseAnimation = true;
                    player.currentFrame = stillFrame;
                }
                else
                {
                    timeSinceLastMove = 0;
                    player.pauseAnimation = false;
                }

                //sleeping 
                timeSinceLastMove += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastMove > timeForSleep)
                {
                    timeSinceLastMove = 0;
                    player.currentState = GlitchPlayerState.Sleeping;
                    player.spriteSheet.setCurrentSegment((Int32)GlitchPlayerState.Sleeping);
                    player.currentFrame = player.spriteSheet.currentSegment.startFrame;
                }
            }
        }
        private class ClimbingState : AbstractState
        {
            public ClimbingState(GlitchPlayer aPlayer)
                : base(aPlayer)
            {
            }
        }
        private class JumpingState : AbstractState
        {
            public JumpingState(GlitchPlayer aPlayer)
                : base(aPlayer)
            {
            }
        }
        private class SleepingState : AbstractState
        {
            public SleepingState(GlitchPlayer aPlayer)
                : base(aPlayer)
            {
            }

            
        }
    }
}
