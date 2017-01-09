using Microsoft.Xna.Framework;
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
    class EquipmentsMenu
    {
        private Song _menuMusic;
        public Song MenuMusic
        {
            get
            {
                return _menuMusic;
            }
            set { _menuMusic = value; }
        }
        private Texture2D _backGround;
        public Texture2D BackGround
        {
            get { return _backGround; }
            set { _backGround = value; }
        }
        private Animation _blueShip;
        public Animation BlueShip
        {
            get { return _blueShip; }
            set { _blueShip = value; }
        }
        private Animation _yellowShip;
        public Animation YellowShip
        {
            get { return _yellowShip; }
            set { _yellowShip = value; }
        }
        private Animation _goldShip;
        public Animation GreenShip
        {
            get { return _goldShip; }
            set { _goldShip = value; }
        }
        private Animation _purpleShip;
        public Animation PurpleShip
        {
            get { return _purpleShip; }
            set { _purpleShip = value; }
        }
        private Sprite _shipSelector;
        public Sprite ShipSelector
        {
            get { return _shipSelector; }
            set { _shipSelector = value; }
        }
        private Button _button_MainMenu;
        public Button Button_MainMenu
        {
            get { return _button_MainMenu; }
            set { _button_MainMenu = value; }
        }


        List<Animation> shipList;
        MouseState cursorState;
        Rectangle shipBox;
        private Vector2 center;
        private Vector2 itemDestination;
        // private SpriteFont EquipmentMenuTitle;
        //GraphicsDeviceManager graphics;
        //private string text_EquipmentMenuTitle;
        static public bool MusicStarted = false;

        public EquipmentsMenu(Game game)
        {
            // graphics = new GraphicsDeviceManager(game);
            center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
            _blueShip = new Animation(game, 1, 20, 70);
            _yellowShip = new Animation(game, 1, 20, 70);
            _goldShip = new Animation(game, 1, 20, 70);
            _purpleShip = new Animation(game, 1, 24, 70);
            _button_MainMenu = new Button(game);
            _shipSelector = new Sprite(game);


            _blueShip.Speed = new Vector2(0, 1);
            _yellowShip.Speed = new Vector2(0, 1);
            _goldShip.Speed = new Vector2(0, 1);
            _purpleShip.Speed = new Vector2(0, 1);

            _blueShip.Active = true;
            _yellowShip.Active = true;
            _goldShip.Active = true;
            _purpleShip.Active = true;
            _shipSelector.Active = true;
            shipList = new List<Animation>();

            // text_EquipmentMenuTitle = "";
        }

        public void LoadContent(ContentManager content, string menuMusicName = "")
        {
            _backGround = content.Load<Texture2D>("EquipmentMenu-Items/Equipment-Background");
            _blueShip.LoadContent(content, "EquipmentMenu-Items/BlueShip");
            _yellowShip.LoadContent(content, "EquipmentMenu-Items/YellowShip");
            _goldShip.LoadContent(content, "EquipmentMenu-Items/GreenShip");
            _purpleShip.LoadContent(content, "EquipmentMenu-Items/PurpleShip");
            _button_MainMenu.LoadContent(content, "PauseMenu-Items/MainMenu");
            _shipSelector.LoadContent(content, "EquipmentMenu-Items/ShipSelector2");

            _blueShip.Position = new Vector2(100, 130);
            _yellowShip.Position = new Vector2(_blueShip.Position.X + (_yellowShip.Width + 170), _blueShip.Position.Y);
            _goldShip.Position = new Vector2(_yellowShip.Position.X + (_goldShip.Width + 310), _blueShip.Position.Y);
            _purpleShip.Position = new Vector2(_goldShip.Position.X + (_purpleShip.Width + 200), _blueShip.Position.Y);
            _shipSelector.Position = new Vector2(Game1.windowWidth / 2 - (_shipSelector.Texture.Width / 2), Game1.windowHeight / 2);
            //after position value !!!!!


            _blueShip.Box = new Rectangle((int)_blueShip.Position.X - 10, (int)_blueShip.Position.Y - 10, _blueShip.Width + 20, _blueShip.Height + 20);
            _yellowShip.Box = new Rectangle((int)_yellowShip.Position.X - 10, (int)_yellowShip.Position.Y - 10, _yellowShip.Width + 20, _yellowShip.Height + 20);
            _goldShip.Box = new Rectangle((int)_goldShip.Position.X - 10, (int)_goldShip.Position.Y - 10, _goldShip.Width + 20, _goldShip.Height + 20);
            _purpleShip.Box = new Rectangle((int)_purpleShip.Position.X - 10, (int)_purpleShip.Position.Y - 10, _purpleShip.Width + 20, _purpleShip.Height + 20);

            _button_MainMenu.Texture.Position = new Vector2(10, Game1.windowHeight - (_button_MainMenu.Texture.Height + 10));

            shipList.Add(_blueShip);
            shipList.Add(_yellowShip);
            shipList.Add(_goldShip);
            shipList.Add(_purpleShip);


            _shipSelector.Rec = new Rectangle((int)_shipSelector.Position.X, (int)_shipSelector.Position.Y,
                                                   _shipSelector.Texture.Width, _shipSelector.Texture.Height);
            foreach (Animation ship in shipList)
            {
                ship.Direction = ship.MoveToPoint(ship.Position,
                                      new Vector2(_shipSelector.Rec.Center.ToVector2().X - (ship.Width / 2), _shipSelector.Rec.Center.ToVector2().Y - (ship.Height / 2))); /*new Vector2(_shipSelector.Rec.Center.ToVector2().X, _shipSelector.Rec.Center.ToVector2().Y));-(_shipSelector.Texture.Height/2)));*/
            }

            // EquipmentMenuTitle = content.Load<SpriteFont>("SpriteFonts/");
            //_menuMusic =  content.Load<Song>(menuMusicName);
        }

        public void Update(GameTime gameTime)
        {
            cursorState = Mouse.GetState();
            /*  if (!MusicStarted)
              {
                  MediaPlayer.Play(_menuMusic);
                  MediaPlayer.Volume = 0.5f;
                  MediaPlayer.IsRepeating = true;
                  MusicStarted = true;
              }*/
            /*  _blueShip.UpdateLimitLess_ToRight(gameTime);
              _yellowShip.UpdateLimitLess_ToRight(gameTime);
              _goldShip.UpdateLimitLess_ToRight(gameTime);
              _purpleShip.UpdateLimitLess_ToRight(gameTime);*/

            _button_MainMenu.Update(gameTime);


            foreach (Animation ship in shipList)
            {
                if (!ship.Selected)
                {
                    ship.UpdateLimitLess_ToRight(gameTime);

                    if (ship.Rec.Contains(cursorState.X, cursorState.Y))
                    {
                        if ((ship.Position.Y + ship.Height) > (ship.Box.Y + ship.Box.Height))
                        {
                            ship.Speed = new Vector2(0, 1);
                        }

                        if (ship.Position.Y < ship.Box.Y)
                        {
                            ship.Speed = new Vector2(0, -1);
                        }
                        ship.Position = new Vector2(ship.Position.X - ship.Speed.X, ship.Position.Y - ship.Speed.Y);
                        ship.ClickControls();
                    }
                }

                if (ship.Clicked)
                {
                    ship.Selected = true;
                    _shipSelector.Update(gameTime);
                    itemDestination = new Vector2(_shipSelector.Rec.Center.ToVector2().X - (ship.Width / 2), _shipSelector.Rec.Center.ToVector2().Y - 80);
                    ship.Direction = ship.MoveToPoint(ship.Position, itemDestination);
                  
                    ship.Speed = new Vector2(6, 6);
                    ship.Clicked = false;
                }
                if (ship.Selected)
                {
                    ship.Position += ship.Direction * ship.Speed;
                    if (ship.Position.Y > itemDestination.Y)/*- (_shipSelector.Texture.Height / 2))*/
                    {
                        ship.Selected = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Game1.graphics.GraphicsDevice.Clear(Color.DarkBlue);
            spriteBatch.Draw(_backGround, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), Color.White);
            _blueShip.Draw(spriteBatch);
            _yellowShip.Draw(spriteBatch);
            _goldShip.Draw(spriteBatch);
            _purpleShip.Draw(spriteBatch);
            _shipSelector.Draw(spriteBatch);

            _button_MainMenu.Draw(spriteBatch);
            // spriteBatch.DrawString(EquipmentMenuTitle, text_EquipmentMenuTitle, new Vector2(Game1.windowWidth / 2 - 250, 50), Color.DarkSeaGreen);
        }

    }
}
