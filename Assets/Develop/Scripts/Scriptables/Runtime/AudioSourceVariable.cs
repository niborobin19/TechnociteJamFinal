using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/AudioSource", fileName ="new audio source")]
    public class AudioSourceVariable : ScriptableObject
    {
        #region Exposed

        [Header("References")]
        [SerializeField]
        private AudioSource _source;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onFadeIn;
        
        [SerializeField]
        private ScriptableEvent _onFadeOut;


        #endregion


        #region Properties

        public AudioSource Source
        {
            get => _source;

            set
            {
                _source = value;
            }
        }

        public float FadeTime
        {
            get => _fadeTime;

            private set
            {
                _fadeTime = value;
            }
        }

        #endregion


        #region Unity API

        

        #endregion


        #region Main


        #endregion


        #region Utils

        private void FadeIn(float duration)
        {
            FadeTime = duration;
            _onFadeIn.Raise();
        }

        private void FadeOut(float duration)
        {
            FadeTime = duration;
            _onFadeOut.Raise();
        }

        #endregion


        #region Private
        private float _fadeTime;

        #endregion
    }
}