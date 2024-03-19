using System.Collections.Generic;
using Model.Core.Sound;
using UnityEngine;

namespace Plugins.Scripts.Core.Common.Sound
{
    public class SoundManager : SingletonMonoDontDestroy<SoundManager>
    {
        public SoundManager(string className) : base(className)
        {
        }

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;

        private Dictionary<SoundType, AudioClip> _allAudios;

        #region API

        public AudioClip GetAudioClip(SoundType soundType)
        {
            if (_allAudios == null)
            {
                _allAudios = new Dictionary<SoundType, AudioClip>();
            }

            if (_allAudios.ContainsKey(soundType))
            {
                return _allAudios[soundType];
            }

            var audioClip = LoadResourceController.Instance.LoadAudioClip(soundType);
            if (audioClip != null)
            {
                _allAudios[soundType] = audioClip;
            }

            return audioClip;
        }

        public void PlayBackgroundMusic(SoundType soundType)
        {
            var audioClip = GetAudioClip(soundType);
            musicSource.clip = audioClip;
            musicSource.Play();
        }

        public void PlayTimeMusic(SoundType soundType)
        {
            var audioClip = GetAudioClip(soundType);
            musicSource.clip = audioClip;
            musicSource.Play();
        }
        
        public void PlayDollySong(SoundType soundType, float a)
        {
            var audioClip = GetAudioClip(soundType);
            musicSource.clip = audioClip;
            musicSource.pitch = 6.519f/ a;
            musicSource.Play();
        }
        
        public void PauseDollySong(SoundType soundType)
        {
            var audioClip = GetAudioClip(soundType);
            musicSource.clip = audioClip;
            musicSource.pitch = 1;
            musicSource.Stop();
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }

        public void ResumeMusic()
        {
            musicSource.UnPause();
        }

        public void PlaySound(SoundType soundType)
        {
            var audioClip = GetAudioClip(soundType);
            soundSource.PlayOneShot(audioClip);
        }

        #endregion

        private void Start()
        {
            InitData();
            this.RegisterListener(EventID.OnSoundChange, (sender, param) => OnSoundChange());
            this.RegisterListener(EventID.OnMusicChange, (sender, param) => OnMusicChange());
        }

        private void InitData()
        {
            OnSoundChange();
            OnMusicChange();
        }

        private void OnSoundChange()
        {
            //soundSource.mute = DataAccountPlayer.PlayerSettings.SoundOff;
        }

        private void OnMusicChange()
        {
            //musicSource.mute = DataAccountPlayer.PlayerSettings.MusicOff;
        }
    }
}