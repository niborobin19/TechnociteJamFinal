using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/ObjectVariable", fileName ="NewObjectVariable")]
    public class ObjectVariable : ScriptableObject
    {
        #region Exposed
        [Header("References")]
        [SerializeField]
        private Object _value;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onChanged;

        #endregion


        #region Properties

        public Object Value
        {
            get => _value;

            set
            {
                if(value && _value && value.Equals(_value)) return;

                _value = value;
                _onChanged.Raise();
            }
        }

        #endregion
    }
}