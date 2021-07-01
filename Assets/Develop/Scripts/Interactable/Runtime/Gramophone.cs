using UnityEngine;
using Scriptables.Runtime;
using SoundManager.Runtime;

namespace Interactable.Runtime 
{
    public class Gramophone : MonoBehaviour, IInteractable
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private AudioSourceVariable _clicChannel;

        [Header("Variables")]
        [SerializeField]
        private float _transitionTime;

        [SerializeField]
        private AudioClip _clic;

        [SerializeField]
        private float _clicVolumeCoefficient = 1.0f;

        [Header("References")]
        [SerializeField]
        private AudioSource _source;

        #endregion


        #region Unity API

        private void Awake() 
        {
            _currentPitch = 0.0f;
            UpdatePitch();
        }

        private void Update() 
        {
            TryTuneUp();
            TryTuneDown();    
        }

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            _clicChannel.Source.PlayOneShot(_clic, _clicVolumeCoefficient);

            ToggleMusic();
        }

        private void TryTuneUp()
        {
            if(_starting)
            {
                var deltaPitch = Time.deltaTime / _transitionTime;
                _currentPitch += deltaPitch;
                _currentPitch = Mathf.Clamp01(_currentPitch);

                UpdatePitch();

                if(Mathf.Approximately(_currentPitch, 1.0f))
                {
                    _starting = false;
                }
            }
        }

        private void TryTuneDown()
        {
            if(_stopping)
            {
                var deltaPitch = Time.deltaTime / _transitionTime;
                _currentPitch -= deltaPitch;
                _currentPitch = Mathf.Clamp01(_currentPitch);

                UpdatePitch();

                if(Mathf.Approximately(_currentPitch, 0.0f))
                {
                    if(!_source.isPlaying) _source.Pause();
                    _stopping = false;
                }
            }
        }

        private void UpdatePitch()
        {
            _source.pitch = _currentPitch;
        }


        private void ToggleMusic()
        {
            if(!_isOn)
            {
                ToggleMusicOn();
            }
            else
            {
                ToggleMusicOff();
            }
        }

        private void ToggleMusicOn()
        {
            if(!_source.isPlaying) _source.UnPause();
            _isOn = true;
            _starting = true;
            _stopping = false;
        }

        private void ToggleMusicOff()
        {
            _isOn = false;
            _starting = false;
            _stopping = true;
        }

        #endregion


        #region Private

        private bool _isOn;
        private bool _starting;
        private bool _stopping;
        private float _currentPitch;

        #endregion
    }
}