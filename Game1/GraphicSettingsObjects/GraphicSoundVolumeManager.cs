using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
       
        private SoundEffect _testSoundEffectVolume;
        public SoundEffect TestSoundEffectVolume
        {
            get { return _testSoundEffectVolume; }
            set { _testSoundEffectVolume = value; }
        }


        public GraphicSoundVolumeManager(Game game, Vector2 position)
        {
            _soundTexture = new Sprite(game);
            _button_VolumePlus = new Button(game);
            _button_VolumeMoins = new Button(game);
            _volumeBar = new Animation(game, 2, 4, 1);

            _soundTexture.Active = true;
            _volumeBar.Active = true;
            _volumeBar.CurrentFrame = Convert_Volume_ToVolumeBarCurrentFrame();
            _position = position;
           
        }

        public void LoadContent(ContentManager content)
        {
            _button_VolumePlus.LoadContent(content, "SettingsMenu-Items/SoundPlus");
            _volumeBar.LoadContent(content, "SettingsMenu-Items/SoundVolumeBar");
            _button_VolumeMoins.LoadContent(content, "SettingsMenu-Items/SoundMoins");
            _soundTexture.LoadContent(content, "SettingsMenu-Items/SoundTexture");
            _testSoundEffectVolume = content.Load<SoundEffect>("Sounds-Musics/testSoundVolume");

            _soundTexture.Position = _position;
            _button_VolumeMoins.Texture.Position = new Vector2(_soundTexture.Position.X + 250, _soundTexture.Position.Y);
            _volumeBar.Position = new Vector2(_button_VolumeMoins.Texture.Position.X + _button_VolumeMoins.Texture.Width, _soundTexture.Position.Y);
            _button_VolumePlus.Texture.Position = new Vector2(_volumeBar.Position.X + _volumeBar.Width, _soundTexture.Position.Y);

        }

        public void Update(GameTime gameTime)
        {
            _button_VolumePlus.Update(gameTime);
            _button_VolumeMoins.Update(gameTime);

            if (_button_VolumePlus.Clicked)
            {
                _volumeBar.UpdateOnceToRight(gameTime);
                Settings._VolumeSound = Convert_VolumeBarCurrentFrame_ToVolume();
                Media.PlaySound(_testSoundEffectVolume);
                _button_VolumePlus.Clicked = false;
            }
            if (_button_VolumeMoins.Clicked)
            {
                _volumeBar.UpdateOnceToLeft(gameTime);
                Settings._VolumeSound = Convert_VolumeBarCurrentFrame_ToVolume();
                Media.PlaySound(_testSoundEffectVolume);
                _button_VolumeMoins.Clicked = false;
            }
            if (_volumeBar.CurrentFrame == _volumeBar.TotalFrames - 1)
            {
                _button_VolumePlus.Texture.Active = false;
            }
            else
            {
                _button_VolumePlus.Texture.Active = true;
            }
            if (_volumeBar.CurrentFrame == 0)
            {
                _button_VolumeMoins.Texture.Active = false;
            }
            else
            {
                _button_VolumeMoins.Texture.Active = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _soundTexture.Draw(spriteBatch);
            _button_VolumeMoins.Draw(spriteBatch);
            _volumeBar.Draw(spriteBatch);
            _button_VolumePlus.Draw(spriteBatch);
        }

        public float Convert_VolumeBarCurrentFrame_ToVolume()
        {
            float currentFramevalue = (_volumeBar.CurrentFrame + 1);
            float newSoundVolume = (1f / (_volumeBar.TotalFrames)) * currentFramevalue;

            return newSoundVolume;
        }

        public int Convert_Volume_ToVolumeBarCurrentFrame()
        {
            float value = Settings._VolumeMusic;
            float nbMaxFrames = _volumeBar.TotalFrames - 1;
            float newCurrentFrame = (value * nbMaxFrames) / 1f;

            return (int)newCurrentFrame;
        }
    }
}
