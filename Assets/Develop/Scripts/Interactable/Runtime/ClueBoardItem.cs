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