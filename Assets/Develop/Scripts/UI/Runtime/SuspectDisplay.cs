using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using MyCursor.Runtime;

namespace UI.Runtime
{
    public class SuspectDisplay : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private SuspectVariable _suspect;

        [Header("References")]
        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Image _suspectImage;

        [SerializeField]
        private Text _suspectName;

        [SerializeField]
        private Text _suspectDescription;

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


        public void SuspectVariable_OnClueChanged()
        {
            Show();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _suspectImage.sprite = _suspect.Suspect.Sprite;
            _suspectName.text = _suspect.Suspect.Name;
            _suspectDescription.text = _suspect.Suspect.Description;
        }

        #endregion


        #region Private

        #endregion
    }
}