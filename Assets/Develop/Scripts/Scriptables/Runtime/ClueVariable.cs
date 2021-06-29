using System.Diagnostics.Tracing;
using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Clue/Variable", fileName ="new clue variable")]
    public class ClueVariable : ScriptableObject
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private ScriptableClue _clue;

        [SerializeField, Tooltip("Shared between all clue variables")]
        private ScriptableClue _nullClue;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onClueChanged;

        #endregion


        #region Properties

        public ScriptableClue Clue
        {
            get => _clue;
            set
            {
                _clue = value ? value : nullClue;
                _onClueChanged.Raise();
            }
        }

        #endregion


        #region Events

        #endregion


        #region Unity API

        private void OnValidate() 
        {
            SetNullClue(_nullClue);

            if(nullClue != null)
            {
                _nullClue = nullClue;
            }

            Clue = _clue;
        }

        #endregion


        #region Utils

        private void SetNullClue(ScriptableClue next)
        {
            if(!next || (nullClue && nullClue.Equals(next))) return;

            nullClue = next;
        }

        #endregion


        #region Private

        private static ScriptableClue nullClue;

        #endregion
    }
}