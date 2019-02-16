using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace SpaceShooter
{
    class Sprite
    {
        //reference to the game class
       // private Game e_game;

        protected Vector2 _newposition;
        protected Vector2 _position;
        protected Texture2D _texture;
        protected Vector2 _speed;
        protected bool _active;
        protected int _width;
        protected int _height;
        protected Rectangle _rec;
        protected bool _collided;
        private bool _clicked;
        protected Vector2 _direction;

        protected Vector2 _origin;
        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Vector2 NewPosition
        {
            get { return _newposition; }
            set { _newposition = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        public bool Collided
        {
            get { return _collided; }
            set { _collided = value; }
        }
       /* public Game Game
        {
            get { return e_game; }
        }
        public ContentManager Content
        {
            get { return e_game.Content; }
        }*/
        public Rectangle Rec
        {
            get { return _rec; }
            set { _rec = value; }
        }
        public bool Clicked
        {
            get { return _clicked; }
            set { _clicked = value; }
        }
        
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        MouseState state;
        MouseState oldState;

        #region Constructeurs

        public Sprite(Game game)
        {
           // e_game = game;

        }

        public Sprite(Game game, Vector2 position) : this(game)
        {
            _position = position;
        }
        #endregion

        public virtual void Initialize()
        {


        }

        public virtual void LoadContent(ContentManager Content, string textureName)
        {
            _texture = Content.Load<Texture2D>(textureName);
            _width = _texture.Width;
            _height = _texture.Height;

            _rec = new Rectangle(
                 (int)_position.X,
                 (int)_position.Y,
                _width,
                _height);
        }

        public virtual void UnloadContent()
        {
            if (_texture != null)
                _texture.Dispose();

        }

        public virtual void Update()
        {
            _rec = new Rectangle(
             (int)_position.X,
             (int)_position.Y,
             _width,
             _height);

             //add position update*/
             
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
                spriteBatch.Draw(_texture, _position, Color.White);
        }

        public bool Collision(Sprite cible, Rectangle cible_RecSource = new Rectangle(), Rectangle source_RecSource = new Rectangle())
        {
            bool intersect = _rec.Intersects(cible.Rec) && intersectPixels(cible, cible_RecSource, source_RecSource);
            Collided = intersect;
            cible.Collided = intersect;
            return intersect;
        }

        private bool intersectPixels(Sprite cible, Rectangle cible_RecSource = new Rectangle(), Rectangle source_RecSource = new Rectangle()) //sprite cible
        {
            var sourceColors = new Color[_texture.Width * _texture.Height];
            if (source_RecSource.Width > 0)
            {
                _texture.GetData(0, source_RecSource, sourceColors, 0, cible.Rec.Width * cible.Rec.Height);
            }
            else
            {
                _texture.GetData(sourceColors);
            }
           

            var cibleColors = new Color[cible.Texture.Width * cible.Texture.Height];
            if (cible_RecSource.Width > 0)
            {
                cible.Texture.GetData(0, cible_RecSource, cibleColors, 0, cible.Rec.Width * cible.Rec.Height);
            }
            else
            {
                cible.Texture.GetData(cibleColors);
            }



            int left = Math.Max(_rec.Left, cible.Rec.Left);
            int top = Math.Max(_rec.Top, cible.Rec.Top);
            int right = Math.Min(_rec.Right, cible.Rec.Right) - left;
            int bottom = Math.Min(_rec.Bottom, cible.Rec.Bottom) - top;

            Rectangle intersectingRectangle = new Rectangle(left, top, right, bottom);
            Color sourceColor = Color.White;
            Color cibleColor = Color.White;

            for (int x = intersectingRectangle.Left; x < intersectingRectangle.Right; x++)
            {
                for (int y = intersectingRectangle.Top; y < intersectingRectangle.Bottom; y++)
                {
                    sourceColor = sourceColors[(x - _rec.Left) + (y - _rec.Top) * _rec.Width];
                    cibleColor = cibleColors[(x - cible.Rec.Left) + (y - cible.Rec.Top) * cible.Rec.Width];

                    if (sourceColor.A != 0 && cibleColor.A != 0)
                        return true;
                }
            }
            return false;
        }

        public void ClickControls()
        {
            /*if (!Contains)
            {
                soundEffectStarted = false;
            }
            if (Contains)
            {
                if (!soundEffectStarted)
                {
                    _soundEffectInstance.Play();
                    soundEffectStarted = true;
                }*/
            oldState = state;
            state = Mouse.GetState();

            // oldContains = true;
            if (state.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
                {
                    oldState = state;
                }
                else
                if (oldState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
                {
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
                    _clicked = true;
                    oldState = state;
                }


            /*}
            else
            {
                //_texture.UpdateOnceToLeft(gameTime);
            }*/
        }

        public Vector2 Direction_FromPointToPoint(Vector2 start, Vector2 end)
        {
            float distance = Vector2.Distance(start, end);
            Vector2 direction = Vector2.Normalize(end - start);
            return direction;
        }


    }

}







