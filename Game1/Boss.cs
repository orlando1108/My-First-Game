
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
        public int BorderRight
        {
            get { return _borderRight; }
            set { _borderRight = value; }
        }

        private int _borderLeft;
        public int BorderLeft
        {
            get { return _borderLeft; }
            set { _borderLeft = value; }
        }

        private int _randomBorderRight;
        public int RandomBorderRight
        {
            get { return _randomBorderRight; }
            set { _randomBorderRight = value; }
        }

        private int _randomBorderLeft;
        public int RandomBorderLeft
        {
            get { return _randomBorderLeft; }
            set { _randomBorderLeft = value; }
        }

        int randomFiresTimeSpent = 100;
        Random rand = new Random();
        
        public Boss(Game game):base(game)
        {
           
            _textureBoss = new Animation(game, 1, 1, 50);
            _explosion = new Animation(game, 9, 9, 50);
           // _fire = new Tir(game, +15);
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

            _borderRight = Game1.windowWidth - Texture.Width;
            _borderLeft = 0;
            _randomBorderLeft = rand.Next(BorderLeft, Game1.windowWidth / 2 - Texture.Width);
            _randomBorderRight = rand.Next(Game1.windowWidth / 2+Texture.Width, BorderRight);

            //_textureShip.Moving = false;
            // _boost.LoadContent(content, textureBoost);
            // _fireEffect.LoadContent(content, textureFireEffect);
        }

        public void Update(Game game, GameTime gameTime, Vector2 cible)
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

                UpdateFires(game,gameTime, cible);
                
                TouchedScreenBorders();
                BossIsMoving();
                
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
                _randomBorderRight = rand.Next(Game1.windowWidth / 2, BorderRight) - Texture.Width; // substract texture width to ameliorate the random
                
            }
        if (_position.X < _randomBorderLeft)
        {
            _touchLeftScreenBorders = true;
            _touchRightScreenBorders = false;
            _randomBorderLeft = rand.Next(BorderLeft, Game1.windowWidth / 2) + Texture.Width;// substract texture width to ameliorate the random
            }
        }

        public void UpdateFires(Game game, GameTime gameTime, Vector2 cible)
        {
            
            if (firesTimeSpent > randomFiresTimeSpent)
            {
                _fire = new Tir(game, 5);
                _fire.LoadContent(Content, "BossBullet", Rec);
                _fire.Position = new Vector2((Rec.X + (Rec.Width / 2)) - Fire.Texture.Width / 2, (Rec.Y+Texture.Height)-Fire.Texture.Height);
                _fire.Direction = TargetTracking(_fire.Position, cible);
                FiresList.Add(_fire);
                FireSoundEffect.CreateInstance().Play();
                firesTimeSpent = 0;
                randomFiresTimeSpent = rand.Next(500, 1000);
            }
            else
            {
                _fireActive = false;
            }
            foreach (Tir t in FiresList)
            {
                /*Vector2 direction= TargetTracking(t.Position, cible);
                t.Update_toDestination(gameTime,direction);*/
                t.Update_toDestination(gameTime);

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

        // target tracking
        private Vector2 TargetTracking(Vector2 start, Vector2 end )
        {
            
            float distance = Vector2.Distance(start, end);
            Vector2 direction = Vector2.Normalize(end - start);

            return direction;

        //lander.engineOn = true;
      /*  float angle_radian = MathHelper.ToRadians(lander.angle);
            float force_x = (float)Math.Cos(angle_radian) * lander.speed;
            float force_y = (float)Math.Sin(angle_radian) * lander.speed;
            lander.velocity += new Vector2(force_x, force_y);


            /*Vector3 newVector = targetPoint - initialPoint;
   or

   Vector3 newVector = targetTransform.position - fromTransform.position;*/
        }

        private Vector2 HightAI_Fires(Vector2 cible, Tir t)
        {
            Vector2 destination = new Vector2();
            destination = cible - t.Position;

            return destination;
        }
    }
}
