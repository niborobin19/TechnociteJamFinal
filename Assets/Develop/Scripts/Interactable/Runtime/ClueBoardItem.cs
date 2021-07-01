using System.Collections.Generic;
using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class ClueBoardItem : MonoBehaviour, IInteractable
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

        [Header("References")]
        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private Transform _pin;

        #endregion


        #region Unity API

        private void Awake() 
        {
            UpdateDisplay();  
        }

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            _displayedClue.Clue = _clue;
            _channel.Source.PlayOneShot(_interactionSound, _interactionVolumeCoefficient);
        }

        public void UpdateDisplay()
        {
            _sprite.sprite = _clue.Sprite;
            gameObject.SetActive(_clue.IsDiscovered);
        }

        public Transform GetPin()
        {
            return _pin;
        }

        public ScriptableClue GetClue()
        {
            return _clue;
        }

        #endregion


        #region Private


        #endregion
    }
}