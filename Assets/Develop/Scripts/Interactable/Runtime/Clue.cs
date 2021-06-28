using UnityEngine;
using Scriptable.Runtime;

namespace Interactable.Runtime
{
    public class Clue : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Datas")]
        [SerializeField]
        private ScriptableClue _clue;

        #endregion


        #region Unity API

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            
        }

        #endregion


        #region Private

        #endregion
    }
}