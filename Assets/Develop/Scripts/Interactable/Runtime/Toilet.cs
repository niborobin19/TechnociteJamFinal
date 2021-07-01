using System.Collections.Generic;
using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class Toilet : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private StringVariable _response;

        [SerializeField]
        private AudioSourceVariable _voiceChannel;

        [SerializeField]
        private AudioSourceVariable _soundEffectChannel;

        [Header("Variables")]
        [SerializeField]
        private int _interactionLoop;

        [SerializeField]
        private List<string> _responses;

        [SerializeField]
        private List<AudioClip> _responseSpeeches;

        [SerializeField]
        private List<float> _responseSpeechVolume;

        [SerializeField]
        private List<AudioClip> _responseSounds;

        [SerializeField]
        private List<float> _responseSoundVolumeCoefficients;

        #endregion


        #region Unity API

        private void OnValidate() 
        {
            ArrayScaler<string>(ref _responses);
            ArrayScaler<AudioClip>(ref _responseSpeeches);
            ArrayScaler<AudioClip>(ref _responseSounds);
            ArrayScaler<float>(ref _responseSpeechVolume);
            ArrayScaler<float>(ref _responseSoundVolumeCoefficients);
        }

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            var index = _currentInteraction % _interactionLoop;

            _response.Value = _responses[index];

            _voiceChannel.Source.Stop();
            _voiceChannel.Source.clip = _responseSpeeches[index];
            _voiceChannel.Source.Play();

            _soundEffectChannel.Source.PlayOneShot(_responseSounds[index], _responseSoundVolumeCoefficients[index]);

            _currentInteraction++;
        }

        #endregion


        #region Utils

        private void ArrayScaler<T>(ref List<T> list)
        {
            var tmp = new List<T>();

            for (int i = 0; i < _interactionLoop; i++)
            {
                if(i < list.Count)
                {
                    tmp.Add(list[i]);
                }else
                {
                    tmp.Add(default(T));
                }
            }

            list = tmp;
        }

        #endregion

        #region Private

        private int _currentInteraction;

        #endregion
    }
}