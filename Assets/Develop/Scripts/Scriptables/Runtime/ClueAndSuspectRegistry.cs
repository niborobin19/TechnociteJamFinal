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

        private static List<ClueAndSuspectRegistry> Instances
        {
            get
            {
                if(_instances == null)
                {
                    _instances = new List<ClueAndSuspectRegistry>();
                }

                return _instances;
            }

            set
            {
                _instances = value;
            }
        }

        #endregion


        #region Unity API

        private void OnEnable() 
        {
            Register(this);
        }

        private void OnDisable() 
        {
            Unregister(this);
        }

        #endregion


        #region Main

        public void ResetRegistry()
        {
            foreach (var clue in Clues)
            {
                clue.IsDiscovered = false;
            }

            foreach (var suspect in Suspects)
            {
                suspect.UnlinkAll();
            }
        }

        public static void ResetAll()
        {
            foreach (var instance in Instances)
            {
                instance.ResetRegistry();
            }
        }

        private static void Register(ClueAndSuspectRegistry registry)
        {
            if(Instances.Contains(registry)) return;

            Instances.Add(registry);
        }

        private static void Unregister(ClueAndSuspectRegistry registry)
        {
            if(!Instances.Contains(registry)) return;

            Instances.Remove(registry);
        }

        #endregion


        #region Private

        private static List<ClueAndSuspectRegistry> _instances;

        #endregion
    }
}