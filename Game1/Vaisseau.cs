using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

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
        private Tir _fire;
        public Tir Fire
        {
            get { return _fire; }
            set { _fire = value; }
        }

        private List<Tir> _firesList;
        public List<Tir> FiresList
        {
            get { return _firesList; }
            set { _firesList = value; }
        }

        private SoundEffect _fireSoundEffect;
        public SoundEffect FireSoundEffect
        {
            get { return _fireSoundEffect; }
            set { _fireSoundEffect = value; }
        }

        private Song _explosionSound;
        public Song ExplosionSound
        {
            get { return _explosionSound; }
            set { _explosionSound = value; }
        }
        
        private Media _media;
        public Media Media
        {
            get { return _media; }
            set { _media = value; }
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

        long firesTimeSpent;

        public Vaisseau(Game game, Media media) : base(game)
        {
            _textureShip = new Animation(game, 1, 7, 50);
            _explosion = new Animation(game, 9, 9, 50);
            _boost = new Animation(game, 1, 5, 50);                 //change spritesheet
            _fireEffect = new Animation(game, 1, 4, 40);
            _fire = new Tir(game, -15);
            _media = media;
            _firesList = new List<Tir>();
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
            if(_firesList.Count > 0)
            {
                _firesList.Clear();
            }
            _speed = new Vector2(10, 10);
            base.Initialize();
        }

        public override void UnloadContent()
        {
            _textureShip.UnloadContent();
            foreach (Tir t in _firesList)
            {
                t.UnloadContent();
            }

            base.UnloadContent();
        }

        public void LoadContent(ContentManager content, String textureVaisseau, String textureExplosion, String textureFireEffect, String fireSoundEffect, String explosionSound, String textureBoost)
        {
           
            _fireSoundEffect = Content.Load<SoundEffect>(fireSoundEffect);
            _explosionSound = content.Load<Song>(explosionSound);
            _textureShip.LoadContent(content, textureVaisseau);
            _position = new Vector2((Settings._WindowWidth / 2) - (_textureShip.Width / (_textureShip.Cols * 2)),
                                    (Settings._WindowHeight - _textureShip.Height * 2));
            _texture = _textureShip.Texture;
            _explosion.LoadContent(content, textureExplosion);
            _boost.LoadContent(content, textureBoost);
            _fireEffect.LoadContent(content, textureFireEffect);


        }

        public void Update(GameTime gameTime, Game game, Boss boss)
        {
            firesTimeSpent += gameTime.ElapsedGameTime.Milliseconds;

            if (_active == true)
            {                       // rectangle update 
                _rec = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _textureShip.Width,
                _textureShip.Height);
                _textureShip.Position = _position;
                Controls();
                ConditionsTo_UpdateShipMovements(gameTime);
                UpdateFires(game, boss);

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
                {
                    _active = false;
                }
            }

            if (_active == false)
            {
                _media.PlayMusic(_explosionSound);
                _explosion.Active = true;
                _explosion.UpdateOnceToRight(gameTime);
                _explosion.Position = _position;
                _fireActive = false;
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
                foreach (Tir t in _firesList)
                {
                    t.Draw(spriteBatch);
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

        public void Controls()
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
                if (_position.X < (Settings._WindowWidth - _textureShip.Width))
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
                if (_position.Y < (Settings._WindowHeight - (_textureShip.Height + 40)))// + 40 to prevent the draw of the ship over the bottom information zone 
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

        private void UpdateFires(Game game, Boss boss)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && firesTimeSpent > 200)
            {
                _fire = new Tir(game, -15);
                _fire.LoadContent(Content, "Sprites/fire");
                _fire.Position = new Vector2((_rec.X + (_rec.Width / 2)) - _fire.Texture.Width / 2, +_rec.Y);
                _firesList.Add(_fire);
                _media.PlaySound(_fireSoundEffect);
                firesTimeSpent = 0;
            }
            else
            {
                _fireActive = false;
            }
            foreach (Tir t in _firesList)
            {
                if (boss.Active && t.Collision(boss))
                {
                    t.Active = false;
                    boss.Health -= 1;
                }
                t.Update();
            }
            for (int i = 0; i < _firesList.Count; i++)
            {
                if (_firesList[i].Active == false)
                {
                    _firesList.RemoveAt(i);
                    i -= 1;
                }
            }
        }

    }
}
