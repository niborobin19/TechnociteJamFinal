using System;
using UnityEngine;
using Scriptables.Runtime;

namespace SoundManager.Runtime
{
    public class AudioSourceControler : MonoBehaviour
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private AudioSourceVariable _audioSourceVariable;

        [Header("Variables")]
        [SerializeField]
        private float _initialVolumeRatio;

        [SerializeField]
        private bool _fadeInOnAwake;

        [SerializeField]
        private float _fadeTimeAwake;

        [Header("References")]
        [SerializeField]
        private AudioSource _audioSource;

        #endregion


        #region Unity API

        private void Awake() 
        {
            _audioSourceVariable.Source = _audioSource;
            _currentRatio = _initialVolumeRatio;
            _initialVolume = _audioSource.volume;

            if(_fadeInOnAwake)
            {
                _fadingIn = true;
                _fadeTime = _fadeTimeAwake;
            }
        }

        private void Update() 
        {
            TryFadeIn();
            TryFadeOut();
        }

        #endregion


        #region Main

        private void TryFadeIn()
        {
            if(_fadingIn)
            {
                var deltaRatio = Time.deltaTime / _fadeTime;
                _currentRatio += deltaRatio;
                _currentRatio = Mathf.Clamp01(_currentRatio);

                _audioSourceVariable.Source.volume = _initialVolume * _currentRatio;

                if(IsFullyAudible())
                {
                    _fadingIn = false;
                }
            }
        }

        private void TryFadeOut()
        {
            if(_fadingOut)
            {
                var deltaRatio = Time.deltaTime / _fadeTime;
                _currentRatio -= deltaRatio;
                _currentRatio = Mathf.Clamp01(_currentRatio);

                _audioSourceVariable.Source.volume = _initialVolume * _currentRatio;

                if(IsMuted())
                {
                    _fadingOut = false;
                    Pause();
                }
            }
        }

        private bool IsMuted()
        {
            return Mathf.Approximately(_currentRatio, 0.0f);
        }

        private bool IsFullyAudible()
        {
            return Mathf.Approximately(_currentRatio, 1.0f);
        }

        private void Pause()
        {
            _audioSourceVariable.Source.Pause();
        }

        public void AudioSourceVariable_OnFadeIn()
        {
            _audioSourceVariable.Source.UnPause();
            _fadeTime = _audioSourceVariable.FadeTime;
            _fadingIn = true;
            _fadingOut = false;
        }

        public void AudioSourceVariable_OnFadeOut()
        {
            _fadeTime = _audioSourceVariable.FadeTime;
            _fadingIn = false;
            _fadingOut = true;
        }

        #endregion


        #region Private

        private bool _fadingIn;
        private bool _fadingOut;

        private float _initialVolume;
        private float _fadeTime;
        private float _currentRatio;

        #endregion
    }
}