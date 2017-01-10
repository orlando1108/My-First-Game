using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class SettingsMenu
    {
        private Texture2D _backGround;
        public Texture2D BackGround
        {
            get { return _backGround; }
            set { _backGround = value; }
        }
        private Button _button_MainMenu;
        public Button Button_MainMenu
        {
            get { return _button_MainMenu; }
            set { _button_MainMenu = value; }
        }
        private Button _button_VolumePlus;
        public Button Button_VolumePlus
        {
            get { return _button_VolumePlus; }
            set { _button_VolumePlus = value; }
        }
        private Button _button_VolumeMoins;
        public Button Button_VolumeMoins
        {
            get { return _button_VolumeMoins; }
            set { _button_VolumeMoins = value; }
        }
        private Button _button_Difficulty;
        public Button Button_Difficulty
        {
            get { return _button_Difficulty; }
            set { _button_Difficulty = value; }
        }
        private Animation _volumeBar;
        public Animation VolumeBar
        {
            get { return _volumeBar; }
            set { _volumeBar = value; }
        }
        private Animation _difficultyBar;
        public Animation DifficultyBar
        {
            get { return _difficultyBar; }
            set { _difficultyBar = value; }
        }

        private Texture2D _soundTexture;
        public Texture2D SoundTexture
        {
            get { return _soundTexture; }
            set { _soundTexture = value; }
        }



        Vector2 center;

        public SettingsMenu(Game game)
        {
            center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
           
            _button_MainMenu = new Button(game);
            _button_VolumePlus = new Button(game);
            _button_VolumeMoins = new Button(game);
            _button_Difficulty = new Button(game);
            _volumeBar = new Animation(game, 2, 4, 1);
            _difficultyBar = new Animation(game, 1, 2, 1);

            

            _volumeBar.Active = true;
            _difficultyBar.Active = true;

        }

        public void Initialize()
        {
            

        }

        public void LoadContent(ContentManager content, string menuMusicName ="")
        {
            /* panel = content.Load<Texture2D>("PauseMenu-Items/Panel");
             _button_Resume.LoadContent(content, "PauseMenu-Items/Resume");*/
            _backGround = content.Load<Texture2D>("SettingsMenu-Items/Background-Settings");
            _button_MainMenu.LoadContent(content, "PauseMenu-Items/MainMenu");
            _button_VolumePlus.LoadContent(content, "SettingsMenu-Items/Plus");
            _volumeBar.LoadContent(content, "SettingsMenu-Items/VolumeBar");
            _button_VolumeMoins.LoadContent(content, "SettingsMenu-Items/Moins");

            _soundTexture = content.Load<Texture2D>("SettingsMenu-Items/Music");
           /* _button_Difficulty.LoadContent(content, "kfkfkf");
            _difficultyBar.LoadContent(content, "kkgkgk");*/
           

            _button_VolumeMoins.Texture.Position = new Vector2(center.X - (_button_VolumePlus.Texture.Width +10), center.Y - 100);
            _volumeBar.Position = new Vector2(center.X + 10, _button_VolumeMoins.Texture.Position.Y);
            _button_VolumePlus.Texture.Position = new Vector2(_volumeBar.Position.X + _volumeBar.Width + 10, _button_VolumeMoins.Texture.Position.Y);
            _button_Difficulty.Texture.Position = new Vector2(_button_VolumeMoins.Texture.Position.X, _button_VolumeMoins.Texture.Position.Y + 150);
            _difficultyBar.Position = new Vector2(_volumeBar.Position.X, _volumeBar.Position.Y + 150);
            _button_MainMenu.Texture.Position = new Vector2(10, Game1.windowHeight - (_button_MainMenu.Texture.Height + 10));
            /*_button_Resume.Texture.Position = new Vector2(center.X - (_button_Resume.Texture.Width + 5), center.Y - 30);
           
            _menuMusic = content.Load<Song>(menuMusicName);*/
        }

        public void Update(GameTime gameTime)
        {
            /*KeyboardState state = Keyboard.GetState();

            if (!MusicStarted)
            {
                MediaPlayer.Play(_menuMusic);
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.IsRepeating = true;
                MusicStarted = true;
            }
           */
            _button_VolumePlus.Update(gameTime);
            _button_VolumeMoins.Update(gameTime);
           // _button_Difficulty.Update(gameTime);
            if (_button_VolumePlus.Clicked)
            {
                _volumeBar.UpdateOnceToRight(gameTime);
                _button_VolumePlus.Clicked = false;
                //one frame by one frame to right
            }
            if (_button_VolumeMoins.Clicked)
            {
                _volumeBar.UpdateOnceToLeft(gameTime);
                _button_VolumeMoins.Clicked = false;
            }

            if (_volumeBar.CurrentFrame == _volumeBar.TotalFrames-1)
            {
                _button_VolumePlus.Texture.Active = false;
            }else
            {
                _button_VolumePlus.Texture.Active = true;
            }

            if(_volumeBar.CurrentFrame == 0)
            {
                _button_VolumeMoins.Texture.Active = false;
            }else
            {
                _button_VolumeMoins.Texture.Active = true;
            }
           /* if (_button_Difficulty.Clicked)
            {
                _difficultyBar.Update(gameTime);
            }*/
            
            _button_MainMenu.Update(gameTime);

           
            /*if (_button_MainMenu.Clicked == true)
            {
                //MainMenu.MusicStarted = true;
                _button_MainMenu.Clicked = false;
               // MusicStarted = false;
                Game1._gameState = Game1.GameStates.Loading;
            }*/
            //ResumeGame_ByKeyPress(state);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(panel, new Rectangle((int)center.X - 250, (int)center.Y - 250, 500, 500), Color.White);
            spriteBatch.Draw(_backGround, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), Color.White);
            spriteBatch.Draw(SoundTexture, new Rectangle((int)_button_VolumeMoins.Texture.Position.X - (SoundTexture.Width + 5), (int)_button_VolumeMoins.Texture.Position.Y+7,_soundTexture.Width-25,_soundTexture.Height-15),Color.White);
            _button_VolumePlus.Draw(spriteBatch);
            _volumeBar.Draw(spriteBatch);
            _button_VolumeMoins.Draw(spriteBatch);
          //  _button_Difficulty.Draw(spriteBatch);
           
          //  _difficultyBar.Draw(spriteBatch);
            _button_MainMenu.Draw(spriteBatch);

           // _button_Resume.Draw(spriteBatch);

        }


        /*private void ResumeGame_ByKeyPress(KeyboardState state)
        {
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed


            if (!state.IsKeyDown(Keys.P))
            {
                pauseKey_OldState = false;
            }
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit();*/

         /*   if (pauseKey_OldState == false && state.IsKeyDown(Keys.P))
            {
                Game1._gameState = Game1.GameStates.Playing;
                MediaPlayer.Stop();
                MusicStarted = false;
                pauseKey_OldState = true;

            }
        }*/
    }
}
