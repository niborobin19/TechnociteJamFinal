using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;
using Game.Runtime;

namespace UI.Runtime
{
    public class MainMenuControler : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private AudioSourceVariable _channel;

        [Header("Variables")]
        [SerializeField]
        private float _fadeInTime = 0.5f;

        [SerializeField]
        private float _fadeOutTime = -1f;

        [SerializeField]
        private AudioClip _continueSound;

        [SerializeField]
        private float _continueSoundVolumeCoefficient;

        [Header("References")]
        [SerializeField]
        private GameObject _mainPanel;

        [SerializeField]
        private GameObject _transitionPanel;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private Text _text;

        [SerializeField]
        private GameObject _continueButton;
        #endregion


        #region Unity API

        private void Awake() 
        {
            SaveInitialColors();
            _alpha = 0.0f;
            _fadeOutTime = _fadeOutTime > 0 ? _fadeOutTime : _continueSound.length;
            UpdateColors();

            _continueButton.GetComponent<Button>().interactable = false;
            _mainPanel.SetActive(true);
            _transitionPanel.SetActive(false);
        }

        private void Update() 
        {
            UpdateAlphaThenColor();
        }

        #endregion


        #region Main

        public void OnPlayButton()
        {
            FadeIntroductionIn();
        }

        public void OnContinueButton()
        {
            FadeIntroductionOut();
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }

         private void SaveInitialColors()
        {
            _initialBackgroundColor = _background.color;
            
            _initialTextColor = _text.color;

            _initialContinueBackgroundColor = _continueButton.GetComponent<Image>().color;
            _initialContinueTextColor = _continueButton.GetComponentInChildren<Text>().color;
        }

        private void UpdateAlphaThenColor()
        {
            if(_fadingIn)
            {
                var amount = Time.deltaTime / _fadeInTime;
                _alpha += amount;
                _alpha = Mathf.Clamp(_alpha, 0.0f, 1.0f);

                if(Mathf.Approximately(_alpha, 1.0f)) 
                {
                    OnEndFadeIn();
                }

                UpdateColors();

                return;
            }
            if(_fadingOut)
            {
                var amount = Time.deltaTime / _fadeOutTime;
                _alpha -= amount;
                _alpha = Mathf.Clamp(_alpha, 0.0f, 1.0f);

                if(Mathf.Approximately(_alpha, 0.0f))
                {
                    OnEndFadeOut();
                }

                UpdateColors();

                return;
            }
        }

        private void UpdateColors()
        {
            _background.color = new Color(_initialBackgroundColor.r, _initialBackgroundColor.g, _initialBackgroundColor.b, _alpha);
            
            _text.color = new Color(_initialTextColor.r, _initialTextColor.g, _initialTextColor.b, _alpha);

            _continueButton.GetComponent<Image>().color = 
                new Color(_initialContinueBackgroundColor.r, _initialContinueBackgroundColor.g, _initialContinueBackgroundColor.g, _alpha);
            _continueButton.GetComponentInChildren<Text>().color = 
                new Color(_initialContinueTextColor.r, _initialContinueTextColor.g, _initialContinueTextColor.b, _alpha);

        }

        public void FadeIntroductionIn()
        {
            _background.gameObject.SetActive(true);

            _fadingOut = false;
            _fadingIn = true;
        }

        public void FadeIntroductionOut()
        {
            _channel.Source.PlayOneShot(_continueSound, _continueSoundVolumeCoefficient);

            _fadingOut = true;
            _fadingIn = false;
        }

        private void OnEndFadeIn()
        {
            _fadingIn = false;

            _mainPanel.SetActive(false);
            _continueButton.GetComponent<Button>().interactable = true;
        }

        private void OnEndFadeOut()
        {
            _fadingOut = false;

            _background.gameObject.SetActive(false);
            gameObject.SetActive(false);

            GameManager.CurrentState = GameManager.GameState.Play;

        }

        #endregion


        #region Private

        private bool _fadingIn;
        private bool _fadingOut;
        private float _alpha;
        private Color _initialBackgroundColor;
        private Color _initialTextColor;
        private Color _initialContinueBackgroundColor;        
        private Color _initialContinueTextColor;

        #endregion
    }
}