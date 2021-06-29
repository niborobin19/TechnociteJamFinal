using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class Inspectable : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Variables")]
        [SerializeField, TextArea]
        private string _responseText;

        [Header("Datas")]
        [SerializeField]
        private StringVariable _displayedText;

        #endregion


        #region Unity API

        #endregion


        #region Main
        public void Interacted(Object source)
        {
            _displayedText.Value = _responseText;
        }

        #endregion


        #region Private

        #endregion
    }
}