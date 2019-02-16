using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace SpaceShooter
{
    class Tir : Sprite
    {
        private List<Tir> _tirs;
        public List<Tir> Tirs
        {
            get { return _tirs; }
            set { _tirs = value; }
        }
        
        private float _angle { get; set; }
        public float _angleSpeed { get; set; }
        public float speed { get; set; }

        public Tir(Game game, float speed) : base(game)
        {
            base._speed = new Vector2(0, speed);
            base._active = true;
            this.speed = speed; // todo : change it in the same _speed; 
            this._angle = 90;
        }        

        public override void Initialize()
        {
            base.Initialize();
            _tirs.Clear();
        }
       /* public void LoadContent(ContentManager content, String textureName)
        {
            base.LoadContent(content, textureName);
        }*/
     
        public override void Update()
        {
            _rec = new Rectangle(
                  (int)_position.X,
                  (int)_position.Y,
                  _texture.Width,
                  _texture.Height);
            
                _position = new Vector2(_position.X, _position.Y - _speed.Y);
            
                if (_active && _position.Y + _texture.Height <= 0)
                    _active = false;
        }


        public void Update_toDestination()
        {
            _rec = new Rectangle(
                 (int)_position.X,
                 (int)_position.Y,
                 _texture.Width,
                 _texture.Height);

            _position += _direction*speed;
            
           
           
        }

        public void DrawTir_WithAngle(SpriteBatch spriteBatch)
        {
            if(_active == true)
            {
                Vector2 originTir = new Vector2(_texture.Width / 2, _texture.Height / 2);
                // add a new vector2 in order to the origin for detect the correct position of the sprites collision
                spriteBatch.Draw(_texture, new Vector2(_position.X + _texture.Width / 2, _position.Y + _texture.Height / 2), null, Color.White,
                _angleSpeed - MathHelper.ToRadians(90), originTir, 1, SpriteEffects.None, 0);
            }
           
           

          
        }


    }
}
