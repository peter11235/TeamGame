using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    class Platform : Sprite
    {
        public bool isMoving = false;
        public CollisionOffset coll;
        public Point frameSize;
        public float speed = 10;
        public Platform(Texture2D texture, Point frameSize, Vector2 position, bool moving)
            : base(new SpriteSheet(texture, new Point(0, 0), 1.0f), position,
            new CollisionOffset(4, 4, 4, 4))
        {
            isMoving = moving;
            coll = new CollisionOffset(5, 5, 5, 5);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(0, 0), 10);
            this.frameSize = frameSize;
            spriteSheet.setCurrentSegment(0);
        }

        public void oscillate()
        {
            float minX = 250f;
            float maxX = 700f;
            this.position.X += speed;
            if (this.position.X < minX || this.position.X > maxX)
                speed = -speed;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
            base.Update(gameTime, clientBounds);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            base.Draw(gameTime, spriteBatch);
        }

        public override Vector2 direction
        {
            get
            {
                Vector2 zero = new Vector2(0, 0);
                return zero;
            }

        }
    }
}