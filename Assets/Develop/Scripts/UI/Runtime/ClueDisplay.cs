using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using MyCursor.Runtime;

namespace UI.Runtime
{
    public class ClueDisplay : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private ClueVariable _clue;

        [Header("References")]
        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Image _clueImage;

        [SerializeField]
        private Text _clueName;

        [SerializeField]
        private Text _clueDescription;


        #endregion


        #region Unity API

        private void Awake() 
        {
            _clue.OnClueChanged += ClueVariable_OnClueChanged;    
        }

        #endregion


        #region Main

        public void Show()
        {
            CursorManager.ShowCursor();

            _content.SetActive(true);
        }

        public void Hide()
        {
            CursorManager.HideCursor();

            _content.SetActive(false);
        }


        private void ClueVariable_OnClueChanged(ScriptableClue next)
        {
            Show();
            UpdateDisplay(next);
        }

        private void UpdateDisplay(ScriptableClue value)
        {
            _clueImage.sprite = value.Sprite;
            _clueName.text = value.Name;
            _clueDescription.text = value.Description;
        }

        #endregion


        #region Private

        #endregion
    }
}