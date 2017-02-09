using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Media
    {

        private static SoundEffectInstance _soundEffectInstance;
        private bool _musicStarted;
       
        private Song _playing_Music;
        public Song Playing_Music
        {
            get { return _playing_Music; }
            set { _playing_Music = value; }
        }

        private Song _loading_Music;
        public Song Loading_Music
        {
            get { return _loading_Music; }
            set { _loading_Music = value; }
        }

        private Song _paused_Music;
        public Song Paused_Music
        {
            get { return _paused_Music; }
            set { _paused_Music = value; }
        }

        private static float _volumeMusic
        {
            get { return Settings._VolumeMusic; }
           
        }

        private static float _volumeSound
        {
            get { return Settings._VolumeSound; }
           
        }


        private static bool _musicActive
        {
            get { return Settings._MusicActive; }
            
        }


        private static bool _soundActive
        {
            get { return Settings._SoundActive; }
            
        }


        Dictionary<Game1.GameStates, Song> GameMusics;
        //List<SoundEffect> soundsList;
        //bool sameMusic;
        Game1.GameStates oldGameState;

        public Media()
        {
            //soundsList = new List<SoundEffect>();
            //this.sameMusic = false;
            oldGameState = Game1.GameStates.Paused;
            MediaPlayer.IsRepeating = true;
            _musicStarted = false;
           /* _volumeMusic = 1.0f;
            _volumeSound = 1.0f;
            _musicActive = true;
            _soundActive = true;*/
        }

        public void LoadContent(ContentManager content)
        {
            this._playing_Music = content.Load<Song>("Sounds-Musics/Redemption");
            this._loading_Music = content.Load<Song>("Sounds-Musics/menuMusic");
            this._paused_Music = content.Load<Song>("Sounds-Musics/pauseMenuMusic");

            GameMusics = new Dictionary<Game1.GameStates, Song>()
            {
                { Game1.GameStates.Loading, _loading_Music},
                { Game1.GameStates.Playing, _playing_Music},
                { Game1.GameStates.Paused, _paused_Music}
            };
        }


        public void PlayGameMusics(Game1.GameStates gameStates)
        {
            if (_musicActive)
            {
                //to allow update on music volume
                MediaPlayer.Volume = _volumeMusic;
                if (oldGameState != gameStates || MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(GameMusics[gameStates]);
                }
            }
            else
            {
                MediaPlayer.Stop();
            }
            oldGameState = gameStates;
        }

        public static void PlaySound(SoundEffect sound)
        {
            if (_soundActive)
            {
                _soundEffectInstance = sound.CreateInstance();
                _soundEffectInstance.Volume = _volumeSound;
                _soundEffectInstance.Play();

            }
        }
        //for the moment its to play a music like a soundEffect
        public void PlayMusic(Song song)
        {
            if (_soundActive && !_musicStarted )
            {
                //change it later
                MediaPlayer.Volume = _volumeSound;
                MediaPlayer.IsRepeating = false;
                MediaPlayer.Play(song);
                _musicStarted = true;

            }
          /*  if(MediaPlayer.State == MediaState.Stopped)
            {
                _musicStarted = false;
            }*/
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public void StopSound()
        {
            _soundEffectInstance.Stop();
        }
    }
}
