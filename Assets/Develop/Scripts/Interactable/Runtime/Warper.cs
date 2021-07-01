using UnityEngine;
using Events.Runtime;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class Warper : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private AudioSourceVariable _channel;

        [Header("Variables")]
        [SerializeField]
        private float _warpDelay;

        [SerializeField]
        private AudioClip _warpSound;
        
        [SerializeField]
        private float _warpSoundVolumeCoefficient = 1.0f;

        [Header("References")]
        [SerializeField]
        private Transform _destination;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onWarping;

        #endregion


        #region Unity API

        public void Interacted(Object source)
        {
            _target = ((MonoBehaviour)source).gameObject.transform;
            _channel.Source.PlayOneShot(_warpSound, _warpSoundVolumeCoefficient);
            _onWarping.Raise();

            Invoke(nameof(Warp), _warpDelay); 
        }

        private void Warp()
        {
            _target.position = _destination.position;
            _target.rotation = _destination.rotation;
        }

        #endregion


        #region Private

        private Transform _target;

        #endregion
    }
}