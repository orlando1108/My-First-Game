using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class GraphicSoundVolumeManager
    {
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
        private Animation _volumeBar;
        public Animation VolumeBar
        {
            get { return _volumeBar; }
            set { _volumeBar = value; }
        }
        private Sprite _soundTexture;
        public Sprite SoundTexture
        {
            get { return _soundTexture; }
            set { _soundTexture = value; }
        }

        public GraphicSoundVolumeManager(Game game)
        {
            _button_VolumePlus = new Button(game);
            _button_VolumeMoins = new Button(game);
            _soundTexture = new Sprite(game);
            _volumeBar = new Animation(game, 2, 4, 1);

            _soundTexture.Active = true;
            _volumeBar.Active = true;
        }

        public void LoadContent(ContentManager content)
        {
            _button_VolumePlus.LoadContent(content, "SettingsMenu-Items/SoundPlus");
            _volumeBar.LoadContent(content, "SettingsMenu-Items/SoundVolumeBar");
            _button_VolumeMoins.LoadContent(content, "SettingsMenu-Items/SoundMoins");
            _soundTexture.LoadContent(content, "SettingsMenu-Items/SoundTexture");

            _button_VolumeMoins.Texture.Position = new Vector2(center.X - (_button_VolumePlus.Texture.Width + 5), center.Y - 100);
            _volumeBar.Position = new Vector2(center.X + 10, _button_VolumeMoins.Texture.Position.Y);
            _button_VolumePlus.Texture.Position = new Vector2(_volumeBar.Position.X + _volumeBar.Width + 5, _button_VolumeMoins.Texture.Position.Y);

        }

        public void Update(GameTime gameTime)
        {
            _button_VolumePlus.Update(gameTime);
            _button_VolumeMoins.Update(gameTime);

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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(_soundTexture.Texture,
                              new Rectangle((int)_button_VolumeMoins.Texture.Position.X - (_soundTexture.Texture.Width + 5),
                                             (int)_button_VolumeMoins.Texture.Position.Y + 7,
                                             _soundTexture.Texture.Width - 25,
                                             _soundTexture.Texture.Height - 15),
                              Color.White);


             
             _button_VolumePlus.Draw(spriteBatch);
             
                              new Rectangle((int)_button_VolumeMoins.Texture.Position.X + (_button_MusicOnOff.Texture.Width + 20),
                                            (int)_button_VolumeMoins.Texture.Position.Y + 200,
                                             _soundTexture.Texture.Width - 25,
                                             _soundTexture.Texture.Height - 15),
                              Color.White);
            // _button_VolumePlus.Draw(spriteBatch);
             _volumeBar.Draw(spriteBatch);
             _button_VolumeMoins.Draw(spriteBatch);
        }
    }
}
