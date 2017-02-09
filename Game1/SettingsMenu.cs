using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private Button _button_SoundOnOff;
        public Button Button_SoundOnOff
        {
            get { return _button_SoundOnOff; }
            set { _button_SoundOnOff = value; }
        }
        private Animation _difficultyBar;
        public Animation DifficultyBar
        {
            get { return _difficultyBar; }
            set { _difficultyBar = value; }
        }
       

        public GraphicMusicVolumeManager _musicVolumeComponent { get; set; }
        public GraphicSoundVolumeManager _soundVolumeComponent { get; set; }
        public GraphicMusicActiveManager _musicActiveComponent { get; set; }
        public GraphicSoundActiveManager _soundActiveComponent { get; set; }
        Vector2 center;
        Media _media;

        public SettingsMenu(Game game, Media media)
        {
            center = new Vector2(Settings._WindowWidth / 2, Settings._WindowHeight / 2);
            _musicVolumeComponent = new GraphicMusicVolumeManager(game, new Vector2(center.X - 220, center.Y-150));
            _soundVolumeComponent = new GraphicSoundVolumeManager(game, new Vector2(center.X - 220, _musicVolumeComponent.Position.Y + 80));
            _musicActiveComponent = new GraphicMusicActiveManager(game, new Vector2(center.X - 220, _soundVolumeComponent.Position.Y + 80));
            _soundActiveComponent = new GraphicSoundActiveManager(game, new Vector2(center.X - 220, _musicActiveComponent.Position.Y + 80));
            _button_MainMenu = new Button(game);
            //_button_Difficulty = new Button(game);
           // _difficultyBar = new Animation(game, 1, 2, 1);*/
            _media = media;
           // _difficultyBar.Active = true;
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content, string menuMusicName = "")
        {
            _backGround = content.Load<Texture2D>("SettingsMenu-Items/Background-Settings");
            _button_MainMenu.LoadContent(content, "PauseMenu-Items/MainMenu");
            _musicVolumeComponent.LoadContent(content);
            _soundVolumeComponent.LoadContent(content);
            _musicActiveComponent.LoadContent(content);
            _soundActiveComponent.LoadContent(content);
            
            _button_MainMenu.Texture.Position = new Vector2(10, Settings._WindowHeight - (_button_MainMenu.Texture.Height + 10)); //bug
            // panel = content.Load<Texture2D>("PauseMenu-Items/Panel");
         
         
           /* _button_Difficulty.LoadContent(content, "kfkfkf");
            _difficultyBar.LoadContent(content, "kkgkgk");*/
            // _button_Difficulty.Texture.Position = new Vector2(_button_VolumeMoins.Texture.Position.X, _button_VolumeMoins.Texture.Position.Y + 150);
            // _difficultyBar.Position = new Vector2(_volumeBar.Position.X, _volumeBar.Position.Y + 150);*/
            /*_button_Resume.Texture.Position = new Vector2(center.X - (_button_Resume.Texture.Width + 5), center.Y - 30);
            _menuMusic = content.Load<Song>(menuMusicName);*/
        }

        public void Update(GameTime gameTime)
        {
            _musicVolumeComponent.Update(gameTime);
            _soundVolumeComponent.Update(gameTime);
            _musicActiveComponent.Update(gameTime);
            _soundActiveComponent.Update(gameTime);
            _button_MainMenu.Update(gameTime);
          
            // _button_Difficulty.Update(gameTime);
            // ManageMusicSound_OnOffState();
            /* if (_button_Difficulty.Clicked)
             {
                 _difficultyBar.Update(gameTime);
             }*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backGround, new Rectangle(0, 0, Settings._WindowWidth, Settings._WindowHeight), Color.White);
            _musicVolumeComponent.Draw(spriteBatch);
            _soundVolumeComponent.Draw(spriteBatch);
            _musicActiveComponent.Draw(spriteBatch);
            _soundActiveComponent.Draw(spriteBatch);
            
            _button_MainMenu.Draw(spriteBatch);
            // spriteBatch.Draw(panel, new Rectangle((int)center.X - 250, (int)center.Y - 250, 500, 500), Color.White);
            //  _button_Difficulty.Draw(spriteBatch);
            //  _difficultyBar.Draw(spriteBatch);
        }
    }
}

