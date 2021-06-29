using UnityEngine;
using Events.Runtime;

namespace Scriptables.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Clue/Instance", fileName ="new clue")]
    public class ScriptableClue : ScriptableObject
    {
        #region Exposed

        [Header("Variables")]
        [SerializeField]
        private string _name;

        [SerializeField, TextArea]
        private string _description;

        [SerializeField]
        private Sprite _sprite;

        
        [Header("Events")]

        [SerializeField]
        private ScriptableEvent _onClueChanged;

        #endregion


        #region Properties

        public string Name
        {
            get => _name;
            set 
            {
                _name = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }

        public Sprite Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
            }
        }

        public bool IsDiscovered
        {
            get => _isDiscovered;
            set
            {
                _isDiscovered = value;
                _onClueChanged.Raise();
            }
        }

        #endregion


        #region Unity API

        private void OnEnable() 
        {
            IsDiscovered = false;
        }

        #endregion


        #region Private

        private bool _isDiscovered;
        private bool _isDiscoveredRuntime;

        #endregion
    }
}