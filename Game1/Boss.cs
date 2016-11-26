
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
    class Boss : Sprite
    {
        long firesTimeSpent;

        private Animation _explosion;
        public Animation Explosion
        {
            get { return _explosion; }
            set { _explosion = value; }
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
            get { return _firesList     ; }
            set { _firesList      = value; }
        }

        private SoundEffect _fireSoundEffect;
        public SoundEffect FireSoundEffect
        {
            get { return _fireSoundEffect; }
            set { _fireSoundEffect = value; }
        }

        SoundEffect fire;

        /*  private Animation _fireEffect;
          public Animation FireEffect
          {
              get { return _fireEffect; }
              set { _fireEffect = value; }
          }*/

        /* private Animation _boost;
         public Animation Boost
         {
             get { return _boost; }
             set { _boost = value; }
         }*/

        private Animation _textureBoss;
        public Animation TextureBoss
        {
            get { return _textureBoss; }
            set { _textureBoss = value; }
        }

        private bool _fireActive;
        public bool FireActive
        {
            get { return _fireActive; }
            set { _fireActive = value; }
        }

       /* private bool _boostActive;
        public bool BoostActive
        {
            get { return _boostActive; }
            set { _boostActive = value; }
        }*/

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

        private bool _touchRightScreenBorders;
        public bool TouchScreenBorders
        {
            get { return _touchRightScreenBorders; }
            set { _touchRightScreenBorders = value; }
        }

        private bool _touchLeftScreenBorders;
        public bool TouchLeftScreenBorders
        {
            get { return _touchLeftScreenBorders; }
            set { _touchLeftScreenBorders = value; }

        }

        public Boss(Game game):base(game)
        {
            _textureBoss = new Animation(game, 1, 1, 50);
            _explosion = new Animation(game, 9, 9, 50);
            _fire = new Tir(game, +15);
            _textureBoss.Active = true;
            _active = true;
            _health = 10;
            _fireActive = false;
            _touchRightScreenBorders = false;
            _touchLeftScreenBorders = true;
            _firesList = new List<Tir>();
            //_textureBoss.CurrentFrame = (_textureBoss.TotalFrames / 2) - (int)0.5; // define initial position in the sprite sheet when the ship does't moving
            // _boost = new Animation(game, 1, 5, 50);                 //change spritesheet
            // _fireEffect = new Animation(game, 1, 4, 40);
            // _boostActive = false;
            // _fireEffect.Active = false;

        }
        public override void Initialize()
        {

            base.Initialize();
        }

        public void LoadContent(ContentManager content, string textureBoss, string textureExplosion, string textureFire, string fireSoundEffect)
        {
            TextureBoss.LoadContent(content, textureBoss);
            Texture = _textureBoss.Texture; // define the parent object texture for  collision method with parameter(sprite)
            Explosion.LoadContent(content, textureExplosion);
           // Fire.LoadContent(content, textureFire, TextureBoss.Rec);
            FireSoundEffect = Content.Load<SoundEffect>(fireSoundEffect);

            //_textureShip.Moving = false;
            // _boost.LoadContent(content, textureBoost);
            // _fireEffect.LoadContent(content, textureFireEffect);
        }

        public void Update(Game game, GameTime gameTime, int H, int W)
        {

            firesTimeSpent += gameTime.ElapsedGameTime.Milliseconds;

            if (_active == true)
            {                       // rectangle update 
                _rec = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _textureBoss.Width,
                _textureBoss.Height);

                _textureBoss.FinalPosition = _position;

                UpdateFires(game,gameTime);
                
                TouchedScreenBorders(H, W);
                BossIsMoving(H, W);


               // Controls(state, H, W);
                //ConditionsTo_UpdateShipMovements(gameTime);

              /*  if (_boostActive == true)
                {
                    _boost.Active = true;
                    _boost.FinalPosition = new Vector2((_position.X + (_textureShip.Width / 2)) - (_boost.Texture.Width / (_boost.Cols * 2)), _position.Y + (_textureShip.Height - 10));
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
                    _fireEffect.FinalPosition = new Vector2(((_position.X + (_textureShip.Width / 2)) - (_fireEffect.Texture.Width / (_fireEffect.Cols * 2))) + 1,
                                                             (_position.Y - _fireEffect.Height) + 14);
                    _fireEffect.UpdateLimitLess_ToRight(gameTime);
                }
                else
                {
                    _fireEffect.Active = false;
                }*/
            }

            if (_active == false)
            {
                _explosion.Active = true;
                _explosion.UpdateOnceToRight(gameTime);
                _explosion.FinalPosition = _position;
                _fireActive = false;
            }

        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            if (_active == true)
            {
                foreach (Tir t in FiresList)
                {
                    t.Draw(spriteBatch);
                }
                if (_health > 3)
                {
                    _textureBoss.Draw(spriteBatch);

                }
                else
                {
                    _textureBoss.Draw(spriteBatch);// add color 
                }

               /* if (_boostActive)
                    _boost.Draw(spriteBatch);
                if (_fireActive)
                    _fireEffect.Draw(spriteBatch);*/

            }
            if (_active == false)
            {
                _explosion.Draw(spriteBatch);
            }

        }

        private void BossIsMoving( int H, int W)
        {
            if (_touchRightScreenBorders)
            {
               /* if (_position.X >= 0)*/
                    _position.X -= _speed.X;
               // _moveLeftActive = true;
               // _textureShip.Moving = true;

            }
         /*   else
            {
              //  _moveLeftActive = false;
              //  _textureShip.MoveToPoint = true;
            }*/

          
                if (_touchLeftScreenBorders)
                {
                    _position.X += _speed.X;
                  // _moveRightActive = true;
                 //   _textureShip.Moving = true;
                }
           
           /* else
            {
               // _moveRightActive = false;
              //  _textureShip.MoveToPoint = true;
            }*/
        
        }

        private void TouchedScreenBorders(int H, int W)
        {
            if (_position.X == W - _textureBoss.Width) {
                _touchRightScreenBorders = true;
                _touchLeftScreenBorders = false;
            }
            if(_position.X == 0){
                _touchLeftScreenBorders = true;
                _touchRightScreenBorders = false;
            }
                
        }

        public void UpdateFires(Game game, GameTime gameTime)
        {
            
            if (firesTimeSpent > 100)
            {
                _fire = new Tir(game, +15);
                _fire.LoadContent(Content, "fire", Rec);
                _fire.Position = new Vector2((Rec.X + (Rec.Width / 2)) - Fire.Texture.Width / 2, (Rec.Y+Texture.Height)-Fire.Texture.Height);
                FiresList.Add(_fire);
                FireSoundEffect.CreateInstance().Play();
                firesTimeSpent = 0;
            }
            else
            {
                _fireActive = false;
            }
            foreach (Tir t in FiresList)
            {
                t.Update(gameTime);
            }
            for (int i = 0; i < FiresList.Count; i++)
            {
                if (FiresList[i].Active == false)
                {
                    FiresList.RemoveAt(i);
                    i -= 1;
                }
            }
        }
    }
}
