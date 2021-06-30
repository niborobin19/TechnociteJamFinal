using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using Game.Runtime;

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
        private ClueAndSuspectRegistry _registy;

        [SerializeField]
        private SuspectLinkButton _suspectButtonPrefab;

        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Image _clueImage;

        [SerializeField]
        private Text _clueName;

        [SerializeField]
        private Text _clueDescription;

        [SerializeField]
        private Transform _suspectLinksRoot;

        #endregion


        #region Unity API

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

            RegenerateSuspects();
        }

        private void RegenerateSuspects()
        {
            for (int i = _suspectLinksRoot.childCount-1; i >= 0; i--)
            {
                Destroy(_suspectLinksRoot.GetChild(i).gameObject);    
            }

            foreach (var suspect in _registy.Suspects)
            {
                if(suspect.IsDisplayable)
                {
                    var button = Instantiate<SuspectLinkButton>(_suspectButtonPrefab, _suspectLinksRoot);
                    button.SetClue(_clue.Clue);
                    button.SetSuspect(suspect);
                    button.UpdateDisplay();
                }
            }
        }

        #endregion


        #region Private

        #endregion
    }
}