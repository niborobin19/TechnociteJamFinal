using UnityEngine;
using UnityEngine.UI;
using Scriptables.Runtime;

namespace UI.Runtime
{
    public class TextBoxControler : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private StringVariable _displayedText;

        [Header("Variables")]
        [SerializeField]
        private float _displayTime = 3.0f;

        [SerializeField]
        private float _fadeInTime = 0.5f;

        [SerializeField]
        private float _fadeOutTime = 0.5f;

        [Header("References")]
        [SerializeField]
        private Image _background;

        [SerializeField]
        private Text _text;

        #endregion


        #region Unity API

        private void Awake() 
        {
            _initialBackgroundColor = _background.color;
            _initialTextColor = _text.color;
            _alpha = 0.0f;
            UpdateColors();
        }

        private void Update() 
        {
            UpdateAlphaThenColor();
        }

        #endregion


        #region Main

        private void UpdateAlphaThenColor()
        {
            if(_fadingIn)
            {
                var amount = 1 * Time.deltaTime * _fadeInTime;
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
                var amount = 1 * Time.deltaTime * _fadeOutTime;
                _alpha -= amount;
                _alpha = Mathf.Clamp(_alpha, 0.0f, 1.0f);

                if(Mathf.Approximately(_alpha, 0.0f))
                {
                    _fadingOut = false;
                }

                UpdateColors();

                return;
            }
        }

        private void UpdateColors()
        {
            _background.color = new Color(_initialBackgroundColor.r, _initialBackgroundColor.g, _initialBackgroundColor.b, _alpha);
            _text.color = new Color(_initialTextColor.r, _initialTextColor.g, _initialTextColor.b, _alpha);
        }

        public void DisplayedText_OnChanged()
        {
            _fadingOut = false;
            _fadingIn = true;
            _text.text = _displayedText.Value;

            Invoke(nameof(DelayedFadeOut), _fadeInTime + _displayTime);
        }

        private void DelayedFadeOut()
        {
            _fadingOut = true;
        }

        #endregion


        #region Private

        private bool _fadingIn;
        private bool _fadingOut;
        private float _alpha;
        private Color _initialBackgroundColor;
        private Color _initialTextColor;

        #endregion
    }
}