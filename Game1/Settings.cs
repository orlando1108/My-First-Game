using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Settings
    {
        public static float _VolumeMusic { get; set; }
        public static float _VolumeSound { get; set; }
        public static int _WindowWidth { get; set; }
        public static int _WindowHeight { get; set; }
        public static bool _MusicActive { get; set; }
        public static bool _SoundActive { get; set; }

        /*  private static float _volumeMusic;
          public static float VolumeMusic
          {
              get { return _volumeMusic; }
              set { _volumeMusic = value; }
          }
          private static float _volumeSound;
          public static float VolumeSound
          {
              get { return _volumeSound; }
              set { _volumeSound = value; }
          }

          private static bool _musicActive;
          public static bool MusicActive
          {
              get { return _musicActive; }
              set { _musicActive = value; }
          }

          private static bool _soundActive;
          public static bool SoundActive
          {
              get { return _soundActive; }
              set { _soundActive = value; }
          }


          private static int _windowWidth;
          public static int WindowWidth
          {
              get { return _windowWidth; }
              set { _windowWidth = value; }
          }

          private static int _windowHeight;
          public static int WindowHeight
          {
              get { return _windowHeight; }
              set { _windowHeight = value; }
          }*/


        public Settings(int windowWidth, int windowHeight, float volumeMusic, float volumeSound, bool musicActive, bool soundActive)
        {
            _WindowWidth = windowWidth;
            _WindowHeight = windowHeight;
            _VolumeMusic = volumeMusic;
            _VolumeSound = volumeSound;
            _MusicActive = musicActive;
            _SoundActive = soundActive;
        }

        public static void Update(int windowWidth, int windowHeight, float volumeMusic, float volumeSound, bool musicActive, bool soundActive)
        {
            _WindowWidth = windowWidth;
            _WindowHeight = windowHeight;
            _VolumeMusic = volumeMusic;
            _VolumeSound = volumeSound;
            _MusicActive = musicActive;
            _SoundActive = soundActive;
        }
        
    }
}
