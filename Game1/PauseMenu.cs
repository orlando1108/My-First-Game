using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;



namespace SpaceShooter
{
    class PauseMenu
    {
        private Button _button_Resume;
        public Button Button_Resume
        {
            get { return _button_Resume; }
            set { _button_Resume = value; }
        }

        private Button _button_MainMenu;
        public Button Button_MainMenu
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

        public bool pauseKey_OldState;
        private Texture2D panel;
        private bool MusicStarted = false;
        

        Vector2 center;
       

        public PauseMenu(Game game)
        {
            center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
            _button_Resume = new Button(game);
            _button_MainMenu = new Button(game);
            pauseKey_OldState = false;

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
            _button_MainMenu.LoadContent(content, "PauseMenu-Items/MainMenu");
            
            _button_Resume.Texture.Position = new Vector2(center.X - (_button_Resume.Texture.Width + 5), center.Y - 30);
            _button_MainMenu.Texture.Position = new Vector2(center.X + 5, _button_Resume.Texture.Position.Y);
            _menuMusic = content.Load<Song>(menuMusicName);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (!MusicStarted)
            {
                MediaPlayer.Play(_menuMusic);
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.IsRepeating = true;
                MusicStarted = true;
            }

            _button_Resume.Update(gameTime);
            _button_MainMenu.Update(gameTime);
            
           if(_button_Resume.Clicked == true)
            {
                MediaPlayer.Stop();
                MusicStarted = false;
                pauseKey_OldState = true;
                Game1.pauseKey_OldState = true;
                _button_Resume.Clicked = false;
                Game1._gameState = Game1.GameStates.Playing;
            }
           if(_button_MainMenu.Clicked == true)
            {
                MainMenu.MusicStarted = true;
                _button_MainMenu.Clicked = false;
                MusicStarted = false;
                Game1._gameState = Game1.GameStates.Loading;
            }
            ResumeGame_ByKeyPress(state);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(panel, new Rectangle((int)center.X - 250, (int)center.Y - 250, 500, 500), Color.White);
            _button_MainMenu.Draw(spriteBatch);
            _button_Resume.Draw(spriteBatch);
           
        }


        private void ResumeGame_ByKeyPress(KeyboardState state)
        {
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
            
            
            if (!state.IsKeyDown(Keys.P))
            {
                pauseKey_OldState = false;
            }
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit();*/

            if (pauseKey_OldState == false && state.IsKeyDown(Keys.P))
            {
                Game1._gameState = Game1.GameStates.Playing;
                MediaPlayer.Stop();
                MusicStarted = false;
                pauseKey_OldState = true;
                
            }
        }
    }
}
