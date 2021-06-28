using UnityEngine;

namespace Interactable.Runtime
{
    public interface IInteractable
    {
        #region Main

        public void Interacted(Object source);

        #endregion
    }
}