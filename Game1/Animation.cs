using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace SpaceShooter
{
    class Animation : Sprite
    {
        private int _timeSinceLastFrame = 0;
        private int _speedPerFrames = 0;
        Random rand = new Random();

        private bool _rotationActive;
        public bool RotationActive
        {
            get { return _rotationActive; }
            set { _rotationActive = value; }
        }

        private int _currentFrame;
        public int CurrentFrame
        {
            get { return _currentFrame; }
            set { _currentFrame = value; }
        }
        private int _totalFrames;
        public int TotalFrames
        {
            get { return _totalFrames; }
            set { _totalFrames = value; }
        }
        private bool _destroyable;
        public bool Destroyable
        {
            get { return _destroyable; }
            set { _destroyable = value; }
        }

        
        public int Ligs { get; set; }
        public int Cols { get; set; }

        private bool _removeable;
        public bool Removeable
        {
            get { return _removeable; }
            set { _removeable = value; }
        }

        private bool _finalFrameActive;
        public bool FinalFrameActive
        {
            get { return _finalFrameActive; }
            set { _finalFrameActive = value; }
        }

        private bool _moving;
        public bool Moving
        {
            get { return _moving; }
            set { _moving = value; }
        }

        private bool _moveToPoint;
        public bool MoveToPoint
        {
            get { return _moveToPoint; }
            set { _moveToPoint = value; }
        }

        private Rectangle _sourceRec;
        public Rectangle SourceRec
        {
            get { return _sourceRec; }
            set { _sourceRec = value; }
        }

        private Rectangle _box;
        public Rectangle Box
        {
            get { return _box; }
            set { _box = value; }
        }



        public Animation(Game game, int ligs, int cols, Vector2 Speed, float Position, int speedPerFrames) : base(game)
        {
            this._position.X = Position;
            this._speed = Speed;
            this.Ligs = ligs;
            this.Cols = cols;
            this._currentFrame = 0;
            this._totalFrames = Ligs * Cols;
            this._active = false;
            this._removeable = false;
            this._moving = true;
            this._speedPerFrames = speedPerFrames;
           // this._rotationActive = false;

        }
        public Animation(Game game, int ligs, int cols, int speedPerFrames)
            : base(game)
        {
            this.Ligs = ligs;
            this.Cols = cols;
            this._currentFrame = 0;
            this._totalFrames = Ligs * Cols;
            this._active = false;
            this._removeable = false;
            this._finalFrameActive = false;
            this._moving = true;
            this._speedPerFrames = speedPerFrames;
           //_rotationActive = false;
    
        }

        public override  void LoadContent(ContentManager Content, string texture)
        {
            base.LoadContent(Content, texture);
            Width = _texture.Width/Cols;
            Height = _texture.Height/Ligs;

           // _box = new Rectangle((int)_position.X - boxSize, (int)_position.Y - boxSize, _width + (boxSize*2), _height + (boxSize*2));
            /*  _rec = new Rectangle(
                  (int)_position.X,
                  (int)_position.Y,
                 Width,
                 Height);*/

        }

        public void UpdateOnceToRight(GameTime gameTime)
        {
            
        if (_active == true)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > _speedPerFrames)
            {
                _timeSinceLastFrame -= _speedPerFrames;
                if (_currentFrame < _totalFrames-1)
                {
                    _currentFrame++;
                }
                else
                {
                    _finalFrameActive = true;
                    _removeable = true;
                }
            }
           
        }
        }

        public void UpdateOnceToLeft(GameTime gameTime)
        {
            if (_active == true)
            { _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                if (_timeSinceLastFrame > _speedPerFrames)
                { _timeSinceLastFrame -= _speedPerFrames;

                    if (_currentFrame > 0)
                    {
                        _currentFrame--;
                    }
                    else
                    {
                        _finalFrameActive = true;
                        _removeable = true;
                    }
                }
            }
        }

        public void UpdateOnce_FromPoint_ToPoint(GameTime gameTime, int toThisFrame)
        {
            if (_active == true)
            {                       //go up to the point
                if(_currentFrame < _totalFrames / 2)
                {
                    _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                    if (_timeSinceLastFrame > _speedPerFrames)
                    {
                        _timeSinceLastFrame -= _speedPerFrames;
                        if (_currentFrame < TotalFrames)
                        {
                            _currentFrame++;
                        }
                        else
                        {
                            _finalFrameActive = true;
                            _removeable = true;
                        }
                    }
                }               

                if(_currentFrame > _totalFrames / 2)
                {                                    //go down to the point
                    _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                    if (_timeSinceLastFrame > _speedPerFrames)
                    {
                        _timeSinceLastFrame -= _speedPerFrames;
                        if (_currentFrame > toThisFrame)
                        {
                            _currentFrame--;
                        }
                        else
                        {
                            _finalFrameActive = true;
                            _removeable = true;
                        }

                    }
                }
            }
        }

        public  void UpdateLimitLess_ToRight(GameTime gameTime)
        {
            if (_active == true)
            {
                _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (_timeSinceLastFrame > _speedPerFrames)
                {
                    _timeSinceLastFrame -= _speedPerFrames;
                    _currentFrame++;
                    if (_currentFrame == _totalFrames)
                        _currentFrame = 0;
                }
                base.Update();
            }
        }

        public void UpdateLimitLess_ToLeft(GameTime gameTime)
        {

            if (_active == true)
            {
                _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (_timeSinceLastFrame > _speedPerFrames)
                {
                    _timeSinceLastFrame -= _speedPerFrames;
                    _currentFrame--;
                    if (_currentFrame == _totalFrames)
                        _currentFrame = 0;
                }
                base.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, float scale = 1)
        {
            _rec = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
               Width,
               Height);

            if (_moving == true)
            {
                if (_active == true)
                {
                    int row;
                    int column;
                    
                        row = (int)((float)_currentFrame / Cols);
                        column = _currentFrame % Cols;
                        _sourceRec = new Rectangle(_width * column, _height * row, _width, _height);
                    Rectangle destinationRec = new Rectangle((int)Position.X, (int)Position.Y, _width, _height);
                       if(scale != 1)
                    {
                        spriteBatch.Draw(_texture, new Vector2(_position.X+_width,_position.Y+_height), _sourceRec, Color.White, 0, new Vector2(_position.X + _width, _position.Y + _height), scale, SpriteEffects.None, 0);
                    }else
                    {
                        spriteBatch.Draw(_texture, destinationRec, _sourceRec, Color.White);
                    }
                        
                }
            }
            if(_moving == false)
            {
                // to display one of all sprites of the texture 
                _sourceRec = new Rectangle((_texture.Width/2)-(_width/2), 0, _width, _height); // change this according to the sprite sheet size 
                Rectangle destinationRec = new Rectangle((int)Position.X, (int)Position.Y, _width, _height);
                spriteBatch.Draw(Texture, destinationRec, _sourceRec, Color.White);

            }

        }
    }
}
