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
    class MainMenu
    {
        private Button _button_Play;
        public Button Button_Play
        {
            get { return _button_Play; }
            set { _button_Play = value; }
        }

        private Button _button_Equipments;
        public Button Button_Equipments
        {
            get { return _button_Equipments; }
            set { _button_Equipments = value; }
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
        
      
        private SpriteFont gameTitle;
        private string text_GameTitle;
        private Texture2D backGround;
        private Vector2 center;
        static public bool MusicStarted = false;
        enum MainMenuStates
        {
            MainMenu,
            Equipment,
            Settings,
            Quit,
        }
        MainMenuStates _mainMenuState { get; set; }
        EquipmentsMenu equipmentMenu;
        SettingsMenu settingsMenu;
      
        
        public MainMenu(Game game, Media media)
        {
            center = new Vector2(Settings._WindowWidth / 2, Settings._WindowHeight / 2);
           
            _button_Play = new Button(game);
            _button_Equipments = new Button(game);
            _button_Settings = new Button(game);
            _button_Quit = new Button(game);
            equipmentMenu = new EquipmentsMenu(game);
            settingsMenu = new SettingsMenu(game, media);
           
        ;

            _mainMenuState = MainMenuStates.MainMenu;
            text_GameTitle = "             GALACTOR\nThe best video game of all time !\n     Et ouais ma gueule !!!";
            // _buttonResume.Moving = false;
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager content)
        {
            backGround = content.Load<Texture2D>("MainMenu-Items/BackGround-StartMenu");
            _button_Play.LoadContent(content, "MainMenu-Items/Play");
            _button_Settings.LoadContent(content, "MainMenu-Items/Settings");
            _button_Equipments.LoadContent(content, "MainMenu-Items/Equipments");
            _button_Quit.LoadContent(content, "MainMenu-Items/Quit");
            //_menuMusic = content.Load<Song>(menuMusicName);
            gameTitle = content.Load<SpriteFont>("SpriteFonts/GAME-TITLE");
            equipmentMenu.LoadContent(content);
            settingsMenu.LoadContent(content);

            //set positions in order to their texture and the other buttons
            _button_Equipments.Texture.Position = new Vector2(center.X -(_button_Equipments.Texture.Width/2), center.Y-100);
            _button_Settings.Texture.Position = new Vector2(_button_Equipments.Texture.Position.X, _button_Equipments.Texture.Position.Y + 70);
            _button_Play.Texture.Position = new Vector2(_button_Settings.Texture.Position.X, _button_Settings.Texture.Position.Y + 70);
            _button_Quit.Texture.Position = new Vector2(_button_Play.Texture.Position.X, _button_Play.Texture.Position.Y + 120);
           

        }

        public void Update(GameTime gameTime, Settings settings)
        {
            if (_mainMenuState == MainMenuStates.MainMenu)
            {
                

                _button_Play.Update(gameTime);
                _button_Settings.Update(gameTime);
                _button_Equipments.Update(gameTime);
                _button_Quit.Update(gameTime);

                if (_button_Play.Clicked)
                {
                    Game1._gameState = Game1.GameStates.Playing;
                    _button_Play.Clicked = false;
                }
                if (_button_Equipments.Clicked)
                {
                    _mainMenuState = MainMenuStates.Equipment;
                    _button_Equipments.Clicked = false;
                }
                if (_button_Settings.Clicked)
                {
                    _mainMenuState = MainMenuStates.Settings;
                    _button_Settings.Clicked = false;
                }
            }
            if(_mainMenuState == MainMenuStates.Equipment)
            {
                equipmentMenu.Update(gameTime);
                if (equipmentMenu.Button_MainMenu.Clicked)
                {
                    _mainMenuState = MainMenuStates.MainMenu;
                    equipmentMenu.Button_MainMenu.Clicked = false;
                }  
            }
            if(_mainMenuState == MainMenuStates.Settings)
            {
                settingsMenu.Update(gameTime, settings);
                if (settingsMenu.Button_MainMenu.Clicked)
                {
                    _mainMenuState = MainMenuStates.MainMenu;
                    settingsMenu.Button_MainMenu.Clicked = false;
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_mainMenuState == MainMenuStates.MainMenu)
            {
                spriteBatch.Draw(backGround, new Rectangle(0, 0, Settings._WindowWidth, Settings._WindowHeight), Color.White);
                spriteBatch.DrawString(gameTitle, text_GameTitle, new Vector2(Settings._WindowWidth / 2 - 250, 50), Color.DarkSeaGreen);

                _button_Equipments.Draw(spriteBatch);
                _button_Settings.Draw(spriteBatch);
                _button_Play.Draw(spriteBatch);
                _button_Quit.Draw(spriteBatch);
            }
                

            if (_mainMenuState == MainMenuStates.Equipment)
            {
                equipmentMenu.Draw(spriteBatch);
            }
            if(_mainMenuState == MainMenuStates.Settings)
            {
                settingsMenu.Draw(spriteBatch);
            }
            

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
