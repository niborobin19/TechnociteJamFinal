using UnityEngine;

namespace MyCursor.Runtime
{
    public class CursorManager : MonoBehaviour
    {
        #region Properties

        public static bool IsVisible
        {
            get => _isVisible;

            private set
            {
                _isVisible = value;
            }
        }

        #endregion


        #region Main

        public static void ToggleCursor()
        {
            IsVisible = !IsVisible;

            UpdateCursor();
        }

        public static void ShowCursor()
        {
            IsVisible = true;

            UpdateCursor();
        }

        public static void HideCursor()
        {
            IsVisible = false;

            UpdateCursor();
        }

        public static void UpdateCursor()
        {   
            Cursor.visible = _isVisible;
            Cursor.lockState = _isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        #endregion


        #region Private

        private static bool _isVisible;

        #endregion
    }
}