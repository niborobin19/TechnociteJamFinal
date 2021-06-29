using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class SuspectBoardItem : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private ScriptableSuspect _suspect;

        [SerializeField]
        private SuspectVariable _displayedSuspect;

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
            _displayedSuspect.Suspect = _suspect;
        }

        public void UpdateDisplay()
        {
            _sprite.sprite = _suspect.Sprite;
            gameObject.SetActive(_suspect.IsDisplayable);
        }

        #endregion


        #region Private

        #endregion
    }
}