using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Suspect/Variable", fileName = "New Suspect Variable")]
    public class SuspectVariable : ScriptableObject
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private ScriptableSuspect _suspect;

        [SerializeField, Tooltip("Shared between all suspect variables")]
        private ScriptableSuspect _nullSuspect;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onSuspectChanged;

        #endregion


        #region Properties

        public ScriptableSuspect Suspect
        {
            get => _suspect;

            set
            {
                _suspect = value ? value : nullSuspect;
                _onSuspectChanged.Raise();
            }
        }

        #endregion


        #region Unity API

        private void OnValidate() 
        {
            SetNullClue(_nullSuspect);

            if(nullSuspect != null)
            {
                _nullSuspect = nullSuspect;
            }

            Suspect = _suspect;
        }

        #endregion


        #region Utils

        private void SetNullClue(ScriptableSuspect next)
        {
            if(!next || (nullSuspect && nullSuspect.Equals(next))) return;

            nullSuspect = next;
        }

        #endregion


        #region Private

        private static ScriptableSuspect nullSuspect;

        #endregion
    }
}