using UnityEngine;
using Game.Runtime;

namespace UI.Runtime
{
    public class MainMenuControler : MonoBehaviour
    {
        #region Main

        public void OnPlayButton()
        {
            GameManager.CurrentState = GameManager.GameState.Play;
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }

        #endregion
    }
}