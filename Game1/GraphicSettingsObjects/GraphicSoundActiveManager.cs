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
    class GraphicSoundActiveManager
    {
        private Button _button_SoundOnOff;
        public Button Button_SoundcOnOff
        {
            get { return _button_SoundOnOff; }
            set { _button_SoundOnOff = value; }
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

        private bool _newSoundActive;
        public bool NewSoundActive
        {
            get { return _newSoundActive; }
            set { _newSoundActive = value; }
        }

        bool oldSoundActive;

        public GraphicSoundActiveManager(Game game, Vector2 position)
        {
            _soundTexture = new Sprite(game);
            _button_SoundOnOff = new Button(game);
            _soundTexture.Active = true;
            _position = position;
            _newSoundActive = true;
        }

        public void LoadContent(ContentManager content)
        {
            _button_SoundOnOff.LoadContent(content, "SettingsMenu-Items/OnOff");
            _soundTexture.LoadContent(content, "SettingsMenu-Items/SoundTexture");
            _soundTexture.Position = _position;
            _button_SoundOnOff.Texture.Position = new Vector2(_soundTexture.Position.X + 250, _position.Y);
        }

        public void Update(GameTime gameTime)
        {
            bool currentSoundActive = Settings._SoundActive;
           _button_SoundOnOff.UpdateSimple(gameTime);

            if (_button_SoundOnOff.Clicked)
            {
                oldSoundActive = currentSoundActive;
                if (currentSoundActive)
                {
                    Settings._SoundActive = false;
                }
                if (!oldSoundActive && !currentSoundActive)
                {
                    Settings._SoundActive = true;
                }
                _button_SoundOnOff.Clicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _soundTexture.Draw(spriteBatch);
            _button_SoundOnOff.Draw(spriteBatch);

        }
    }
}
