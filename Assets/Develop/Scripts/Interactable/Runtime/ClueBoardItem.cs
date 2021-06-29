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

        #endregion


        #region Private

        #endregion
    }
}