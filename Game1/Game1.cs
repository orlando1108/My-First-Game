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
        
        SpriteFont Score;
        SpriteFont Level;
        
        long asteroidsTimeSpent;
       // long tirsTimeSpent;
      //  long bossFiresTimeSpent;
        int points;
        int level;
        int scoreMeter;
        int levelMeter;
        int newAsteroidSpeed;
        int timeToGenerateAsteroids;
        float transparency = 0;
       // bool gameMusicStarted = false;
       
        Texture2D BGtexture;
        Texture2D Health;
        Texture2D BGinfos;
        Texture2D gameOver_Texture;
        Rectangle scrollBG1;
        Rectangle scrollBG2;

       // List<Texture2D> Healths;
        List<Asteroid> ListeAsteroids;
        Asteroid asteroid;
        Vaisseau vaisseau;
        Boss boss;
        MainMenu mainMenu;
        PauseMenu pauseMenu;
        Settings settings;
        Media media;
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
            /*  IsFixedTimeStep = false;
             graphics.SynchronizeWithVerticalRetrace = false;*/
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 15); // 33ms = 30fps
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            settings = new Settings(1200, 800, 1, 1, true, true);
            graphics.PreferredBackBufferHeight = Settings._WindowHeight;
            graphics.PreferredBackBufferWidth = Settings._WindowWidth;
            ListeAsteroids = new List<Asteroid>();
            
            _gameState = GameStates.Loading;
            media = new Media();
            vaisseau = new Vaisseau(this, media);
            boss = new Boss(this, media);
            pauseMenu = new PauseMenu(this);
            mainMenu = new MainMenu(this, media);
            
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
            scrollBG1 = new Rectangle(0, 0, Settings._WindowWidth, Settings._WindowHeight);
            scrollBG2 = new Rectangle(0, - Settings._WindowHeight, Settings._WindowWidth, Settings._WindowHeight);
            media = new Media();
            vaisseau = new Vaisseau(this, media); // à revoir 
           // vaisseau.Initialize();
            boss = new Boss(this, media);
          //  boss.Initialize();
            newAsteroidSpeed = 2;
            timeToGenerateAsteroids = 500;
            scoreMeter = 20;
            levelMeter = 1;
            points = 0;
            level = 0;
            transparency = 0;
            ListeAsteroids.Clear();

            //set this variable in order to _gameSates
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
            media.LoadContent(Content);
            vaisseau.LoadContent(Content, "SpriteSheets/ShipTexture", "SpriteSheets/explosionSHIP", "SpriteSheets/FireEffect", "Sounds-Musics/SoundLazerShip", "Sounds-Musics/Explosion", "SpriteSheets/BoostEffect1");
            boss.LoadContent(Content, "Sprites/BossTexture", "SpriteSheets/explosionSHIP", "Sprites/HealthBoss", "Sounds-Musics/SoundLazerShip", "Sounds-Musics/Explosion", "Sprites /test3-bis");
            
        
            Score = Content.Load<SpriteFont>("SpriteFonts/SCORE");
            Level = Content.Load<SpriteFont>("SpriteFonts/LEVEL");
            Health = Content.Load<Texture2D>("Sprites/health");
            BGinfos = Content.Load<Texture2D>("Sprites/infosBar");
            gameOver_Texture = Content.Load<Texture2D>("Sprites/GameOver");
            
            pauseMenu.LoadContent(Content);
            mainMenu.LoadContent(Content);

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
            
            
            foreach (Asteroid ast in ListeAsteroids)
            {
                ast.UnloadContent();
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

            if (vaisseau.Active && boss.Active)
            {
                media.PlayGameMusics(_gameState);
            }
            
            if (_gameState == GameStates.Loading)
            {
                mainMenu.Update(gameTime);
                ExitGame(state);
            }
            if (_gameState == GameStates.Playing)
            {
                /* scroller = new Thread(UpdateBGScrolling);
                     scroller.Start();
                     scroller.Join();*/
                UpdateBGScrolling();
                PauseGame_ByKeyPress(gameTime, state);
                
                if (level < 1)
                {
                    asteroidsTimeSpent += gameTime.ElapsedGameTime.Milliseconds;

                    
                    if (level == levelMeter)
                    {
                        newAsteroidSpeed += 2;
                        levelMeter += 1;
                        timeToGenerateAsteroids -= 10;
                    }
                    //generation des asteroids toutes les 500 msec
                    if (asteroidsTimeSpent > timeToGenerateAsteroids)
                    {
                        LoadAsteroids(newAsteroidSpeed);
                        asteroidsTimeSpent = 0;
                    }
                    //Mise à jour des asteroids
                    UpdateAsteroids(gameTime, vaisseau.FiresList);

                }
                else
                {
                    boss.Update(this, gameTime, vaisseau, Content);
                }

                if (vaisseau.Active == true && vaisseau.Health == 0)
                {
                    vaisseau.Active = false;
                }
               
                if (vaisseau.Explosion.Removeable == true || boss.Explosion.Removeable == true)
                {
                    Initialize();
                }
                vaisseau.Update(gameTime, this, boss, Content);
            }
            
            if (_gameState == GameStates.Paused)
            {
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
            if (_gameState == GameStates.Playing || _gameState == GameStates.Paused)
            {
                int pixelAlign = 25;
                //graphics.GraphicsDevice.Clear(Color.AliceBlue);

                spriteBatch.Begin();
                spriteBatch.Draw(BGtexture, scrollBG1, Color.White);
                spriteBatch.Draw(BGtexture, scrollBG2, Color.White);

                vaisseau.Draw(spriteBatch);
                

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
                    transparency += 0.005f;
                    //spriteBatch.DrawString(GameOver, "GAME OVER", new Vector2((windowWidth / 2) - (GameOver.Texture.Width / 2), windowHeight / 2), Color.White);
                    spriteBatch.Draw(gameOver_Texture, new Rectangle(0, 0, Settings._WindowWidth, Settings._WindowHeight - 40), Color.White * transparency);
                }

                spriteBatch.Draw(BGinfos, new Vector2(-30, Settings._WindowHeight - 40), Color.White);
                spriteBatch.DrawString(Score, "Score: " + points, new Vector2(Settings._WindowWidth - 100, Settings._WindowHeight - 30), Color.MonoGameOrange);
                spriteBatch.DrawString(Level, "Level: " + level, new Vector2(10, Settings._WindowHeight - 30), Color.LawnGreen);

                for (int i = 0; i != vaisseau.Health; i++)
                {
                    spriteBatch.Draw(Health, new Vector2(100 + pixelAlign, Settings._WindowHeight - 35), Color.PaleGoldenrod);
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

        private void UpdateAsteroids(GameTime gameTime, List<Tir> shipFiresList)
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
                        for (int i = 0; i < shipFiresList.Count; i++)
                        {
                            if (shipFiresList[i].Collision(a, a.TextureAsteroid.SourceRec))
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
                    a.Update(gameTime, vaisseau, shipFiresList, spriteBatch);
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

       /* public void UpdateShipFires()
        {
           
        }*/

        private void LoadAsteroids(int newSpeed)
        {
            asteroid = new Asteroid(this);
            asteroid.LoadContent(Content, "SpriteSheets/AsteroidAnimation5", "SpriteSheets/AsteroidAnimation5", "SpriteSheets/AsteroidExplosion2", newSpeed);
            ListeAsteroids.Add(asteroid);
        }

        private void UpdateBGScrolling()
        {
            if (scrollBG1.Y == Settings._WindowHeight)
                scrollBG1.Y = -Settings._WindowHeight;
            if (scrollBG2.Y == Settings._WindowHeight)
                scrollBG2.Y = -Settings._WindowHeight;
            scrollBG1.Y += 1;
            scrollBG2.Y += 1;
        }

        private void PauseGame_ByKeyPress(GameTime gameTime, KeyboardState state)
        {
            
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
          
            if (!state.IsKeyDown(Keys.P))
            {
                pauseKey_OldState = false;
            }
            if (pauseKey_OldState == false && state.IsKeyDown(Keys.P))
            {
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
