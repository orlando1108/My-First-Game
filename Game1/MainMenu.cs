﻿using Microsoft.Xna.Framework;
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
    class MainMenu
    {
        private Button _button_Play;
        public Button Button_Play
        {
            get { return _button_Play; }
            set { _button_Play = value; }
        }

        private Button _button_YourShip;
        public Button Button_YourShip
        {
            get { return _button_YourShip; }
            set { _button_YourShip = value; }
        }

        private Button _button_Settings;
        public Button Button_Settings
        {
            get { return _button_Settings; }
            set { _button_Settings = value; }
        }

        private Button _button_Quit;
        public Button Button_Quit
        {
            get { return _button_Quit; }
            set { _button_Quit = value; }
        }
        
        private Song _menuMusic;
        public Song MenuMusic
        {
            get
            {
                return _menuMusic;
            }
            set { _menuMusic = value; }
        }

        
        private SpriteFont gameTitle;
        private string text_GameTitle;
        private Texture2D backGround;
        private Vector2 center;
        
        static public bool MusicStarted = false;

        public MainMenu(Game game)
        {
            center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
            _button_Play = new Button(game);
            _button_YourShip = new Button(game);
            _button_Settings = new Button(game);
            _button_Quit = new Button(game);
            text_GameTitle = "             GALACTOR\nThe best video game of all time !\n     Et ouais ma gueule !!!";
            // _buttonResume.Moving = false;
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager content, String menuMusicName)
        {
            backGround = content.Load<Texture2D>("MainMenu-Items/BackGround-StartMenu");
            _button_Play.LoadContent(content, "MainMenu-Items/Play");
            _button_Settings.LoadContent(content, "MainMenu-Items/Settings");
            _button_YourShip.LoadContent(content, "MainMenu-Items/Your Ship");
            _button_Quit.LoadContent(content, "MainMenu-Items/Quit");

            //set positions in order to their texture and the other buttons
            _button_YourShip.Texture.Position = new Vector2(center.X -(_button_YourShip.Texture.Width/2), center.Y-100);
            _button_Settings.Texture.Position = new Vector2(_button_YourShip.Texture.Position.X, _button_YourShip.Texture.Position.Y + 70);
            _button_Play.Texture.Position = new Vector2(_button_Settings.Texture.Position.X, _button_Settings.Texture.Position.Y + 70);
            _button_Quit.Texture.Position = new Vector2(_button_Play.Texture.Position.X, _button_Play.Texture.Position.Y + 120);
            _menuMusic = content.Load<Song>(menuMusicName);
            gameTitle = content.Load<SpriteFont>("SpriteFonts/GAME-TITLE");

        }

        public void Update(GameTime gameTime)
        {
            if (!MusicStarted)
            {
                MediaPlayer.Play(_menuMusic);
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.IsRepeating = true;
                MusicStarted = true;
            }

            _button_Play.Update(gameTime);
            _button_Settings.Update(gameTime);
            _button_YourShip.Update(gameTime);
            _button_Quit.Update(gameTime);

            if(_button_Play.Clicked == true)
            {
                Game1._gameState = Game1.GameStates.Playing;
                _button_Play.Clicked = false;
            }
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), Color.White);
            spriteBatch.DrawString(gameTitle, text_GameTitle, new Vector2(Game1.windowWidth/2 - 250, 50), Color.DarkSeaGreen);

            _button_YourShip.Draw(spriteBatch);
            _button_Settings.Draw(spriteBatch);
            _button_Play.Draw(spriteBatch);
            _button_Quit.Draw(spriteBatch);

        }
        
       /* private void ResumeGame_ByKeyPress()
        {
            //GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
            KeyboardState state = Keyboard.GetState();

            if (!state.IsKeyDown(Keys.P))
            {
                oldState = state;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit();

            if (oldState != state && state.IsKeyDown(Keys.P))
            {
                Game1._gameState = Game1.GameStates.Playing;
                MediaPlayer.Stop();
                MusicStarted = false;
                oldState = state;

            }
        }*/
    }
}
