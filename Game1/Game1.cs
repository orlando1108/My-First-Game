using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using SpaceShooter;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using Microsoft.Xna.Framework.Media;
using System;
using System.Windows;

namespace SpaceShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameTime gameTime = new GameTime();

        SpriteFont GameOver;
        SpriteFont Score;
        SpriteFont Level;
        // SpriteFont HealthPoints;

        SoundEffect fire;
        Song explosionShip;
        Song gameMusic;

        public const int windowHeight = 800;
        public const int windowWidth = 1000;


        long asteroidsTimeSpent;
        long tirsTimeSpent;
        long bossFiresTimeSpent;
        int points;
        int level;
        int scoreMeter;
        int levelMeter;
        int newAsteroidSpeed;
        int timeToGenerateAsteroids;
        bool paused;

        public Vector2 BGposition;
        public Texture2D BGtexture;
        public Texture2D Health;
        public Texture2D BGinfos;
        public List<Texture2D> Healths;
        public Rectangle scrollBG1;
        public Rectangle scrollBG2;

        List<Asteroid> ListeAsteroids;
        List<Tir> ListeTirs;
        Asteroid asteroid;
        //Asteroid asteroidForMissile;
        Vaisseau vaisseau;
        Boss boss;
        Tir tir;
        // Tir missile;

        Thread scroller;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            /*  IsFixedTimeStep = false;
              graphics.SynchronizeWithVerticalRetrace = false;*/
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            ListeAsteroids = new List<Asteroid>();
            ListeTirs = new List<Tir>();
            // ListeExplosions = new List<Explosion>();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            scrollBG1 = new Rectangle(0, 0, windowWidth, windowHeight);
            scrollBG2 = new Rectangle(0, -windowHeight, windowWidth, windowHeight);
            vaisseau = new Vaisseau(this);
            vaisseau.Speed = new Vector2(10, 10);
            boss = new Boss(this);
            boss.Speed = new Vector2(10, 10);
            newAsteroidSpeed = 2;
            timeToGenerateAsteroids = 500;
            scoreMeter = 20;
            levelMeter = 1;
            points = 0;
            level = 0;
            paused = false;

            ListeAsteroids.Clear();
            ListeTirs.Clear();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            BGtexture = Content.Load<Texture2D>("Scrollspace.jpg");//fond de l'ecran
            this.fire = Content.Load<SoundEffect>("SoundLazerShip");
            this.explosionShip = Content.Load<Song>("explosionTEST2");
            this.gameMusic = Content.Load<Song>("Redemption");

            MediaPlayer.Play(gameMusic);
            MediaPlayer.IsRepeating = true;

            //texture du vaisseau
            vaisseau.LoadContent(Content, "ShipTexture", "explosionSHIP", "FireEffect", "BoostEffect1");
            vaisseau.Position = new Vector2((graphics.PreferredBackBufferWidth / 2) - (vaisseau.TextureShip.Width / (vaisseau.TextureShip.Cols * 2)), (graphics.PreferredBackBufferHeight - vaisseau.TextureShip.Height * 2));

            boss.LoadContent(Content, "BossTexture", "explosionSHIP", "boss_FireTexture", "HealthBoss", "SoundLazerShip");
            //  boss.Position = Vector2.Zero;
            boss.Position = new Vector2((graphics.PreferredBackBufferWidth / 2) - (boss.Texture.Width / 2), 0);

            GameOver = Content.Load<SpriteFont>("GAME-OVER");
            Score = Content.Load<SpriteFont>("SCORE");
            Level = Content.Load<SpriteFont>("LEVEL");
            Health = Content.Load<Texture2D>("health");
            BGinfos = Content.Load<Texture2D>("backgroundINFOS");
            // HealthPoints = Content.Load<SpriteFont>("HEALTH2");

            base.LoadContent();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            BGtexture.Dispose();
            Health.Dispose();
            vaisseau.UnloadContent();
            boss.UnloadContent();
            //missile.UnloadContent();
            foreach (Asteroid ast in ListeAsteroids)
            {
                ast.UnloadContent();
            }
            foreach (Tir t in ListeTirs)
            {
                t.UnloadContent();
            }

            base.UnloadContent();

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            pauseGame(gameTime);

            if (paused == false)
            {
                KeyboardState state = Keyboard.GetState();
                asteroidsTimeSpent += gameTime.ElapsedGameTime.Milliseconds;
                tirsTimeSpent += gameTime.ElapsedGameTime.Milliseconds;
                bossFiresTimeSpent += gameTime.ElapsedGameTime.Milliseconds;

                /* scroller = new Thread(UpdateBGScrolling);
                 scroller.Start();
                 scroller.Join();*/
                UpdateBGScrolling();


                if (level == levelMeter)
                {
                    newAsteroidSpeed += 1;
                    levelMeter += 1;
                    timeToGenerateAsteroids -= 2;
                }
                //generation des asteroids toutes les 500 msec
                /*  if (asteroidsTimeSpent > timeToGenerateAsteroids)
                  {
                      LoadAsteroids(newAsteroidSpeed);
                      asteroidsTimeSpent = 0;
                  }
                  //Mise à jour des asteroids
                  UpdateAsteroids(gameTime);*/

                //Mise à jour des tirs
                UpdateShipFires();

                vaisseau.Update(gameTime, state, windowHeight, windowWidth);
                boss.Update(this, gameTime, vaisseau);

                if (vaisseau.Active == true & vaisseau.Health == 0)
                {
                    vaisseau.Active = false;
                    MediaPlayer.Play(explosionShip);
                    MediaPlayer.Volume = 1.0f;
                    MediaPlayer.IsRepeating = false;
                }

                if (boss.Active == true & boss.Health == 0)
                {
                    boss.Active = false;
                    MediaPlayer.Play(explosionShip);
                    MediaPlayer.Volume = 1.0f;
                    MediaPlayer.IsRepeating = false;
                }

                if (vaisseau.Explosion.Removeable == true || boss.Explosion.Removeable == true)
                {
                    Initialize();
                }

              

                base.Update(gameTime);

                

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            int pixelAlign = 25;
            graphics.GraphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(BGtexture, scrollBG1, Color.White);
            spriteBatch.Draw(BGtexture, scrollBG2, Color.White);

            vaisseau.Draw(spriteBatch);
            boss.Draw(spriteBatch);

            foreach (Tir t in ListeTirs)
            {
                t.Draw(spriteBatch);
            }

            /* for (int i = 0; i < ListeAsteroids.Count; i++)
             {
                  ListeAsteroids[i].Draw(spriteBatch);

             }*/

            if (vaisseau.Active == false)
            {
                spriteBatch.DrawString(GameOver, "GAME OVER", new Vector2((windowWidth / 2) - (GameOver.Texture.Width / 2), windowHeight / 2), Color.White);
            }

            spriteBatch.Draw(BGinfos, new Vector2(0, windowHeight - 40), Color.White);
            spriteBatch.DrawString(Score, "Score: " + points, new Vector2(windowWidth - 100, windowHeight - 30), Color.DarkBlue);
            spriteBatch.DrawString(Level, "Level: " + level, new Vector2(10, windowHeight - 30), Color.DarkMagenta);

            for (int i = 0; i != vaisseau.Health; i++)
            {
                spriteBatch.Draw(Health, new Vector2(100 + pixelAlign, windowHeight - 35), Color.Beige);
                pixelAlign += 25;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateAsteroids(GameTime gameTime)
        {
            if (ListeAsteroids != null)
            {
                foreach (Asteroid a in ListeAsteroids)
                {
                    if (vaisseau.Active == true && a.Active == true && a.Collision(vaisseau))
                    {
                        a.Active = false;
                        vaisseau.Health -= 1;
                    }
                    if (a.Active == true)
                    {
                        for (int i = 0; i < ListeTirs.Count; i++)
                        {
                            if (ListeTirs[i].Collision(a))
                            {
                                a.Active = false;
                                points += 1;

                                if (points == scoreMeter)
                                {
                                    level += 1;
                                    scoreMeter += 20;
                                }
                            }
                        }
                    }
                    a.Update(gameTime, vaisseau, ListeTirs, spriteBatch);
                }
                for (int i = 0; i < ListeAsteroids.Count; i++)
                {
                    if (ListeAsteroids[i].Explosion.Removeable == true)
                    {
                        ListeAsteroids.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void UpdateShipFires()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && tirsTimeSpent > 300)
            {
                tir = new Tir(this, -15);
                tir.LoadContent(Content, "fire", vaisseau.Rec);
                tir.Position = new Vector2((vaisseau.Rec.X + (vaisseau.Rec.Width / 2)) - tir.Texture.Width / 2, +vaisseau.Rec.Y);
                ListeTirs.Add(tir);
                fire.CreateInstance().Play();
                tirsTimeSpent = 0;
            }
            else
            {
                vaisseau.FireActive = false;
            }
            foreach (Tir t in ListeTirs)
            {
                if(boss.Active && t.Collision(boss))
                {
                    t.Active = false;
                    boss.Health -= 1;
                }
                t.Update(gameTime);

            }
            for (int i = 0; i < ListeTirs.Count; i++)
            {
                if (ListeTirs[i].Active == false)
                {
                    ListeTirs.RemoveAt(i);
                    i -= 1;
                }
            }
        }

        public void LoadAsteroids(int newSpeed)
        {
            asteroid = new Asteroid(this);
            asteroid.LoadContent(Content, "AsteroidAnimation5", "AsteroidAnimation5", "AsteroidExplosion2", newSpeed);
            ListeAsteroids.Add(asteroid);

        }

        public void UpdateBGScrolling()
        {
            if (scrollBG1.Y == windowHeight)
                scrollBG1.Y = -windowHeight;
            if (scrollBG2.Y == windowHeight)
                scrollBG2.Y = -windowHeight;

            scrollBG1.Y += 1;
            scrollBG2.Y += 1;
        }

        /*  public delegate void OnKeyDownEventHandler(object sender, EventArgs e);
          public event OnKeyDownEventHandler Changed;*/


        public void pauseGame(GameTime gameTime)
        { // state.GetPressedKeys() += new OnKeyDownEventHandler(MainWindow_KeyDown);
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
            KeyboardState state = Keyboard.GetState();


            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                    ButtonState.Pressed || Keyboard.GetState().
                    IsKeyDown(Keys.Escape))
                    Exit();

                if (paused == false & Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    paused = true;
                }


            }

        }

        /* private void OnKeyDownHandler(object sender, OnKeyDownEventArgs e)
         {
             if (e.Key == Keys.P)
             {
                 paused = true;
             }
         }
public void LoadMissile()
{
    // int i = 0;
    tir = new Tir(this);
    tir.LoadContent(Content, "fire2", vaisseau);

    //Vector2 Destination = ListeAsteroids[i].Origine;
}
public void UpdateMissile()
{

}*/

        /* static bool intersectPixels(Rectangle rec1, Color[] data1,
                                     Rectangle rec2, Color[] data2)
        {
            int top = Math.Max(rec1.Top, rec2.Top);
            int bottom = Math.Min(rec1.Bottom, rec2.Bottom);
            int left = Math.Max(rec1.Left, rec2.Left);
            int right = Math.Min(rec1.Right, rec2.Right);

            Color color1 = Color.White;
            Color color2 = Color.White;
            for (int y = top; y<bottom ; y++)
            {
                for(int x = left; x<right; x++)
                {


                    if (x >= 0 && x < rec1.Width && y >= 0 && y < rec1.Height)
                    {

                        color1 = data1[(x - rec1.Left) +
                        (y - rec1.Top) * rec1.Width];
                    }
                    if (x >= 0 && x < rec2.Width && y >= 0 && y < rec2.Height)
                    {
                         color2 = data2[(x - rec2.Left) +
                       (y - rec2.Top) * rec2.Width];
                    }


                    if (color1.A != 0 && color2.A != 0)
                        return true;   
                }

            }


            return false;
        }*/

        /*TODO pierre:
         *regenerer la vie du vaisseau en fonction du level
         * rajouter de la vie aux asteroids.
         * 
         * 
         * HomerVador
         * 
         */




    }
}
