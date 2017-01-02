using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;



namespace SpaceShooter
{
    class PauseMenu
    {
        private Animation _button_Resume;
        public Animation Button_Resume
        {
            get { return _button_Resume; }
            set { _button_Resume = value; }
        }

        private Animation _button_MainMenu;
        public Animation Button_MainMenu
        {
            get { return _button_MainMenu; }
            set { _button_MainMenu = value; }
        }

        private Song _menuMusic;
        public Song MenuMusic
        {
            get {
                return _menuMusic; }
            set { _menuMusic = value; }
        }

        public KeyboardState oldState;
        private Texture2D panel;
        bool MusicStarted = false;
        Vector2 center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
       

        public PauseMenu(Game game)
        {
            _button_Resume = new Animation(game, 1, 2, 1);
            _button_MainMenu = new Animation(game, 1, 2, 1);
            
            _button_Resume.Active = true;
            _button_MainMenu.Active = true;
           // _buttonResume.Moving = false;
        }

        public void Initialize()
        {
           // _buttonResume.UnloadContent();

        }

        public void LoadContent(ContentManager content, String menuMusicName)
        {
            panel = content.Load<Texture2D>("PauseMenu-Items/Panel");
            _button_Resume.LoadContent(content, "PauseMenu-Items/Resume");
            _button_MainMenu.LoadContent(content, "PauseMenu-Items/Main Menu");

            _button_Resume.Position = new Vector2(center.X - (_button_Resume.Width+5), center.Y - 30);
            _button_MainMenu.Position = new Vector2(center.X + 5, _button_Resume.Position.Y);
            _menuMusic = content.Load<Song>(menuMusicName);
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

            Button_Resume_Update(gameTime);
            Button_MainMenu_Update(gameTime);
            ResumeGame();
            //bool oldContains = false;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(panel, new Rectangle((int)center.X - 250, (int)center.Y - 250, 500, 500), Color.White);
            _button_Resume.Draw(spriteBatch);
            _button_MainMenu.Draw(spriteBatch);
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

        private void Button_Resume_Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            MouseState oldState = Mouse.GetState();
            if (_button_Resume.Rec.Contains(state.X, state.Y))
            {
                _button_Resume.Moving = true;
                // oldContains = true;
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    _button_Resume.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                if (state.LeftButton == ButtonState.Pressed)
                {
                    _button_Resume.UpdateOnceToLeft(gameTime);
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
            if (!_button_Resume.Rec.Contains(state.X, state.Y))
            {
                _button_Resume.UpdateOnceToLeft(gameTime);
                // _buttonResume.Moving = false;
            }
        }

        private void Button_MainMenu_Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            MouseState oldState = Mouse.GetState();
            if (_button_MainMenu.Rec.Contains(state.X, state.Y))
            {
                _button_MainMenu.Moving = true;
                // oldContains = true;
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    _button_MainMenu.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                if (state.LeftButton == ButtonState.Pressed)
                {
                    _button_MainMenu.UpdateOnceToLeft(gameTime);
                    oldState = state;
                    MainMenu.MusicStarted = true;
                    Game1._gameState = Game1.GameStates.Loading;
                }
                /* if(state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
                 { 
                     _buttonResume.UpdateOnceToRight(gameTime);
                     oldState = state;
                 }*/
            }
            if (!_button_MainMenu.Rec.Contains(state.X, state.Y))
            {
                _button_MainMenu.UpdateOnceToLeft(gameTime);
                // _buttonResume.Moving = false;
            }
        }
    }
}
