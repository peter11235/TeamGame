using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOne
{
    class Chicken : AutomatedSprite
    {
        public float start;
        public float finish;
        public float height; 
        //153
        //139
        public Chicken(Texture2D texture, float height, float start, float finish)
            :base(new SpriteSheet(texture, new Point(24, 0), .75f), new Vector2(850, 50), 
            new CollisionOffset(50, 0, 50, 50), new Vector2(-2f, 0))
        {
            this.height = height;
            this.start = start;
            this.finish = finish;
            Point frameSize = new Point(153, 139);
            spriteSheet.addSegment(frameSize, new Point(0, 0), new Point(1, 5), 50);
            
            spriteSheet.setCurrentSegment(0);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
            position.Y = height;
            if(this.position.X<start){
                this.position.X = this.start;
                this.speed.X = this.speed.X * -1;
            }
            if(this.position.X > finish){
                effects = SpriteEffects.FlipVertically;
                this.position.X = this.start;
                this.speed.X = this.speed.X * -1;
            }
            base.Update(gameTime, clientBounds);
        }

        public override void Collision(Sprite otherSprite)
        {
            System.Diagnostics.Debug.WriteLine("Chicken Collision");
            position.X += 20f;
            speed *= -1;
        }
    }
}
