using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceShooter
{
    class Vaisseau : Sprite
    {
        private Animation _explosion;
        public Animation Explosion
        {
            get { return _explosion; }
            set { _explosion = value; }
        }

        private Animation _fireEffect;
        public Animation FireEffect
        {
            get { return _fireEffect; }
            set { _fireEffect = value; }
        }

        private Animation _boost;
        public Animation Boost
        {
            get { return _boost; }
            set { _boost = value; }
        }

        private Animation _textureShip;
        public Animation TextureShip
        {
            get { return _textureShip; }
            set { _textureShip = value; }
        }

        private bool _fireActive;
        public bool FireActive
        {
            get { return _fireActive; }
            set { _fireActive = value; }
        }
        private bool _boostActive;
        public bool BoostActive
        {
            get { return _boostActive; }
            set { _boostActive = value; }
        }

        private bool _moveLeftActive;
        public bool MoveLeftActive
        {
            get { return _moveLeftActive; }
            set { _moveLeftActive = value; }
        }

        private bool _moveRightActive;
        public bool MoveRightActive
        {
            get { return _moveRightActive; }
            set { _moveRightActive = value; }
        }

        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public Vaisseau(Game game) : base(game)
        {
            _textureShip = new Animation(game, 1, 7, 50);
            _explosion = new Animation(game, 9, 9, 50);
            _boost = new Animation(game, 1, 5, 50);                 //change spritesheet
            _fireEffect = new Animation(game, 1, 4, 40);
            _textureShip.Active = true;
            _textureShip.CurrentFrame = (_textureShip.TotalFrames / 2) - (int)0.5; // define initial position in the sprite sheet when the ship isn't moving
            _active = true;
            _health = 10;
            _boostActive = false;
            _fireEffect.Active = false;
            _fireActive = false;
            _textureShip.Active = true;
            _textureShip.Moving = false;

        }
        public override void Initialize()
        {

            base.Initialize();
        }

        public void LoadContent(ContentManager content, String textureVaisseau, String textureExplosion, String textureFireEffect, String textureBoost)
        {
            _textureShip.LoadContent(content, textureVaisseau);
            _position = new Vector2((Game1.windowWidth / 2) - (_textureShip.Width / (_textureShip.Cols * 2)),
                                           (Game1.windowHeight - _textureShip.Height * 2));
            _texture = _textureShip.Texture; // define the parent object texture for  collision method with parameter(sprite)

            
            _explosion.LoadContent(content, textureExplosion);
            _boost.LoadContent(content, textureBoost);
            _fireEffect.LoadContent(content, textureFireEffect);


        }

        public void Update(GameTime gameTime, int H, int W)
        {

            if (_active == true)
            {                       // rectangle update 
                _rec = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _textureShip.Width,
                _textureShip.Height);

                _textureShip.Position = _position;

                Controls(H, W);
                ConditionsTo_UpdateShipMovements(gameTime);

                if (_boostActive == true)
                {
                    _boost.Active = true;
                    _boost.Position = new Vector2((_position.X + (_textureShip.Width / 2)) - (_boost.Texture.Width / (_boost.Cols * 2)), _position.Y + (_textureShip.Height - 10));
                    _boost.UpdateLimitLess_ToRight(gameTime);
                }
                else
                {
                    _boost.Active = false;
                }

                if (_fireActive == true)
                {
                    _fireEffect.Active = true;
                    // +1 and +14 to adjust position                                          
                    _fireEffect.Position = new Vector2(((_position.X + (_textureShip.Width / 2)) - (_fireEffect.Texture.Width / (_fireEffect.Cols * 2))) + 1,
                                                             (_position.Y - _fireEffect.Height) + 14);
                    _fireEffect.UpdateLimitLess_ToRight(gameTime);
                }
                else
                {
                    _fireEffect.Active = false;
                }
                if (_health == 0)
                    _active = false;
            }

            if (_active == false)
            {
                _explosion.Active = true;
                _explosion.UpdateOnceToRight(gameTime);
                _explosion.Position = _position;

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (_active == true)
            {
                if (_health > 3)
                {
                    _textureShip.Draw(spriteBatch);
                }
                else
                {
                    _textureShip.Draw(spriteBatch);// add color 
                }

                if (_boostActive)
                    _boost.Draw(spriteBatch);
                if (_fireActive)
                    _fireEffect.Draw(spriteBatch);

            }
            if (_active == false)
            {
                _explosion.Draw(spriteBatch);
            }

        }

        public void Controls(int H, int W)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q))
            {
                if (_position.X >= 0)
                    _position.X -= _speed.X;
                _moveLeftActive = true;
                _textureShip.Moving = true;

            }
            else
            {
                _moveLeftActive = false;
                _textureShip.MoveToPoint = true;
            }

            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                if (_position.X < (W - _textureShip.Width))
                {
                    _position.X += _speed.X;
                    _moveRightActive = true;
                    _textureShip.Moving = true;
                }
            }
            else
            {
                _moveRightActive = false;
                _textureShip.MoveToPoint = true;
            }
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z))
            {
                if (_position.Y >= 0)
                {
                    _position.Y -= _speed.Y;
                    _boostActive = true;
                }
            }
            else
            {
                _boostActive = false;
            }
            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                if (_position.Y < (H - (_textureShip.Height + 40)))// + 40 to prevent the draw of the ship over the bottom information zone 
                    _position.Y += _speed.Y;
            }
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Right))
            {
                _moveLeftActive = false;
                _moveRightActive = false;
                _textureShip.MoveToPoint = true;
            }

            if (state.IsKeyDown(Keys.Space))
            {
                _fireActive = true;

            }
            else
            {
                _fireActive = false;
            }

        }

        private void ConditionsTo_UpdateShipMovements(GameTime gameTime)
        {
            // TODO ameliorer les booleens
            if (_textureShip.Moving == true)
            {       //from center of spritesheet to the left
                if (_moveLeftActive == true)
                {
                    _textureShip.UpdateOnceToLeft(gameTime);
                }
                //from the left or from the right of the spritesheet to the center
                if (_moveLeftActive == false && _moveRightActive == false && _textureShip.Moving == true)
                {
                    _textureShip.UpdateOnce_FromPoint_ToPoint(gameTime, _textureShip.TotalFrames / 2);
                }
                //from the center of the spritesheet to the right
                if (_moveRightActive == true)
                {
                    _textureShip.UpdateOnceToRight(gameTime);
                }
            }

        }

    }
}
