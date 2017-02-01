
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Boss : Sprite
    {
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
            get { return _firesList; }
            set { _firesList = value; }
        }
        private List<Tir> _missileList;
        public List<Tir> MissileList
        {
            get { return _missileList; }
            set { _missileList = value; }
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

        private Animation _textureBoss;
        public Animation TextureBoss
        {
            get { return _textureBoss; }
            set { _textureBoss = value; }
        }

        private Texture2D _healthTexture;
        public Texture2D HealthTexture
        {
            get { return _healthTexture; }
            set { _healthTexture = value; }
        }

        private Tir _missile;
        public Tir Missile
        {
            get { return _missile; }
            set { _missile = value; }
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

        private int _health;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        private bool _touchRightScreenBorders;
        public bool TouchRightScreenBorders
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

        private int _borderRight;
        private int _borderLeft;
        private int _randomBorderRight;
        private int _randomBorderLeft;
        long firesTimeSpent;
        long missileTimeSpent;


        int randomFiresTimeSpent = 500;
        int randomMissileTimeSpent = 300;
        Random rand = new Random();

        public Boss(Game game, Media media) : base(game)
        {
            _textureBoss = new Animation(game, 1, 1, 50);
            _explosion = new Animation(game, 9, 9, 50);
            _missile = new Tir(game, 4);
            _media = media;
            _missile.Active = false;
            _textureBoss.Active = true;
            _active = true;
            _health = 10;
            _fireActive = false;
            _touchRightScreenBorders = false;
            _touchLeftScreenBorders = true;
            _firesList = new List<Tir>();
            _missileList = new List<Tir>();
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
            _textureBoss.UnloadContent();
            foreach (Tir t in _firesList)
            {
                t.UnloadContent();
            }

            base.UnloadContent();
        }

        public void LoadContent(ContentManager content, string textureBoss, string textureExplosion, string healthTexture, string fireSoundEffect, string explosionSound, string textureMissile)
        {
           
            TextureBoss.LoadContent(content, textureBoss);
            Texture = _textureBoss.Texture; // define the parent object texture for  collision method with parameter(sprite)
            _position = new Vector2((Settings._WindowWidth / 2) - (_texture.Width / 2), 0);
            _missile.LoadContent(Content, "Sprites/missile1");

            //_missile.Position = new Vector2(300, 300);
            Explosion.LoadContent(content, textureExplosion);
            HealthTexture = Content.Load<Texture2D>(healthTexture);
            _fireSoundEffect = Content.Load<SoundEffect>(fireSoundEffect);
            _explosionSound = content.Load<Song>(explosionSound);

            _borderRight = Settings._WindowWidth - Texture.Width;
            _borderLeft = 0;
            _randomBorderLeft = rand.Next(_borderLeft, Settings._WindowWidth / 2 - Texture.Width);
            _randomBorderRight = rand.Next(Settings._WindowWidth / 2 + Texture.Width, _borderRight);

            //_textureShip.Moving = false;
            // _boost.LoadContent(content, textureBoost);
            // _fireEffect.LoadContent(content, textureFireEffect);
        }

        public void Update(Game game, GameTime gameTime, Vaisseau ship)
        {
            firesTimeSpent += gameTime.ElapsedGameTime.Milliseconds;
            missileTimeSpent += gameTime.ElapsedGameTime.Milliseconds;

            if (_active == true)
            {                       // rectangle update 
                _rec = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _textureBoss.Width,
                _textureBoss.Height);

                _textureBoss.Position = _position;

                if (ship.Active == true)
                {
                    UpdateFires(game, ship);
                    UpdateMissile(game, ship);
                }
                if (_health == 0)
                {
                    _active = false;
                }

                if (Collision(ship, ship.TextureShip.SourceRec))
                {
                    ship.Health = 0;
                }

                TouchedScreenBorders();
                BossIsMoving();
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
                int pixelAlign = 25;

                foreach (Tir t in _firesList)
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

                for (int i = 0; i != _health; i++)
                {
                    spriteBatch.Draw(HealthTexture, new Vector2(5, pixelAlign), Color.Beige);
                    pixelAlign += 40;
                }
                
               // foreach(Tir missile in _missileList)
               // {
              
                    _missile.DrawTir_WithAngle(spriteBatch);
              //  }
                    

            }
            if (_active == false)
            {
                _explosion.Draw(spriteBatch);
            }

        }

        private void BossIsMoving()
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
        //method to indicate if boss has touched  borders of the screen
        //add a random variable for the boss AI
        private void TouchedScreenBorders()
        {

            if (_position.X > _randomBorderRight)
            {
                _touchRightScreenBorders = true;
                _touchLeftScreenBorders = false;
                _randomBorderRight = rand.Next(Settings._WindowWidth / 2, _borderRight); // substract texture width to ameliorate the random

            }
            if (_position.X < _randomBorderLeft)
            {
                _touchLeftScreenBorders = true;
                _touchRightScreenBorders = false;
                _randomBorderLeft = rand.Next(_borderLeft, Settings._WindowWidth / 2);// add texture width to ameliorate the random
            }
        }

        public void UpdateFires(Game game, Vaisseau ship)
        {
            if (firesTimeSpent > randomFiresTimeSpent)
            {
                _fire = new Tir(game, 15);
                _fire.LoadContent(Content, "Sprites/BossBullet");
                _fire.Position = new Vector2((Rec.X + (Rec.Width / 2)) - Fire.Texture.Width / 2, (Rec.Y + Texture.Height) - Fire.Texture.Height);
                _fire.Direction = BulletTracking(_fire.Position, ship.Rec.Center.ToVector2());
                FiresList.Add(_fire);
                _media.PlaySound(_fireSoundEffect);
                firesTimeSpent = 0;
                randomFiresTimeSpent = rand.Next(500, 800);
            }

            foreach (Tir t in FiresList)
            {
                t.Update_toDestination();
                if (t.Collision(ship, ship.TextureShip.SourceRec))
                {
                    t.Active = false;
                    ship.Health -= 1;
                    if(_health < 10)
                    {
                        _health += 1;
                    }
                }
            }
            for (int i = 0; i < FiresList.Count; i++)
            {
                if (_firesList[i].Active == false)
                {
                    _firesList.RemoveAt(i);
                    i -= 1;
                }
            }
        }

        private void UpdateMissile(Game game, Vaisseau ship)
        {
            /*Vector2 direction= TargetTracking(t.Position, cible);                       method for one target tracking
               t.Update_toDestination(gameTime,direction);*/
          
                if (missileTimeSpent > randomMissileTimeSpent && _missile.Active == false)
                {
                     _missile.Position = new Vector2((_position.X + (Rec.Width / 2)) - _missile.Texture.Width / 2, (Rec.Y +  _missile.Texture.Height));
                  _missile.Active = true;
                //  _missileList.Add(_missile);
                     missileTimeSpent = 0;
                    randomMissileTimeSpent = rand.Next(800, 1000);
                }
                
               // foreach(Tir missile in _missileList)
             //   {
             if(_missile.Active == true)
            {
                if (_missile.Position.Y < ship.Position.Y + ship.Height)
                {
                    _missile.Direction = MissileTracking(_missile.Position, ship.Rec.Center.ToVector2());
                    _missile._angleSpeed = GetAngleMissile_FromShipPosition(ship.Rec.Center.ToVector2());
                }
                if (_missile.Collision(ship, ship.TextureShip.SourceRec))
                {
                    _missile.Active = false;
                    ship.Health = 0;
                }
                if(_missile.Position.X > Settings._WindowWidth || _missile.Position.Y > Settings._WindowHeight)
                {
                    _missile.Active = false;
                }
                _missile.Update_toDestination();
            }

               /* for (int i = 0; i < _missileList.Count; i++)
                {
                    if (_missileList[i].Active == false)
                    {
                        _missileList.RemoveAt(i);
                        i -= 1;
                    }
                }*/
            }
        
        // target tracking
        private Vector2 BulletTracking(Vector2 start, Vector2 end)
        {
            float distance = Vector2.Distance(start, end);
            Vector2 direction = Vector2.Normalize(end - start);
            return direction;
        }

        // missile tracking
        private Vector2 MissileTracking(Vector2 start, Vector2 end)
        {
            float distance = Vector2.Distance(start, end);
            Vector2 direction = Vector2.Normalize(end - start);
            return direction;
        }

        private float GetAngleMissile_FromShipPosition(Vector2 shipPosition)
        {
            Vector2 Pos = _missile.Position - shipPosition;
            float angle_radians = (float)Math.Atan2(Pos.Y, Pos.X);

            return angle_radians;
        }
    }
}
