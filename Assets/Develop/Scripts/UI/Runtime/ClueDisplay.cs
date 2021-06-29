using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using MyCursor.Runtime;
using Events.Runtime;

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


        public void ClueVariable_OnClueChanged()
        {
            Show();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _clueImage.sprite = _clue.Clue.Sprite;
            _clueName.text = _clue.Clue.Name;
            _clueDescription.text = _clue.Clue.Description;
        }

        #endregion


        #region Private

        #endregion
    }
}