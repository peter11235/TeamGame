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


namespace GameOne
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Sprite player1;
        //Sprite player2;
        Projectile projectile;
        //Reticle reticle;
        List<Sprite> spriteList = new List<Sprite>();
        List<Platform> platy = new List<Platform>();
        List<Chicken> chicks = new List<Chicken>();
        Texture2D background;
        SoundEffect scream;
        SoundEffect win;
        SoundEffect lose;
        SoundEffectInstance winInstance;
        SoundEffectInstance loseInstance;
        SoundEffectInstance screamInstance;

        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue music;

        public int p1Hits = 0;
        public int foxyCounter = 0;

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>(@"Images/tripy");

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            Texture2D icePlat = Game.Content.Load<Texture2D>(@"Images/platform_ice_001-f");
            
            player1 = new GlitchPlayer(Game.Content.Load<Texture2D>(@"Images/LadyZ"));
            Chicken chick = new Chicken(Game.Content.Load<Texture2D>(@"Images/fox"), 50f, 800f, 1300f);
            chicks.Add(chick);
            //player2 = new GlitchPlayer(Game.Content.Load<Texture2D>(@"Images/demon"), 2);
            Platform plat1 = new Platform(Game.Content.Load<Texture2D>(@"Images/milkymilk"), new Point(250, 88),new Vector2(100,100), false);
            Platform plat2 = new Platform(Game.Content.Load<Texture2D>(@"Images/milkymilk"), new Point(250, 88), new Vector2(500, 600), false);
            Platform plat3 = new Platform(Game.Content.Load<Texture2D>(@"Images/milkymilk"), new Point(250, 88), new Vector2(50, 500), false);
            Platform plat4 = new Platform(icePlat, new Point(250, 88), new Vector2(250,300), true);
            Platform plat5 = new Platform(Game.Content.Load<Texture2D>(@"Images/milkymilk"), new Point(250, 88), new Vector2(800, 150), false);
            
            projectile = new Projectile(Game.Content.Load<Texture2D>(@"Images/fireball2"));

            scream = Game.Content.Load<SoundEffect>(@"Audio/WilhelmScream");
            screamInstance = scream.CreateInstance();
            
            win = Game.Content.Load<SoundEffect>(@"Audio/win_sound");
            winInstance = win.CreateInstance();

            lose = Game.Content.Load<SoundEffect>(@"Audio/fire");
            loseInstance = lose.CreateInstance();

            audioEngine = new AudioEngine(@"Content\Audio\music.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\Sound Bank.xsb");
            music = soundBank.GetCue("j_dilla_donuts");
            music.Play();

            //reticle = new Reticle(Game.Content.Load<Texture2D>(@"Images/reticle"));
            spriteList.Add(player1);
            spriteList.Add(chick);
            //spriteList.Add(player2);
            spriteList.Add(projectile);
            platy.Add(plat1);
            platy.Add(plat2);
            platy.Add(plat3);
            platy.Add(plat4);
            platy.Add(plat5);
            //spriteList.Add(reticle);
            

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            int width = Game.Window.ClientBounds.Width;
            int height = Game.Window.ClientBounds.Height;
            Vector2 startPosition = new Vector2(0, (float)height);
            //MouseState mouseState = Mouse.GetState();
            //Vector2 path;
            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            //    path = new Vector2((float)mouseState.X, (float)mouseState.Y);
            //    projectile = new Projectile(path);
            //    spriteList.Add(projectile);
            //}
            player1.Update(gameTime, Game.Window.ClientBounds);
            //player2.Update(gameTime, Game.Window.ClientBounds);
            //reticle.Update(gameTime, Game.Window.ClientBounds);
            Vector2 mousePath;
            MouseState mouseState = Mouse.GetState();
            
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mousePath = new Vector2(mouseState.X, mouseState.Y);
                projectile.shoot(mousePath);
            }
            // update each automated sprite
            foreach (Sprite sprite in spriteList)
                sprite.Update(gameTime, Game.Window.ClientBounds);
            
            // collision/ is off screen?

            //Projectile hit?
            if (projectile.collisionRect.Intersects(player1.collisionRect)) 
            {
                screamInstance.Play();
                projectile.position = Vector2.Zero;
                player1.position = startPosition;
                ++p1Hits;
                if (p1Hits >= 6)
                {

                    loseInstance.Play();
                    
                }
            }
            /*
            if (projectile.collisionRect.Intersects(player2.collisionRect))
            {
                projectile.position = Vector2.Zero;
                player2.position = startPosition;
                ++p2Hits;
                if (p2Hits >= 6)
                {
                    //Game.Exit();
                }
            }
            */
            if (projectile.position.X > width - 2 || projectile.position.X < 2
                || projectile.position.Y > height -2 || projectile.position.Y < 2)
            {
                projectile.position = Vector2.Zero;
                projectile.shoot(Vector2.Zero);
            }
            foreach (Platform plat in platy)
            {
                if (plat.isMoving)
                {
                    plat.oscillate();
                }
                if (player1.collisionRect.Intersects(plat.collisionRect))
                {
                    player1.Collision(plat);
                }
                /*
                if (player2.collisionRect.Intersects(plat.collisionRect))
                {
                    player2.Collision(plat);
                }
                 * */
            }
            Chicken hick = null;
            foreach (Chicken chik in chicks)
            {
                if (chik.collisionRect.Intersects(player1.collisionRect))
                {
                    player1.Collision(chik);
                    ++foxyCounter;
                    if (foxyCounter >= 2)
                    {
                        winInstance.Play();
                        foxyCounter = 0;
                        p1Hits = 0;
                    }

                }
            }
            
         
            foreach (Sprite sprite in spriteList)
            {
                if (sprite.collisionRect.Intersects(player1.collisionRect))
                {
                    player1.Collision(sprite);
                    sprite.Collision(player1);
                    
                }
                /*
                if (sprite.collisionRect.Intersects(player2.collisionRect))
                {
                    player2.Collision(sprite);
                    sprite.Collision(player2);
                }
                 * */
            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, Game.Window.ClientBounds.Width, 
                Game.Window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

            player1.Draw(gameTime, spriteBatch);
            //player2.Draw(gameTime, spriteBatch);
            //reticle.Draw(gameTime, spriteBatch);
            foreach (Sprite sprite in spriteList)
                sprite.Draw(gameTime, spriteBatch);
            foreach (Platform plat in platy)
            {
                ((Sprite)plat).Draw(gameTime, spriteBatch);
            }
            foreach (Chicken chick in chicks)
            {
                ((Sprite)chick).Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
