
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class GraphicMusicVolumeManager
    {
        private Sprite _containerTexture;
        public Sprite ContainerTexture
        {
            get { return _containerTexture; }
            set { _containerTexture = value; }
        }

        private Sprite _volumeBarTexture;
        public Sprite VolumeBarTexture
        {
            get { return _volumeBarTexture; }
            set { _volumeBarTexture = value; }
        }

        private Sprite _selectorTexture;
        public Sprite SelectorTexture
        {
            get { return _selectorTexture; }
            set { _selectorTexture = value; }
        }

        private Sprite _musicTexture;
        public Sprite MusicTexture
        {
            get { return _musicTexture; }
            set { _musicTexture = value; }
        }

        private bool _isChanged;
        public bool IsChanged
        {
            get { return _isChanged; }
            set { _isChanged = value; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public GraphicMusicVolumeManager(Game game, Vector2 position)
        {
            
            _musicTexture = new Sprite(game);
            _containerTexture = new Sprite(game);
            _volumeBarTexture = new Sprite(game);
            _selectorTexture = new Sprite(game);

            _musicTexture.Active = true;
            _containerTexture.Active = true;
            _volumeBarTexture.Active = true;
            _selectorTexture.Active = true;
            _isChanged = false;
            _position = position;
        }

        public void UnloadContent()
        {
            _containerTexture.UnloadContent();
            _volumeBarTexture.UnloadContent();
            _selectorTexture.UnloadContent();
        }

        public void LoadContent(ContentManager content)
        {
            _musicTexture.LoadContent(content, "SettingsMenu-Items/MusicTexture");
            _containerTexture.LoadContent(content, "SettingsMenu-Items/MusicVolumeContainer");
            _volumeBarTexture.LoadContent(content, "SettingsMenu-Items/MusicVolumeBar");
            _selectorTexture.LoadContent(content, "SettingsMenu-Items/MusicVolumeSelector");

            _musicTexture.Position = _position;
            _containerTexture.Position = new Vector2(_musicTexture.Position.X + 250, _musicTexture.Position.Y + (_musicTexture.Height / 4));
            _volumeBarTexture.Position = new Vector2((_containerTexture.Position.X + _containerTexture.Width / 2) - (_volumeBarTexture.Width / 2), (_containerTexture.Position.Y + (_containerTexture.Height / 2) - (_volumeBarTexture.Height / 2)));
            _selectorTexture.Position = new Vector2((_volumeBarTexture.Position.X - (_selectorTexture.Width / 2) + Convert_Volume_ToSelectorPosition()), (_volumeBarTexture.Position.Y + (_volumeBarTexture.Height / 2)) - (_selectorTexture.Height / 2));


        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            bool contains = _selectorTexture.Rec.Contains(state.X, state.Y);
            bool isMouseLeftIn = state.X > _volumeBarTexture.Position.X;
            bool isMouseRightIn = state.X < _volumeBarTexture.Position.X + _volumeBarTexture.Texture.Width;
            _isChanged = contains && isMouseLeftIn && isMouseRightIn && state.LeftButton == ButtonState.Pressed;

            _selectorTexture.Update();

            if (_isChanged)
            {
                _selectorTexture.Position = new Vector2((state.X - (_selectorTexture.Width / 2)), _selectorTexture.Position.Y);
                _selectorTexture.Update();
                Settings._VolumeMusic = Convert_SelectorPosition_ToVolume();

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            _musicTexture.Draw(spriteBatch);
            _containerTexture.Draw(spriteBatch);

            spriteBatch.Draw(_volumeBarTexture.Texture, new Rectangle((int)_volumeBarTexture.Position.X,
                                                                      (int)_volumeBarTexture.Position.Y,
                                                                      (int)((_selectorTexture.Position.X + (_selectorTexture.Width / 2)) - _volumeBarTexture.Position.X),
                                                                      _volumeBarTexture.Height),
                             Color.White);
            _selectorTexture.Draw(spriteBatch);
            // this tecture is drew in order to the selector position
            /* spriteBatch.Draw(_volumeBarTexture.Texture, new Rectangle((int)_volumeBarTexture.Position.X,
                                                                       (int)_volumeBarTexture.Position.Y,
                                                                       _volumeBarTexture.Width,
                                                                       _volumeBarTexture.Height),
                              new Rectangle(0,0,(int)((_selectorTexture.Position.X + (_selectorTexture.Width/2))- _volumeBarTexture.Position.X), _volumeBarTexture.Height ),
                              Color.White);*/
        }

        public float Convert_SelectorPosition_ToVolume()
        {
            float value = (_selectorTexture.Position.X - _volumeBarTexture.Position.X);
            float maxVolumeValue = 1f;
            float newMusiqueVolume = (value * maxVolumeValue) / _volumeBarTexture.Width;

            return newMusiqueVolume;
        }

        public float Convert_Volume_ToSelectorPosition()
        {
            float value = Settings._VolumeMusic;
            float maxPosition = _volumeBarTexture.Width;
            float newSelectorPosition = (value * maxPosition) / 1f;

            return newSelectorPosition;
        }



    }
}
