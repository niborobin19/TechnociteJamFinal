using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class Clue : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private ScriptableClue _clue;

        [SerializeField]
        private ClueVariable _displayedClue;

        [SerializeField]
        private AudioSourceVariable _channel;

        [Header("Variables")]
        [SerializeField]
        private AudioClip _interactionSound;

        [SerializeField]
        private float _interactionVolumeCoefficient = 1.0f;

        #endregion


        #region Unity API

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            _clue.IsDiscovered = true;
            _displayedClue.Clue = _clue;
            _channel.Source.PlayOneShot(_interactionSound, _interactionVolumeCoefficient);
            
        }

        #endregion


        #region Private

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        #endregion
    }
}