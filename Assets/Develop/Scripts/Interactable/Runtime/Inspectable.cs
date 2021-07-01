using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class Inspectable : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private AudioSourceVariable _channel;

        [Header("Variables")]
        [SerializeField, TextArea]
        private string _responseText;

        [SerializeField]
        private AudioClip _responseAudio;
        
        [SerializeField]
        private float _responseVolume = 1.0f;

        [Header("Datas")]
        [SerializeField]
        private StringVariable _displayedText;

        #endregion


        #region Unity API

        #endregion


        #region Main
        public void Interacted(Object source)
        {
            _displayedText.Value = _responseText;

            _channel.Source.Stop();
            _channel.Source.clip = _responseAudio;
            _channel.Source.volume = _responseVolume;
            _channel.Source.Play();
        }

        #endregion


        #region Private

        #endregion
    }
}