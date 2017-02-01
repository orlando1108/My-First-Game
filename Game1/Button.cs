using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    class Button
    {
        private Animation _texture;
        public Animation Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        private SoundEffectInstance _soundEffectInstance;
        public SoundEffectInstance SoundEffectInstance
        {
            get { return _soundEffectInstance; }
            set { _soundEffectInstance = value; }
        }

        private bool _clicked;
        public bool Clicked
        {
            get { return _clicked; }
            set { _clicked = value; }
        }
        private bool _alreadyClicked;
        public bool AlreadyClicked
        {
            get { return _alreadyClicked; }
            set { _alreadyClicked = value; }
        }


        private SoundEffect button_Soundeffect;
        bool soundEffectStarted;
        MouseState state;
        MouseState oldState;
        int nbClick = 0;

        public Button(Game game)
        {
            _texture = new Animation(game, 1, 2, 1);
            _texture.Active = true;
            _clicked = false;
            _alreadyClicked = false;
        }

        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content, String texture, String soundEffect = null)
        {
            _texture.LoadContent(content, texture);
            if (soundEffect != null)
            {
                button_Soundeffect = content.Load<SoundEffect>("Sounds-Musics/" + soundEffect);
            }
            else
            {
                button_Soundeffect = content.Load<SoundEffect>("Sounds-Musics/SoundButton_MouseHover");
            }
            _soundEffectInstance = button_Soundeffect.CreateInstance();
           
        }

        public void Update(GameTime gameTime)
        {
            oldState = state;
            state = Mouse.GetState();
            bool contains = _texture.Rec.Contains(state.X, state.Y);
            if (!contains)
            {
                soundEffectStarted = false;
            }
            if (contains)
            {
                if (!soundEffectStarted)
                {
                    _soundEffectInstance.Volume = Settings._VolumeSound;
                    _soundEffectInstance.Play();
                    soundEffectStarted = true;
                }
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    _texture.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
                {
                    _texture.UpdateOnceToLeft(gameTime);
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Pressed)
                {
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released)
                {
                    _texture.UpdateOnceToRight(gameTime);
                    _clicked = true;
                    oldState = state;
                }


            }
            else
            {
                _texture.UpdateOnceToLeft(gameTime);
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _texture.Draw(spriteBatch);
        }

        public void UpdateSimple(GameTime gameTime)
        {
            oldState = state;
            state = Mouse.GetState();
            bool Contains = _texture.Rec.Contains(state.X, state.Y);
           
            /*if (!Contains)
            {
                soundEffectStarted = false;
            }*/
            if (Contains)
            {
                /* if (!soundEffectStarted)
                 {
                     _soundEffectInstance.Play();
                     soundEffectStarted = true;
                 }*/
                if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    // _texture.UpdateOnceToRight(gameTime);
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
                {
                    // _texture.UpdateOnceToLeft(gameTime);
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Pressed)
                {
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released)
                {
                 
                    nbClick += 1;
                    if (_clicked == false)
                    {
                        _clicked = true;
                    }
                    if(nbClick <= 1 && _clicked == true)
                    {
                        _texture.UpdateOnceToRight(gameTime);
                    }
                   
                    if(nbClick > 1 && _clicked == true)
                    {
                        _texture.UpdateOnceToLeft(gameTime);
                        nbClick = 0;
                    }
                   
                }
            }
              else
              {
                
                _clicked = false;
              }
        }

    }
}
