using UnityEngine;
using Events.Runtime;

namespace Interactable.Runtime
{
    public class Warper : MonoBehaviour, IInteractable
    {
        #region Exposed

        [Header("Variables")]
        [SerializeField]
        private float _warpDelay;

        [Header("References")]
        [SerializeField]
        private Transform _destination;

        [Header("Events")]
        [SerializeField]
        private ScriptableEvent _onWarping;

        #endregion


        #region Unity API

        public void Interacted(Object source)
        {
            _target = ((MonoBehaviour)source).gameObject.transform;
            _onWarping.Raise();

            Invoke(nameof(Warp), _warpDelay); 
        }

        private void Warp()
        {
            _target.position = _destination.position;
            _target.rotation = _destination.rotation;
        }

        #endregion


        #region Private

        private Transform _target;

        #endregion
    }
}