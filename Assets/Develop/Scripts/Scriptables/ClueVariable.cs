using UnityEngine;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Clue/Variable", fileName ="new clue variable")]
    public class ClueVariable : ScriptableObject
    {
        #region Exposed
        [SerializeField]
        private ScriptableClue _clue;

        [SerializeField, Tooltip("Shared between all clues")]
        private ScriptableClue _nullClue;

        #endregion


        #region Properties

        public ScriptableClue Clue
        {
            get => _clue;
            set
            {
                _clue = value ? value : nullClue;
                OnClueChanged?.Invoke(_clue);
            }
        }

        #endregion


        #region Events

        public delegate void OnClueChangedHandler(ScriptableClue next);
        public OnClueChangedHandler OnClueChanged;

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