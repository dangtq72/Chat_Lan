using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

namespace Nvs_Monitor
{
    public class SoundEffect
    {
        string _soundFile;
        Thread _soundThread;
        bool _isStopped = true;
        SoundPlayer _SoundPlayer;

        public bool IsFinished { get { return _isStopped; } }

        public SoundEffect(string soundFile)
        {
            _soundFile = soundFile;
            _SoundPlayer = new System.Media.SoundPlayer(_soundFile);
        }

        public void PlaySync()
        {
            if (!_isStopped)
                return;

            _soundThread = new Thread(PlayThread);
            _soundThread.Start();
        }

        public void PlayLooping()
        {
            _isStopped = false;
            _SoundPlayer.PlayLooping();
        }

        public void Play()
        {
            _SoundPlayer.Play();
            _isStopped = false;
        }

        public void Stop()
        {
            _SoundPlayer.Stop();
            _isStopped = true;
        }

        private void PlayThread()
        {
            _isStopped = false;
            _SoundPlayer.PlaySync();
            _isStopped = true;
        }
    }
}
