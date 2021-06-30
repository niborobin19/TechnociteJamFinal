using UnityEngine;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class TextSizeFitter : MonoBehaviour
    {
        #region Exposed
        [Header("Variables")]
        [SerializeField]
        private int _lineCount = 1;

        [SerializeField]
        private float _fontSizeCorrector = 1.0f;


        [Header("References")]
        [SerializeField]
        private Text _target;

        #endregion


        #region Unity API

        private void OnValidate() 
        {
            ComputeFontSize();
        }

        private void Update() 
        {
            ComputeFontSize();
        }

        #endregion


        #region Main

        private void ComputeFontSize()
        {
            var height = _target.rectTransform.rect.height;

            _target.fontSize = (int)(height / _lineCount * _fontSizeCorrector);
        }

        #endregion


        #region Private

        #endregion
    }
}