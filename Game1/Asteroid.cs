using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;



namespace SpaceShooter
{
    class Asteroid : Sprite
    {
        
        Random rand = new Random();
        private int aleaTexture;
        private int aleaVitesse;
        private int sensRotation;
        private float aleaPositionX;
        
       // private bool _reverseUpdate;
        // private bool _isDisposed;

        private Animation _explosion;
        public Animation Explosion
        {
            get { return _explosion; }
            set { _explosion = value; }
        }

        private Animation _textureAsteroid;
        public Animation TextureAsteroid
        {
            get { return _textureAsteroid; }
            set { _textureAsteroid = value; }
        }
        
        private bool _finish;
        public bool Finish
        {
            get { return _finish; }
            set { _finish = value; }
        }

        /*private float _rotation;
         public float Rotation
         {
             get { return _rotation; }
             set { _rotation = value; }
         }*/

        private List<Asteroid> _asteroids;
        public List<Asteroid> Asteroids
        {
            get { return _asteroids; }
            set { _asteroids = value; }
        }
        
        public Asteroid(Game game): base(game)
        {
            aleaTexture = rand.Next(3);
            aleaVitesse = rand.Next(6);
            sensRotation = rand.Next(3);
            base._position.X = aleaPositionX;
            base._active = true;
            this._finish = false;
            this._explosion = new Animation(game, 4, 4, _speed,_position.X, 50); //lines then columns
            this._textureAsteroid = new Animation(game, 1, 24, 90);
            this._textureAsteroid.Active = true;
            this._textureAsteroid.RotationActive = true;
             //_reverseUpdate = true;
            //  _rotation = Rotation;

        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public void LoadContent(ContentManager content, String texture1, String texture2, String textureExplosion, int newSpeed)
        {
            if (aleaTexture % 2 == 0)
            {
                 _textureAsteroid.LoadContent(Content, texture1);
            }
            else
            {
                _textureAsteroid.LoadContent(Content, texture2);
            }

            _texture = _textureAsteroid.Texture;
            _textureAsteroid.Active = true;
            _textureAsteroid.Moving = true;
            
            aleaPositionX = rand.Next(Game1.windowWidth - _textureAsteroid.Width);
            _position = new Vector2(aleaPositionX, -_textureAsteroid.Height);
          
            
            if (aleaVitesse % 5 == 0)
            {
                _speed = new Vector2(0, newSpeed+2);
               
            }
            else
            {
                _speed = new Vector2(0, newSpeed);
            }
            _explosion.Moving = true;
            _explosion.LoadContent(content, textureExplosion);
           
        }

        public void Update(GameTime gameTime, Vaisseau v, List<Tir> t, SpriteBatch sb)
        {
            if (_active == true)
            {
                if (_position.Y > Game1.windowHeight)
                {
                    _active = false;
                }

                _rec = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _textureAsteroid.Width,
                _textureAsteroid.Height);

                _position = new Vector2(_position.X, _position.Y + _speed.Y);
                _textureAsteroid.Position = _position;
                _textureAsteroid.UpdateLimitLess_ToRight(gameTime);
            }
        else
        {
            _explosion.Active = true;
            _explosion.Position = _position;
            _explosion.UpdateOnceToRight(gameTime);
               
        }
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_active == true)
            {
                //spriteBatch.Draw(_texture, new Vector2(_position.X + _texture.Width / 2, _position.Y + _texture.Height / 2), null, Color.White, _rotation, _origine, 1f, SpriteEffects.FlipVertically, 0);
                // _textureAsteroid.Draw(spriteBatch);
                _textureAsteroid.Draw(spriteBatch);

            } else
            {
                _explosion.Draw(spriteBatch);
            }
           
        }

        //update method for sprite rotation
        /* if(_reverseUpdate == false)
               {
                   _textureAsteroid.UpdateLimitLess_ToLeft(gameTime);
               }

               if (sensRotation % 2 == 0)
                {
                    _rotation += -((float)rand.Next(1, 10) / 100);
                }
                else
                {
                    _rotation += +((float)rand.Next(1, 10) / 100);
                }*/
                
        // _origin = new Vector2(_textureAsteroid.Width/2 ,_textureAsteroid.Height/2 );

        //MouseState m = new MouseState();
    }
}


         

        


    











