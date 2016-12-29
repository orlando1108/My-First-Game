using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace SpaceShooter
{
    class Sprite
    {
        //reference to the game class
        private Game e_game;

        protected Vector2 _newposition;
        protected Vector2 _position;
        protected Texture2D _texture;
        protected Vector2 _speed;
        protected bool _active;
        protected int _width;
        protected int _height;
        protected Rectangle _rec;

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

        protected bool _collided;
        public bool Collided
        {
            get { return _collided; }
            set { _collided = value; }
        }

        public Game Game
        {
            get { return e_game; }
        }

        public ContentManager Content
        {
            get { return e_game.Content; }
        }

        public Rectangle Rec
        {
            get { return _rec; }
            set { _rec = value; }
        }
        protected Rectangle _recDestination;
        public Rectangle Destination
        {
            get { return _recDestination; }
            set { _recDestination = value; }
        }

        #region Constructeurs

        public Sprite(Game game)
        {
            e_game = game;

        }

        public Sprite(Game game, Vector2 position)
            : this(game)
        {
            this._position = position;
        }
        #endregion

        public virtual void Initialize()
        {


        }

        public virtual void LoadContent(ContentManager Content, string textureName)
        {
            _texture = Content.Load<Texture2D>(textureName);
            /*_rec = new Rectangle(
                 (int)_position.X,
                 (int)_position.Y,
                _texture.Width,
                _texture.Height);*/
        }

        public virtual void UnloadContent()
        {
            if (_texture != null)
                _texture.Dispose();

        }

        public virtual void Update(GameTime gameTime)
        {

            _recDestination = new Rectangle(
             (int)_position.X,
             (int)_position.Y,
             _texture.Width,
             _texture.Height);

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
                spriteBatch.Draw(_texture, _position, Color.White);
        }

        public bool Collision(Sprite cible, Rectangle textureSource = new Rectangle())
        {
            bool intersect = _rec.Intersects(cible.Rec) && intersectPixels(cible, textureSource);
            Collided = intersect;
            cible.Collided = intersect;
            return intersect;
        }

        private bool intersectPixels(Sprite cible, Rectangle textureSource = new Rectangle()) //sprite cible
        {
            var sourceColors = new Color[_texture.Width * _texture.Height];
            _texture.GetData(sourceColors);

            var cibleColors = new Color[cible.Rec.Width * cible.Rec.Height];
            if (textureSource.Width > 0)
            {
                cible.Texture.GetData(0, textureSource, cibleColors, 0, cible.Rec.Width * cible.Rec.Height);
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


    }

}







