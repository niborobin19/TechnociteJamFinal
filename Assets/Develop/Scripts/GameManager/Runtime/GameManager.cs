using UnityEngine;
using MyCursor.Runtime;
using UnityEngine.SceneManagement;
using Scriptables.Runtime;

namespace Game.Runtime
{
    public class GameManager
    {
        #region Exposed
        public static float test;
        public enum GameState
        {
            Pause,
            Play
        }

        public static GameState CurrentState
        {
            get => _currentState;

            set
            {
                _currentState = value;
                UpdateCursor();
            } 
        }



        #endregion


        #region Utils

        private static void UpdateCursor()
        {
            switch (_currentState)
            {
                case GameState.Play:
                {
                    CursorManager.HideCursor();
                }break;

                case GameState.Pause:
                {
                    CursorManager.ShowCursor();
                }break;
            }
        }

        public static void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.CurrentState = GameState.Pause;
            ClueAndSuspectRegistry.ResetAll();
        }

        #endregion


        #region Private

        private static GameState _currentState = GameState.Pause;

        #endregion
    }
}