using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class EndGamePopUp : MonoBehaviour
    {
        private Button _restartButton;
        private Button _quitButton;

        private Launcher _launcher;

        private void Start()
        {
            _restartButton = transform.GetChild(0).GetComponent<Button>();
            _quitButton = transform.GetChild(1).GetComponent<Button>();

            _launcher = FindObjectOfType<Launcher>();

            _restartButton.onClick.AddListener(() => { RestartGame(); });

            _quitButton.onClick.AddListener(() => { QuitGame(); });
        }

        public void RestartGame()
        {
            _launcher.Launch();
            gameObject.SetActive(false);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
