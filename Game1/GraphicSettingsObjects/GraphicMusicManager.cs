
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
    class GraphicMusicManager
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


        public GraphicMusicManager(Game game, Vector2 position)
        {
            _musicTexture = new Sprite(game, position);
            _containerTexture = new Sprite(game);
            _volumeBarTexture = new Sprite(game);
            _selectorTexture = new Sprite(game);

            _musicTexture.Active = true;
            _containerTexture.Active = true;
            _volumeBarTexture.Active = true;
            _selectorTexture.Active = true;
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

            _containerTexture.Position = new Vector2(_musicTexture.Position.X + (_musicTexture.Width + 20), _musicTexture.Position.Y);
            _volumeBarTexture.Position = new Vector2((_containerTexture.Position.X + _containerTexture.Width/2) - (_volumeBarTexture.Width / 2), _musicTexture.Position.Y);
            _selectorTexture.Position = new Vector2((_volumeBarTexture.Position.X - (_selectorTexture.Width / 2) + (Settings._VolumeMusic * 10)), _musicTexture.Position.Y);

        }

        public void Update()
        {
            MouseState state = Mouse.GetState();
            bool contains = _selectorTexture.Rec.Contains(state.X, state.Y);

            if (contains && state.LeftButton == ButtonState.Pressed)
            {
                if(_selectorTexture.Rec.Center.ToVector2().X > _volumeBarTexture.Position.X || _selectorTexture.Rec.Center.ToVector2().X < (_volumeBarTexture.Position.X+ _volumeBarTexture.Width))
                {
                    _selectorTexture.Position = new Vector2(state.X, _selectorTexture.Position.Y);
                }
               
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(_containerTexture.Texture, new Rectangle((int)_containerTexture.Position.X,
                                                              (int)_containerTexture.Position.Y,
                                                              _containerTexture.Width,
                                                              _containerTexture.Height), 
                             Color.White);
            spriteBatch.Draw(_volumeBarTexture.Texture, new Rectangle((int)_volumeBarTexture.Position.X,
                                                                      (int)_volumeBarTexture.Position.X,
                                                                      _volumeBarTexture.Width,
                                                                      _volumeBarTexture.Width),
                             Color.White);
            spriteBatch.Draw*/
            _musicTexture.Draw(spriteBatch);
            _containerTexture.Draw(spriteBatch);
            // this tecture is drew in order to the selector position
            spriteBatch.Draw(_volumeBarTexture.Texture, new Rectangle((int)_volumeBarTexture.Position.X,
                                                                      (int)_volumeBarTexture.Position.X,
                                                                      _volumeBarTexture.Width,
                                                                      _volumeBarTexture.Width),
                             new Rectangle(0,0,(int)(_selectorTexture.Position.X-_volumeBarTexture.Position.X), _volumeBarTexture.Height ),
                             Color.White);
            _selectorTexture.Draw(spriteBatch);

        }


    }
}
