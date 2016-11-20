using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        
        public Tir(Game game) : base(game)
        {
            base._position = Position;
            base._active = true;
            base._speed = Speed;
        }

        public override void Initialize()
        {
            base.Initialize();
            _tirs.Clear();
            
        }
        public void LoadContent(ContentManager content, String textureName, Vaisseau v)
        {
            base.LoadContent(content, textureName);
            
                _position = new Vector2((v.Position.X + (v.TextureShip.Width / 2)) - _texture.Width / 2, v.Position.Y);
                _speed = new Vector2(0, 15);
            
        }
     
        public override void Update(GameTime gameTime)
        {
            _rec = new Rectangle(
                  (int)_position.X,
                  (int)_position.Y,
                  _texture.Width,
                  _texture.Height);
            
                _position = new Vector2(_position.X, _position.Y - _speed.Y);
            
                if (_active && _position.Y + _texture.Height <= 0)
                    _active = false;
       
            /* public override void Draw(SpriteBatch spriteBatch)
              {
              destination.x = Math.Cos(a.Position.X);
              destination.y = Math.Sin(a.Position.Y);
              
             }*/
        }


    }
}
