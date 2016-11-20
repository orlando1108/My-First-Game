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

        private Vector2 _finalPosition;
        public Vector2 FinalPosition
        {
            get { return _finalPosition; }
            set { _finalPosition = value; }
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

        public override void LoadContent(ContentManager Content, string texture)
        {
            base.LoadContent(Content, texture);
            Width = _texture.Width/Cols;
            Height = _texture.Height/Ligs;
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
                base.Update(gameTime);
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
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRec;
            if(_moving == true)
            {
                if (_active == true)
                {
                    int row;
                    int column;
                    
                        row = (int)((float)_currentFrame / Cols);
                        column = _currentFrame % Cols;
                        sourceRec = new Rectangle(_width * column, _height * row, _width, _height);
                        Rectangle destinationRec = new Rectangle((int)_finalPosition.X, (int)_finalPosition.Y, _width, _height);
                        spriteBatch.Draw(Texture, destinationRec, sourceRec, Color.White);
                }
            }
            if(_moving == false)
            {
                // to display one of all sprites of the texture 
                sourceRec = new Rectangle((_texture.Width/2)-(_width/2), 0, _width, _height); // change this according to the sprite sheet size 
                Rectangle destinationRec = new Rectangle((int)_finalPosition.X, (int)_finalPosition.Y, _width, _height);
                spriteBatch.Draw(Texture, destinationRec, sourceRec, Color.White);

            }

        }

        public void Draw_ToResize(SpriteBatch spriteBatch)
        {
            Rectangle sourceRec;
            Vector2 origin;
            if (_moving == true)
            {
                if (_active == true)
                {
                    int row;
                    int column;

                    row = (int)((float)_currentFrame / Cols);
                    column = _currentFrame % Cols;
                    sourceRec = new Rectangle(_width * column, _height * row, _width, _height);
                    origin = new Vector2(_finalPosition.X + (_width/2), FinalPosition.Y + (_height/2));
                    Rectangle destinationRec = new Rectangle((int)_finalPosition.X, (int)_finalPosition.Y, _width, _height);
                   // spriteBatch.Draw(Texture, destinationRec, sourceRec, Color.White);
                    spriteBatch.Draw(Texture, _finalPosition, sourceRec, Color.White, 0f, origin, 0.5f, SpriteEffects.None, 0f);
                }
            }
            if (_moving == false)
            {
                // to display one of all sprites of the texture 
                sourceRec = new Rectangle((_texture.Width / 2) - (_width / 2), 0, _width, _height); // change this according to the sprite sheet size 
                Rectangle destinationRec = new Rectangle((int)_finalPosition.X, (int)_finalPosition.Y, _width, _height);
                spriteBatch.Draw(Texture, destinationRec, sourceRec, Color.White);

            }

        }

    }
}
