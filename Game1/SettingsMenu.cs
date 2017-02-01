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

        private Sprite _soundTexture;
        public Sprite SoundTexture
        {
            get { return _soundTexture; }
            set { _soundTexture = value; }
        }
        private Sprite _musicTexture;
        public Sprite MusicTexture
        {
            get { return _musicTexture; }
            set { _musicTexture = value; }
        }
        private Button _button_MusicOnOff;
        public Button Button_MusicOnOff
        {
            get { return _button_MusicOnOff; }
            set { _button_MusicOnOff = value; }
        }
        private Button _button_SoundOnOff;
        public Button Button_SoundOnOff
        {
            get { return _button_SoundOnOff; }
            set { _button_SoundOnOff = value; }
        }

        public GraphicMusicManager _musicComponent { get; set; }


        bool oldMusicActive;
        bool oldSoundActive;

        Vector2 center;
        Media _media;
        public SettingsMenu(Game game, Media media)
        {
            center = new Vector2(Settings._WindowWidth / 2, Settings._WindowHeight / 2);
            _musicComponent = new GraphicMusicManager(game, new Vector2(center.X-100,center.Y-100));

            _button_MainMenu = new Button(game);
            _button_VolumePlus = new Button(game);
            _button_VolumeMoins = new Button(game);
            _button_Difficulty = new Button(game);
            _button_MusicOnOff = new Button(game);
            _button_SoundOnOff = new Button(game);
            _musicTexture = new Sprite(game);
            _soundTexture = new Sprite(game);
            _volumeBar = new Animation(game, 2, 4, 1);
            _difficultyBar = new Animation(game, 1, 2, 1);
            _media = media;



            _volumeBar.Active = true;
            _difficultyBar.Active = true;
            _soundTexture.Active = true;
            _musicTexture.Active = true;

        }

        public void Initialize()
        {


        }

        public void LoadContent(ContentManager content, string menuMusicName = "")
        {
            _musicComponent.LoadContent(content);
            /* panel = content.Load<Texture2D>("PauseMenu-Items/Panel");
             _button_Resume.LoadContent(content, "PauseMenu-Items/Resume");*/
            _backGround = content.Load<Texture2D>("SettingsMenu-Items/Background-Settings");
            _button_MainMenu.LoadContent(content, "PauseMenu-Items/MainMenu");
            _button_VolumePlus.LoadContent(content, "SettingsMenu-Items/SoundPlus");
            _volumeBar.LoadContent(content, "SettingsMenu-Items/SoundVolumeBar");
            _button_VolumeMoins.LoadContent(content, "SettingsMenu-Items/SoundMoins");
            _button_MusicOnOff.LoadContent(content, "SettingsMenu-Items/OnOff");
            _button_SoundOnOff.LoadContent(content, "SettingsMenu-Items/OnOff");

            _soundTexture.LoadContent(content, "SettingsMenu-Items/SoundTexture");
            _musicTexture.LoadContent(content, "SettingsMenu-Items/MusicTexture");
            /* _button_Difficulty.LoadContent(content, "kfkfkf");
             _difficultyBar.LoadContent(content, "kkgkgk");*/


            _button_VolumeMoins.Texture.Position = new Vector2(center.X - (_button_VolumePlus.Texture.Width + 5), center.Y - 100);
            _volumeBar.Position = new Vector2(center.X + 10, _button_VolumeMoins.Texture.Position.Y);
            _button_VolumePlus.Texture.Position = new Vector2(_volumeBar.Position.X + _volumeBar.Width + 5, _button_VolumeMoins.Texture.Position.Y);

            _button_MusicOnOff.Texture.Position = new Vector2(_button_VolumeMoins.Texture.Position.X + 10, _button_VolumeMoins.Texture.Position.Y + 200);
            _button_SoundOnOff.Texture.Position = new Vector2(_button_MusicOnOff.Texture.Position.X + _button_MusicOnOff.Texture.Width + _soundTexture.Texture.Width + 30, _button_MusicOnOff.Texture.Position.Y);
            
            /* _button_Difficulty.Texture.Position = new Vector2(_button_VolumeMoins.Texture.Position.X, _button_VolumeMoins.Texture.Position.Y + 150);
            _difficultyBar.Position = new Vector2(_volumeBar.Position.X, _volumeBar.Position.Y + 150);*/
            _button_MainMenu.Texture.Position = new Vector2(10, Settings._WindowHeight - (_button_MainMenu.Texture.Height + 10));
            /*_button_Resume.Texture.Position = new Vector2(center.X - (_button_Resume.Texture.Width + 5), center.Y - 30);
           
            _menuMusic = content.Load<Song>(menuMusicName);*/
        }

        public void Update(GameTime gameTime, Settings settings)
        {
            _musicComponent.Update();
            _button_VolumePlus.Update(gameTime);
            _button_VolumeMoins.Update(gameTime);
            _button_MusicOnOff.UpdateSimple(gameTime);
            _button_SoundOnOff.UpdateSimple(gameTime);
            _button_MainMenu.Update(gameTime);
            // _button_Difficulty.Update(gameTime);
            if (_button_VolumePlus.Clicked)
            {
                _volumeBar.UpdateOnceToRight(gameTime);
                Settings._VolumeSound += 0.1f;
                _button_VolumePlus.Clicked = false;
                //one frame by one frame to right
            }
            if (_button_VolumeMoins.Clicked)
            {
                _volumeBar.UpdateOnceToLeft(gameTime);
                Settings._VolumeSound -= 0.1f;
                _button_VolumeMoins.Clicked = false;
            }
            if (_volumeBar.CurrentFrame == _volumeBar.TotalFrames - 1)
            {
                _button_VolumePlus.Texture.Active = false;
                Settings._VolumeSound = 1;
            }
            else
            {
                _button_VolumePlus.Texture.Active = true;
            }
            if (_volumeBar.CurrentFrame == 0)
            {
                _button_VolumeMoins.Texture.Active = false;
                Settings._VolumeSound = 0.2f;
            }
            else
            {
                _button_VolumeMoins.Texture.Active = true;
            }


            ManageMusicSound_OnOffState();
            _media.Update();
            /* if (_button_Difficulty.Clicked)
             {
                 _difficultyBar.Update(gameTime);
             }*/
            //ResumeGame_ByKeyPress(state);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(panel, new Rectangle((int)center.X - 250, (int)center.Y - 250, 500, 500), Color.White);
            spriteBatch.Draw(_backGround, new Rectangle(0, 0, Settings._WindowWidth, Settings._WindowHeight), Color.White);

            _musicComponent.Draw(spriteBatch);
           /* spriteBatch.Draw(_musicTexture.Texture,
                             new Rectangle((int)_button_VolumeMoins.Texture.Position.X - (_musicTexture.Texture.Width + 5),
                                            (int)_button_VolumeMoins.Texture.Position.Y - 70,
                                            _musicTexture.Texture.Width - 25,
                                            _musicTexture.Texture.Height - 15),
                             Color.White);
            spriteBatch.Draw(_soundTexture.Texture,
                             new Rectangle((int)_button_VolumeMoins.Texture.Position.X - (_soundTexture.Texture.Width + 5),
                                            (int)_button_VolumeMoins.Texture.Position.Y + 7,
                                            _soundTexture.Texture.Width - 25,
                                            _soundTexture.Texture.Height - 15),
                             Color.White);
            

            spriteBatch.Draw(_musicTexture.Texture,
                             new Rectangle((int)_button_VolumeMoins.Texture.Position.X - (_musicTexture.Texture.Width + 5),
                                           (int)_button_VolumeMoins.Texture.Position.Y + 200,
                                           _musicTexture.Texture.Width - 25,
                                           _musicTexture.Texture.Height - 15),
                             Color.White);
            _button_VolumePlus.Draw(spriteBatch);
            _button_MusicOnOff.Draw(spriteBatch);
            spriteBatch.Draw(_soundTexture.Texture,
                             new Rectangle((int)_button_VolumeMoins.Texture.Position.X + (_button_MusicOnOff.Texture.Width + 20),
                                           (int)_button_VolumeMoins.Texture.Position.Y + 200,
                                            _soundTexture.Texture.Width - 25,
                                            _soundTexture.Texture.Height - 15),
                             Color.White);
            _button_SoundOnOff.Draw(spriteBatch);

            _button_VolumePlus.Draw(spriteBatch);
            _volumeBar.Draw(spriteBatch);
            _button_VolumeMoins.Draw(spriteBatch);*/


            //  _button_Difficulty.Draw(spriteBatch);
            //  _difficultyBar.Draw(spriteBatch);
            _button_MainMenu.Draw(spriteBatch);
            // _button_Resume.Draw(spriteBatch);
        }

         private void ManageMusicSound_OnOffState()
        {
            if (_button_MusicOnOff.Clicked)
            {
                oldMusicActive = Settings._MusicActive;
                if (Settings._MusicActive)
                {
                    Settings._MusicActive = false;
                }
                if (!oldMusicActive && !Settings._MusicActive)
                {
                    Settings._MusicActive = true;
                }
                _button_MusicOnOff.Clicked = false;
            }

            if (_button_SoundOnOff.Clicked)
            {
                oldSoundActive = Settings._SoundActive;
                if (Settings._SoundActive)
                {
                    Settings._SoundActive = false;
                }
                if (!oldSoundActive && !Settings._SoundActive)
                {
                    Settings._SoundActive = true;
                }
                _button_SoundOnOff.Clicked = false;
            }
        }
    }
}

