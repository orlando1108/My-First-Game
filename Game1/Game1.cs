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
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameTime gameTime = new GameTime();

        // SpriteFont GameOver;
        SpriteFont Score;
        SpriteFont Level;
        // SpriteFont HealthPoints;

        SoundEffect fire;
        Song explosionShip;
        Song gameMusic;
        

        public const int windowHeight = 800;
        public const int windowWidth = 1200;


        long asteroidsTimeSpent;
        long tirsTimeSpent;
        long bossFiresTimeSpent;
        int points;
        int level;
        int scoreMeter;
        int levelMeter;
        int newAsteroidSpeed;
        int timeToGenerateAsteroids;
        float transparency = 0;
        bool gameMusicStarted = false;
        int test = 0;


        Texture2D BGtexture;
        Texture2D Health;
        Texture2D BGinfos;
        Texture2D gameOver_Texture;
        Rectangle scrollBG1;
        Rectangle scrollBG2;

       // List<Texture2D> Healths;
        List<Asteroid> ListeAsteroids;
        List<Tir> ListeTirs;
        Asteroid asteroid;
        Vaisseau vaisseau;
        Boss boss;
        Tir tir;
        MainMenu mainMenu;
        PauseMenu pauseMenu;
        public static bool pauseKey_OldState;
        public enum GameStates
        {
            Loading,
            Playing,
            Paused,
        }
        public static GameStates _gameState { get; set; }
        
        //Thread scroller;

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
            _gameState = GameStates.Loading;
            pauseMenu = new PauseMenu(this);
            mainMenu = new MainMenu(this);
            pauseKey_OldState = false;
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
            transparency = 0;
            //paused = false;

            ListeAsteroids.Clear();
            ListeTirs.Clear();
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BGtexture = Content.Load<Texture2D>("Sprites/Scrollspace.jpg");//fond de l'ecran
            this.fire = Content.Load<SoundEffect>("Sounds-Musics/SoundLazerShip");
            this.explosionShip = Content.Load<Song>("Sounds-Musics/explosionTEST2");
            this.gameMusic = Content.Load<Song>("Sounds-Musics/Redemption");

            

            //texture du vaisseau
            vaisseau.LoadContent(Content, "SpriteSheets/ShipTexture", "SpriteSheets/explosionSHIP", "SpriteSheets/FireEffect", "SpriteSheets/BoostEffect1");
           
            boss.LoadContent(Content, "Sprites/BossTexture", "SpriteSheets/explosionSHIP", "Sprites/HealthBoss", "Sounds-Musics/SoundLazerShip", "Sprites/test3-bis");


            // GameOver = Content.Load<SpriteFont>("SpriteFonts/GAME-OVER");
            Score = Content.Load<SpriteFont>("SpriteFonts/SCORE");
            Level = Content.Load<SpriteFont>("SpriteFonts/LEVEL");
            Health = Content.Load<Texture2D>("Sprites/health");
            BGinfos = Content.Load<Texture2D>("Sprites/backgroundINFOS");
            gameOver_Texture = Content.Load<Texture2D>("Sprites/GameOver");
            // HealthPoints = Content.Load<SpriteFont>("HEALTH2");
            pauseMenu.LoadContent(Content, "Sounds-Musics/menuMusic");
            mainMenu.LoadContent(Content, "Sounds-Musics/menuMusic");

            base.LoadContent();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // add unload method for all menu objects
            BGtexture.Dispose();
            Health.Dispose();
            gameOver_Texture.Dispose();
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
            KeyboardState state = Keyboard.GetState();
            if (_gameState == GameStates.Loading)
            {
                mainMenu.Update(gameTime);
                ExitGame(state);
            }
            if (_gameState == GameStates.Playing)
            {
                tirsTimeSpent += gameTime.ElapsedGameTime.Milliseconds;
                UpdateBGScrolling();
                PauseGame_ByKeyPress(gameTime, state);
                
                
                if (!gameMusicStarted)
                {
                    MediaPlayer.Play(gameMusic);
                    MediaPlayer.IsRepeating = true;
                    gameMusicStarted = true;
                }

                if (level < 1)
                {
                    asteroidsTimeSpent += gameTime.ElapsedGameTime.Milliseconds;

                    /* scroller = new Thread(UpdateBGScrolling);
                     scroller.Start();
                     scroller.Join();*/


                    if (level == levelMeter)
                    {
                        newAsteroidSpeed += 2;
                        levelMeter += 1;
                        timeToGenerateAsteroids -= 5;
                    }
                    //generation des asteroids toutes les 500 msec
                    if (asteroidsTimeSpent > timeToGenerateAsteroids)
                    {
                        LoadAsteroids(newAsteroidSpeed);
                        asteroidsTimeSpent = 0;
                    }
                    //Mise à jour des asteroids
                    UpdateAsteroids(gameTime);

                    //Mise à jour des tirs
                   

                    // TODO: ameliorer ca, mettre active = false dans les classes respectives

                }
                else
                {
                    bossFiresTimeSpent += gameTime.ElapsedGameTime.Milliseconds;
                    if (boss.Active == true && boss.Health == 0)
                    {
                        boss.Active = false;
                        MediaPlayer.Play(explosionShip);
                        MediaPlayer.Volume = 1.0f;
                        MediaPlayer.IsRepeating = false;
                    }
                    boss.Update(this, gameTime, vaisseau);
                }

                if (vaisseau.Active == true && vaisseau.Health == 0)
                {
                    vaisseau.Active = false;
                    MediaPlayer.Play(explosionShip);
                    MediaPlayer.Volume = 1.0f;
                    MediaPlayer.IsRepeating = false;

                }
                else if (vaisseau.Active == false)
                {
                    transparency += 0.005f;
                }

                if (vaisseau.Explosion.Removeable == true || boss.Explosion.Removeable == true)
                {
                    Initialize();
                }
                UpdateShipFires();
                vaisseau.Update(gameTime, windowHeight, windowWidth);
            }


            if (_gameState == GameStates.Paused)
            {
                gameMusicStarted = false;
                // ResumeGame(gameTime);

                pauseMenu.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if(_gameState == GameStates.Loading)
            {
                //graphics.GraphicsDevice.Clear(Color.DarkCyan);
                spriteBatch.Begin();
                mainMenu.Draw(spriteBatch);
                spriteBatch.End();
            }
            if (_gameState == GameStates.Playing)
            {
                int pixelAlign = 25;
                //graphics.GraphicsDevice.Clear(Color.AliceBlue);

                spriteBatch.Begin();
                spriteBatch.Draw(BGtexture, scrollBG1, Color.White);
                spriteBatch.Draw(BGtexture, scrollBG2, Color.White);

                vaisseau.Draw(spriteBatch);
                foreach (Tir t in ListeTirs)
                {
                    t.Draw(spriteBatch);
                }

                if (level < 1)
                {
                    for (int i = 0; i < ListeAsteroids.Count; i++)
                    {
                        ListeAsteroids[i].Draw(spriteBatch);

                    }
                }
                else
                {
                    boss.Draw(spriteBatch);
                }

                if (vaisseau.Active == false)
                {

                    //spriteBatch.DrawString(GameOver, "GAME OVER", new Vector2((windowWidth / 2) - (GameOver.Texture.Width / 2), windowHeight / 2), Color.White);
                    spriteBatch.Draw(gameOver_Texture, new Rectangle(0, 0, windowWidth, windowHeight - 40), Color.White * transparency);
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
            if(_gameState == GameStates.Paused)
            {
                //graphics.GraphicsDevice.Clear(Color.DarkCyan);
                spriteBatch.Begin();
                
                pauseMenu.Draw(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public void UpdateAsteroids(GameTime gameTime)
        {
            if (ListeAsteroids != null)
            {
                foreach (Asteroid a in ListeAsteroids)
                {
                    if (vaisseau.Active == true && a.Active == true && a.Collision(vaisseau, vaisseau.TextureShip.SourceRec, a.TextureAsteroid.SourceRec))
                    {
                        a.Active = false;
                        vaisseau.Health -= 1;
                    }
                    if (a.Active == true)
                    {
                        for (int i = 0; i < ListeTirs.Count; i++)
                        {
                            if (ListeTirs[i].Collision(a, a.TextureAsteroid.SourceRec))
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && tirsTimeSpent > 200)
            {
                tir = new Tir(this, -15);
                tir.LoadContent(Content, "Sprites/fire");
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
                if (boss.Active && t.Collision(boss))
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
            asteroid.LoadContent(Content, "SpriteSheets/AsteroidAnimation5", "SpriteSheets/AsteroidAnimation5", "SpriteSheets/AsteroidExplosion2", newSpeed);
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

        public void PauseGame_ByKeyPress(GameTime gameTime, KeyboardState state)
        {
            
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
          
            if (!state.IsKeyDown(Keys.P))
            {
                pauseKey_OldState = false;
            }
            if (pauseKey_OldState == false && state.IsKeyDown(Keys.P))
            {
                test += 1;
                MediaPlayer.Stop();
                gameMusicStarted = false;
                pauseKey_OldState = true;
                pauseMenu.pauseKey_OldState = true;
                _gameState = GameStates.Paused;
            }

        }

        private void ExitGame(KeyboardState state)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                state.IsKeyDown(Keys.Escape) || mainMenu.Button_Quit.Clicked == true)
                Exit();
        }

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
