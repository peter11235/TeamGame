using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    abstract class Sprite
    {
        protected SpriteSheet spriteSheet;
        protected Point currentFrame;
        protected bool pauseAnimation = false;
        protected SpriteEffects effects = SpriteEffects.None;

        int collisionOffset;
        int timeSinceLastFrame = 0;

        protected Vector2 speed;
        protected Vector2 position;

        public Sprite(SpriteSheet aSpriteSheet, Vector2 aPosition,  int aCollisionOffset, Vector2 aSpeed)
        {
            this.spriteSheet = aSpriteSheet;
            this.position = aPosition;
            this.collisionOffset = aCollisionOffset;
            this.speed = aSpeed;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame >= spriteSheet.currentSegment.millisPerFrame)
            {
                timeSinceLastFrame -= spriteSheet.currentSegment.millisPerFrame;
                if (!pauseAnimation)
                {
                    ++currentFrame.X;
                    if (currentFrame.X >= spriteSheet.sheetSize.X || currentFrame.X > spriteSheet.currentSegment.endFrame.X)
                    {
                        currentFrame.X = spriteSheet.currentSegment.startFrame.X;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= spriteSheet.sheetSize.Y || currentFrame.Y > spriteSheet.currentSegment.endFrame.Y)
                        {
                            currentFrame.Y = spriteSheet.currentSegment.startFrame.Y;
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine(); = System.out.println();
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet.texture,//what to draw
                position, //where to draw
                new Rectangle(currentFrame.X * spriteSheet.currentSegment.frameSize.X, //how big of a rectangle
                    currentFrame.Y * spriteSheet.currentSegment.frameSize.Y, 
                    spriteSheet.currentSegment.frameSize.X,
                    spriteSheet.currentSegment.frameSize.Y),
                Color.White, //tint
                0,
                Vector2.Zero,
                1f, //this is scaling
                effects, 
                0);

        }

        public abstract Vector2 direction //subclasses will have direction defined
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle((int)position.X + collisionOffset, (int)position.Y + collisionOffset,
                    spriteSheet.currentSegment.frameSize.X - (collisionOffset * 2),
                    spriteSheet.currentSegment.frameSize.Y - (collisionOffset * 2));
            }
        }
    }
}
