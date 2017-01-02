using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class MainMenu
    {
        private Animation _button_Play;
        public Animation Button_Play
        {
            get { return _button_Play; }
            set { _button_Play = value; }
        }

        private Animation _button_YourShip;
        public Animation Button_YourShip
        {
            get { return _button_YourShip; }
            set { _button_YourShip = value; }
        }

        private Animation _button_Settings;
        public Animation Button_Settings
        {
            get { return _button_Settings; }
            set { _button_Settings = value; }
        }
        
        private Song _menuMusic;
        public Song MenuMusic
        {
            get
            {
                return _menuMusic;
            }
            set { _menuMusic = value; }
        }

        public KeyboardState oldState;
        private SpriteFont gameTitle;
        private String text_GameTitle;
        private Texture2D backGround;
        private Vector2 center;
        static public bool MusicStarted = false;

        public MainMenu(Game game)
        {
            center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
            _button_Play = new Animation(game, 1, 2, 1);
            _button_YourShip = new Animation(game, 1, 2, 1);
            _button_Settings = new Animation(game, 1, 2, 1);
            _button_Play.Active = true;
            _button_Settings.Active = true;
            _button_YourShip.Active = true;
            text_GameTitle = "             GALACTOR\nThe best video game of all time !\n     Et ouais ma gueule !!!";
            // _buttonResume.Moving = false;
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager content, String menuMusicName)
        {
            backGround = content.Load<Texture2D>("MainMenu-Items/BackGround-StartMenu");
            _button_Play.LoadContent(content, "MainMenu-Items/Play");
            _button_Settings.LoadContent(content, "MainMenu-Items/Settings");
            _button_YourShip.LoadContent(content, "MainMenu-Items/Your Ship");

            //set positions in order to their texture and the other buttons
            _button_YourShip.Position = new Vector2(center.X -(_button_YourShip.Texture.Width/2), center.Y-100);
            _button_Settings.Position = new Vector2(_button_YourShip.Position.X, _button_YourShip.Position.Y + 50);
            _button_Play.Position = new Vector2(_button_Settings.Position.X, _button_Settings.Position.Y + 50);
            _menuMusic = content.Load<Song>(menuMusicName);
            gameTitle = content.Load<SpriteFont>("SpriteFonts/GAME-TITLE");
            

        }

        public void Update(GameTime gameTime)
        {
            if (!MusicStarted)
            {
                MediaPlayer.Play(_menuMusic);
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.IsRepeating = true;
                MusicStarted = true;
            }

            Button_YourShip_Update(gameTime);
            Button_Settings_Update(gameTime);
            Button_Play_Update(gameTime);
            ResumeGame();
            //bool oldContains = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), Color.White);
            spriteBatch.DrawString(gameTitle, text_GameTitle, new Vector2(Game1.windowWidth/2 - 250, 50), Color.DarkSeaGreen);

            _button_YourShip.Draw(spriteBatch);
            _button_Settings.Draw(spriteBatch);
            _button_Play.Draw(spriteBatch);

        }
        
        private void ResumeGame()
        {
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
            KeyboardState state = Keyboard.GetState();

            if (!state.IsKeyDown(Keys.P))
            {
                oldState = state;
            }
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit();*/

            if (oldState != state && state.IsKeyDown(Keys.P))
            {
                Game1._gameState = Game1.GameStates.Playing;
                MediaPlayer.Stop();
                MusicStarted = false;
                oldState = state;

            }
        }

        private void Button_Play_Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            MouseState oldState = Mouse.GetState();
            if (_button_Play.Rec.Contains(state.X, state.Y))
            {
                _button_Play.Moving = true;
                // oldContains = true;
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    _button_Play.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                if (state.LeftButton == ButtonState.Pressed)
                {
                    _button_Play.UpdateOnceToLeft(gameTime);
                    oldState = state;
                    MediaPlayer.Stop();
                    MusicStarted = false;
                    Game1._gameState = Game1.GameStates.Playing;
                }
                /* if(state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
                 { 
                     _buttonResume.UpdateOnceToRight(gameTime);
                     oldState = state;
                 }*/
            }
            if (!_button_Play.Rec.Contains(state.X, state.Y))
            {
                _button_Play.UpdateOnceToLeft(gameTime);
                // _buttonResume.Moving = false;
            }
        }

        private void Button_Settings_Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            MouseState oldState = Mouse.GetState();
            if (_button_Settings.Rec.Contains(state.X, state.Y))
            {
                _button_Settings.Moving = true;
                // oldContains = true;
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    _button_Settings.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                if (state.LeftButton == ButtonState.Pressed)
                {
                    _button_Settings.UpdateOnceToLeft(gameTime);
                    oldState = state;
                    MediaPlayer.Stop();
                    MusicStarted = false;
                    Game1._gameState = Game1.GameStates.Playing;
                }
                /* if(state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
                 { 
                     _buttonResume.UpdateOnceToRight(gameTime);
                     oldState = state;
                 }*/
            }
            if (!_button_Settings.Rec.Contains(state.X, state.Y))
            {
                _button_Settings.UpdateOnceToLeft(gameTime);
                // _buttonResume.Moving = false;
            }
        }

        private void Button_YourShip_Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            MouseState oldState = Mouse.GetState();
            if (_button_YourShip.Rec.Contains(state.X, state.Y))
            {
                _button_YourShip.Moving = true;
                // oldContains = true;
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    _button_YourShip.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                if (state.LeftButton == ButtonState.Pressed)
                {
                    _button_YourShip.UpdateOnceToLeft(gameTime);
                    oldState = state;
                    MediaPlayer.Stop();
                    MusicStarted = false;
                    Game1._gameState = Game1.GameStates.Playing;
                }
                /* if(state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
                 { 
                     _buttonResume.UpdateOnceToRight(gameTime);
                     oldState = state;
                 }*/
            }
            if (!_button_YourShip.Rec.Contains(state.X, state.Y))
            {
                _button_YourShip.UpdateOnceToLeft(gameTime);
                // _buttonResume.Moving = false;
            }
        }

    }
}
