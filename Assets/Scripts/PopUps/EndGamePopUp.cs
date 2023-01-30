using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class EndGamePopUp : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _quitButton;

        private void Start()
        {
            _restartButton = transform.GetChild(0).GetComponent<Button>();
            _quitButton = transform.GetChild(1).GetComponent<Button>();

            _restartButton.onClick.AddListener(() => { RestartGame(); });

            _quitButton.onClick.AddListener(() => { QuitGame(); });
        }

        public void RestartGame()
        {

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
