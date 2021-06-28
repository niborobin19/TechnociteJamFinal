using UnityEngine;

namespace Interactable.Runtime
{
    public class Clue : MonoBehaviour, IInteractable
    {
        #region Exposed

        #endregion


        #region Unity API

        #endregion


        #region Main

        public void Interacted(Object source)
        {
            Debug.Log("Je suis une preuve");
        }

        #endregion


        #region Private

        #endregion
    }
}