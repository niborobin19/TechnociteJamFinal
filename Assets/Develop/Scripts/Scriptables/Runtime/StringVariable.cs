using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/StringVariable", fileName ="NewStringVariable")]
    public class StringVariable : ScriptableObject
    {
       #region Exposed
        [Header("References")]
        [SerializeField]
        private string _value;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onChanged;

        #endregion


        #region Properties

        public string Value
        {
            get => _value;

            set
            {
                _value = value;
                _onChanged.Raise();
            }
        }

        #endregion
    }
}