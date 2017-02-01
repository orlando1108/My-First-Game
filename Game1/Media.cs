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
        // SoundEffect fire;
        //Song explosionShip_Music;
        // Song playing_Music;
        // Song loading_Music;
        // Song paused_Music;

        private SoundEffectInstance _soundEffectInstance;
        public SoundEffectInstance SoundEffectInstance
        {
            get { return _soundEffectInstance; }
            set { _soundEffectInstance = value; }
        }
        /*
        private SoundEffect _fireship_Sound;
        public SoundEffect FireShip_Sound
        {
            get { return _fireship_Sound; }
            set { _fireship_Sound = value; }
        }

        private Song _explosionShip_Music;
        public Song ExplosionShip_Music
        {
            get { return _explosionShip_Music; }
            set { _explosionShip_Music = value; }
        }*/

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

        private float _volumeMusic;
        public float VolumeMusic
        {
            get { return _volumeMusic; }
            set { _volumeMusic = value; }
        }

        private float _volumeSound;
        public float VolumeSound
        {
            get { return _volumeSound; }
            set { _volumeSound = value; }
        }
        
        private bool _musicActive;
        public bool MusicActive
        {
            get { return _musicActive; }
            set { _musicActive = value; }
        }

        private bool _soundActive;
        public bool SoundActive
        {
            get { return _soundActive; }
            set { _soundActive = value; }
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
            _volumeMusic = 1.0f;
            _volumeSound = 1.0f;
            this._musicActive = true;
            this._soundActive = true;
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
        public void Update()
        {
            _volumeMusic = Settings._VolumeMusic;
            _volumeSound = Settings._VolumeSound;
            this._musicActive = Settings._MusicActive;
            this._soundActive = Settings._SoundActive;
        }

        public  void PlayGameMusics(Game1.GameStates gameStates)
        {
            if (_musicActive)
            {
                if(oldGameState != gameStates || MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Volume = _volumeMusic;
                    MediaPlayer.Play(GameMusics[gameStates]);
                }

            }
            else
            {
                MediaPlayer.Stop();
            }
            oldGameState = gameStates;
        }

        public void PlaySound(SoundEffect sound)
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
            if (_soundActive)
            {
                //change it later
                MediaPlayer.Volume = _volumeSound;
                MediaPlayer.IsRepeating = false;
                MediaPlayer.Play(song);
               
            }
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
