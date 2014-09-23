using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Design;


namespace GameOne
{
    class Projectile : Sprite
    {
        Point frameSize = new Point(64, 64);
        Vector2 path = Vector2.Zero;
        Point startFrame = new Point(0, 0);
        Point endFrame = new Point(4, 4);
        const float speed = .02f;
        bool shot = false; 

        //static Texture2D image = Game.Content.Load<Texture2D>(@"Images/fireball2");
        //static Texture2D image = ContentManager.Load<Texture2D>(@"Images/fireball2");

        public Projectile(Texture2D image)
            : base(new SpriteSheet(image, new Point(4,4), 1f), Vector2.Zero, new CollisionOffset(0,0,0,0))
        {
            spriteSheet.addSegment(frameSize, startFrame, endFrame, 40);
            spriteSheet.setCurrentSegment(0);
            
        }

        public override Vector2 direction
        {
            
            get
            { 
                Vector2 direction = path;
                return path;
            }
           
        }

        public void shoot(Vector2 path)
        {
            shot = true;
            this.path = path;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position.X += path.X * speed;
            position.Y += path.Y * speed;
            if (currentFrame == endFrame)
            {
                currentFrame = startFrame;
            }
            int width = clientBounds.Width;
            int height = clientBounds.Height;

            base.Update(gameTime, clientBounds);
        }
    }
}
