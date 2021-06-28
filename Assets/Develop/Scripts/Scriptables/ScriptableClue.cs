using UnityEngine;

namespace Scriptable.Runtime
{
    [CreateAssetMenu(menuName ="Datas/Clue/Instance", fileName ="new clue")]
    public class ScriptableClue : ScriptableObject
    {
        #region Exposed

        [SerializeField]
        private string _name;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Sprite _sprite;

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
                _isDiscovered = true;
            }
        }

        #endregion


        #region Unity API

        private void Awake() 
        {
            IsDiscovered = false;
        }

        #endregion


        #region Private

        private bool _isDiscovered;

        #endregion
    }
}