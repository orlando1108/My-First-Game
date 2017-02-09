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
    class GraphicMusicActiveManager
    {
        private Button _button_MusicOnOff;
        public Button Button_MusicOnOff
        {
            get { return _button_MusicOnOff; }
            set { _button_MusicOnOff = value; }
        }

        private Sprite _musicTexture;
        public Sprite MusicTexture
        {
            get { return _musicTexture; }
            set { _musicTexture = value; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private bool _newMusicActive;
        public bool NewMusicActive
        {
            get { return _newMusicActive; }
            set { _newMusicActive = value; }
        }


        bool oldMusicActive;

        public GraphicMusicActiveManager(Game game, Vector2 position)
        {
            _musicTexture = new Sprite(game);
            _button_MusicOnOff = new Button(game);
            _musicTexture.Active = true;
            _position = position;
            _newMusicActive = true;
        }

        public void LoadContent(ContentManager content)
        {
            _button_MusicOnOff.LoadContent(content, "SettingsMenu-Items/OnOff");
            _musicTexture.LoadContent(content, "SettingsMenu-Items/MusicTexture");
            _musicTexture.Position = _position;
            _button_MusicOnOff.Texture.Position = new Vector2(_musicTexture.Position.X + 250, _position.Y);
        }

        public void Update(GameTime gameTime)
        {
            bool currentSoundActive = Settings._MusicActive;
            _button_MusicOnOff.UpdateSimple(gameTime);

            if (_button_MusicOnOff.Clicked)
            {
                oldMusicActive = currentSoundActive;
                if (currentSoundActive)
                {
                    Settings._MusicActive = false;
                }
                if (!oldMusicActive && !currentSoundActive)
                {
                    Settings._MusicActive = true;
                }
                _button_MusicOnOff.Clicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _musicTexture.Draw(spriteBatch);
            _button_MusicOnOff.Draw(spriteBatch);

        }
    }
}
