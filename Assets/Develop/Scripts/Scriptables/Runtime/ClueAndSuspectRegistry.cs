using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Registry", fileName ="New Registry")]
    public class ClueAndSuspectRegistry : ScriptableObject
    {
        #region Exposed
        [Header("Datas")]
        [SerializeField]
        private List<ScriptableSuspect> _suspects;

        [SerializeField]
        private List<ScriptableClue> _clues;

        #endregion


        #region Properties

        public List<ScriptableSuspect> Suspects
        {
            get => _suspects;
        }

        public List<ScriptableClue> Clues
        {
            get => _clues;
        }

        #endregion
    }
}