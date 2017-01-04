using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class EquipmentMenu
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

        private Texture2D backGround;
        private Vector2 center;
        private SpriteFont EquipmentMenuTitle;
        private string text_EquipmentMenuTitle;
        static public bool MusicStarted = false;

        public EquipmentMenu()
        {
            center = new Vector2(Game1.windowWidth / 2, Game1.windowHeight / 2);
            text_EquipmentMenuTitle = "";
        }

        public void LoadContent(ContentManager content)
        {
            backGround = content.Load<Texture2D>("MainMenu-Items/BackGround-StartMenu");
            EquipmentMenuTitle = content.Load<SpriteFont>("SpriteFonts/");
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

            


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), Color.White);
            spriteBatch.DrawString(EquipmentMenuTitle, text_EquipmentMenuTitle, new Vector2(Game1.windowWidth / 2 - 250, 50), Color.DarkSeaGreen);
        }

    }
}
