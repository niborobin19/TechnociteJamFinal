using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;

namespace UI.Runtime
{
    public class SuspectLinkButton : MonoBehaviour
    {
        #region Exposed
        [Header("Variables")]

        [SerializeField]
        private Color _unlinkedColor;
        
        [SerializeField]
        private Color _linkedColor;
        
        [Header("References")]
        [SerializeField]
        private Image _backGround;

        [SerializeField]
        private Image _foreGround;

        #endregion


        #region Unity API

        private void Start() 
        {
            Square();
        }

        #endregion


        #region Main

        public void UpdateDisplay()
        {
            _foreGround.sprite = _suspect.Sprite;
            _backGround.color = _suspect.HasLinkTo(_clue) ? _linkedColor : _unlinkedColor; 
        }

        public void ToggleLink()
        {
            if(_suspect.HasLinkTo(_clue))
            {
                _suspect.UnlinkClue(_clue);
            }
            else
            {
                _suspect.LinkClue(_clue);
            }

            UpdateDisplay();
        }

        private void Square()
        {
            var rect = (_backGround.rectTransform.parent as RectTransform).rect;
            Debug.Log(rect);
            rect.size = new Vector2(rect.height, rect.height);

            _backGround.rectTransform.sizeDelta = rect.size;
        }

        public void SetSuspect(ScriptableSuspect suspect)
        {
            _suspect = suspect;
        }

        public void SetClue(ScriptableClue clue)
        {
            _clue = clue;
        }

        #endregion


        #region Private

        private ScriptableSuspect _suspect;

        private ScriptableClue _clue;

        #endregion
    }
}