using UnityEngine;
using Scriptables.Runtime;
using UnityEngine.UI;

namespace UI.Runtime
{
    public class CrossairControler : MonoBehaviour
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private ObjectVariable _targetedObject;

        [Header("Variables")]
        [SerializeField]
        private Color _hasTargetColor;

        [SerializeField]
        private Color _noTargetColor;

        [Header("References")]
        [SerializeField]
        private Image _image;

        #endregion


        #region Unity API

        private void Awake() 
        {
            UpdateDisplay();    
        }

        #endregion


        #region Main

        public void UpdateDisplay()
        {
            _image.color = _targetedObject.Value ? _hasTargetColor : _noTargetColor;
        }

        #endregion


        #region Private

        #endregion
    }
}