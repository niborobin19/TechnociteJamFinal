using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;
using MyCursor.Runtime;

namespace UI.Runtime
{
    public class WarpOverlay : MonoBehaviour
    {
        #region Exposed
        [Header("Variable")]
        [SerializeField]
        private float _fadeInTime;

        [SerializeField]
        private float _fadeOutTime;

        [SerializeField]
        private float _displayTime;

        [Header("References")]
        [SerializeField]
        private Image _image;

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
            _initialColor = _image.color;
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
            _image.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, _alpha);
        }

        public void OnWarping()
        {
            gameObject.SetActive(true);
            GameManager.CurrentState = GameManager.GameState.Pause;
            CursorManager.HideCursor();
            _fadingOut = false;
            _fadingIn = true;

            Invoke(nameof(FadeOut), _displayTime + _fadeInTime);
        }

        public void FadeOut()
        {
            _fadingOut = true;
        }

        #endregion


        #region Private

        private bool _fadingIn;
        private bool _fadingOut;

        private float _alpha;
        private Color _initialColor;

        #endregion
    }
}