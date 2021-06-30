using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using Game.Runtime;

namespace UI.Runtime
{
    public class DeductionScreenControler : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private SuspectVariable _chargedSuspect;

        [Header("Variables")]
        [SerializeField]
        private float _fadeInTime = 0.5f;

        [SerializeField]
        private float _fadeOutTime = 0.5f;

        [Header("References")]
        [SerializeField]
        private Image _background;

        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _story;

        [SerializeField]
        private GameObject _returnButton;

        [SerializeField]
        private GameObject _nextButton;

        #endregion


        #region Unity API

        private void Awake() 
        {
            SaveInitialColors();
            _alpha = 0.0f;
            UpdateColors();
            gameObject.SetActive(false);
        }

        private void Update() 
        {
            UpdateAlphaThenColor();
        }

        private void OnDisable() 
        {
            _alpha = 0.0f;    
        }

        #endregion


        #region Main

        private void SaveInitialColors()
        {
            _initialBackgroundColor = _background.color;
            
            _initialTitleColor = _title.color;
            _initialStoryColor = _story.color;

            _initialReturnBackgroundColor = _returnButton.GetComponent<Image>().color;
            _initialReturnTextColor = _returnButton.GetComponentInChildren<Text>().color;

            _initialQuitBackgroundColor = _nextButton.GetComponent<Image>().color;
            _initialQuitTextColor = _nextButton.GetComponentInChildren<Text>().color;
        }

        private void UpdateAlphaThenColor()
        {
            if(_fadingIn)
            {
                var amount = 1 * Time.deltaTime / _fadeInTime;
                _alpha += amount;
                _alpha = Mathf.Clamp(_alpha, 0.0f, 1.0f);

                if(Mathf.Approximately(_alpha, 1.0f)) 
                {
                    _fadingIn = false;
                }

                UpdateColors();

                return;
            }
            if(_fadingOut)
            {
                var amount = 1 * Time.deltaTime / _fadeOutTime;
                _alpha -= amount;
                _alpha = Mathf.Clamp(_alpha, 0.0f, 1.0f);

                if(Mathf.Approximately(_alpha, 0.0f))
                {
                    _fadingOut = false;
                    gameObject.SetActive(false);

                    GameManager.CurrentState = GameManager.GameState.Play;
                }

                UpdateColors();

                return;
            }
        }

        private void UpdateColors()
        {
            _background.color = new Color(_initialBackgroundColor.r, _initialBackgroundColor.g, _initialBackgroundColor.b, _alpha);
            
            _title.color = new Color(_initialTitleColor.r, _initialTitleColor.g, _initialTitleColor.b, _alpha);
            _story.color = new Color(_initialStoryColor.r, _initialStoryColor.g, _initialStoryColor.b, _alpha);

            _returnButton.GetComponent<Image>().color = 
                new Color(_initialReturnBackgroundColor.r, _initialReturnBackgroundColor.g, _initialReturnBackgroundColor.g, _alpha);
            _returnButton.GetComponentInChildren<Text>().color = 
                new Color(_initialReturnTextColor.r, _initialReturnTextColor.g, _initialReturnTextColor.b, _alpha);

            _nextButton.GetComponent<Image>().color =
                new Color(_initialQuitBackgroundColor.r, _initialQuitBackgroundColor.g, _initialQuitBackgroundColor.b, _alpha);
            _nextButton.GetComponentInChildren<Text>().color = 
                new Color(_initialQuitTextColor.r, _initialQuitTextColor.g, _initialQuitTextColor.b, _alpha);
        }

        public void ChargedSuspect_OnChanged()
        {
            gameObject.SetActive(true);
            UpdateDisplay();
            GameManager.CurrentState = GameManager.GameState.Pause;
            _fadingOut = false;
            _fadingIn = true;
        }

        public void FadeOut()
        {
            _fadingOut = true;
        }

        #endregion


        #region Utils

        private void UpdateDisplay()
        {
            _title.text = _chargedSuspect.Suspect.DeductionTitle;
            _story.text = _chargedSuspect.Suspect.DeductionStory;
        }

        #endregion


        #region Private

        private bool _fadingIn;
        private bool _fadingOut;
        private float _alpha;
        private Color _initialBackgroundColor;
        private Color _initialTitleColor;
        private Color _initialStoryColor;
        private Color _initialReturnBackgroundColor;        
        private Color _initialReturnTextColor;
        private Color _initialQuitBackgroundColor;      
        private Color _initialQuitTextColor;

        #endregion
    }
}