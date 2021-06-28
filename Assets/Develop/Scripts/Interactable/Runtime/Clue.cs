using UnityEngine;
using Scriptables.Runtime;

namespace Interactable.Runtime
{
    public class Clue : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private ScriptableClue _clue;

        [SerializeField]
        private ClueVariable _displayedClue;

        #endregion


        #region Unity API

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            _clue.IsDiscovered = true;
            _displayedClue.Clue = _clue;
        }

        #endregion


        #region Private

        private Vector3 _startingPosition;
        private Quaternion _startingRotation;

        #endregion
    }
}