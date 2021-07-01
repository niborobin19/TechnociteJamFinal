using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class ToggleableLight : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private AudioSourceVariable _channel;

        [Header("Variables")]
        [SerializeField]
        private AudioClip _clic;

        [SerializeField]
        private float _clicVolumeCoefficient = 1.0f;

        [Header("References")]
        [SerializeField]
        private Light _light;

        #endregion


        #region Main
    
        public void Interacted(Object source)
        {
            _light.enabled = !_light.enabled;
            _channel.Source.PlayOneShot(_clic, _clicVolumeCoefficient);
        }

        #endregion


        #region Private

        #endregion
    }
}