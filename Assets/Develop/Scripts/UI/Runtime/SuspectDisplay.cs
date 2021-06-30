using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using Game.Runtime;

namespace UI.Runtime
{
    public class SuspectDisplay : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private SuspectVariable _selectedSuspect;
        [SerializeField]
        private SuspectVariable _chargedSuspect;

        [Header("References")]
        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Image _suspectImage;

        [SerializeField]
        private Text _suspectName;

        [SerializeField]
        private Text _suspectDescription;

        [SerializeField]
        private Button _chargeButton;

        #endregion


        #region Main

        public void Show()
        {
            GameManager.CurrentState = GameManager.GameState.Pause;

            _content.SetActive(true);
        }

        public void Hide()
        {
            GameManager.CurrentState = GameManager.GameState.Play;

            _content.SetActive(false);
        }


        public void SuspectVariable_OnClueChanged()
        {
            Show();
            UpdateDisplay();
        }

        public void ChargeButton()
        {
            Hide();
            _chargedSuspect.Suspect = _selectedSuspect.Suspect;
        }

        private void UpdateDisplay()
        {
            _suspectImage.sprite = _selectedSuspect.Suspect.Sprite;
            _suspectName.text = _selectedSuspect.Suspect.Name;
            _suspectDescription.text = _selectedSuspect.Suspect.Description;
            _chargeButton.interactable = _selectedSuspect.Suspect.IsChargeable;
        }

        #endregion


        #region Private

        #endregion
    }
}