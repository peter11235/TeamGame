using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SimpleSprite
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D glitch;
        
        Point frameSize = new Point(192,155);
        Point currentFrame = new Point(14, 0);
        Point sheetSize = new Point(12, 1);
        Vector2 position = Vector2.Zero;

        SpriteEffects direction = SpriteEffects.None;
        bool isMoving = false;
        bool isJumping = false;

        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 50;
        float speed = 2f;
        float stickSpeed = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            //This resets framerate.
            //TargetElapsedTime = new TimeSpan(0,0,0,0,50);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            glitch = Content.Load<Texture2D>(@"Images/glitch");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                isMoving = true;
                direction = SpriteEffects.None;
                position.X += speed;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                isMoving = true;
                direction = SpriteEffects.FlipHorizontally;
                position.X -= speed;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X != 0)
            {
                stickSpeed = 2 * GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                isMoving = true;
                if (stickSpeed < 0)
                {
                    direction = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    direction = SpriteEffects.None;
                }
                
                position.X += stickSpeed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isJumping = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D) && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 0)
            {
                isMoving = false;
            }
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame >= millisecondsPerFrame && isMoving)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                if (isJumping)
                {
                    currentFrame.X = 12;
                    isJumping = false;
                }
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                }
            }
            else if (!isMoving)
            {
                currentFrame.X = 14;
            }
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(glitch, 
                position, 
                new Rectangle((currentFrame.X * frameSize.X), (currentFrame.Y * frameSize.Y), frameSize.X, frameSize.Y), 
                Color.White,
                0, 
                Vector2.Zero,
                1,
                direction,
                0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
