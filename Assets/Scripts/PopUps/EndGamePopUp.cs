using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MillerSoft.Ghost
{
    public class EndGamePopUp : MonoBehaviour
    {
        [SerializeField] private Launcher _launcher;
        [SerializeField] private WavesSpawner _wavesSpawner;

        private string _startMenuScene = "StartMenu";

        private Button _restartButton;
        private Button _contunieForAds;
        private Button _backToMenuButton;

        private void Start()
        {
            _restartButton = transform.GetChild(0).GetComponent<Button>();
            _contunieForAds = transform.GetChild(1).GetComponent<Button>();
            _backToMenuButton = transform.GetChild(2).GetComponent<Button>();

            _restartButton.onClick.AddListener(() => { RestartGame(); });

            _backToMenuButton.onClick.AddListener(() => { ComeBackToMenu(); });
        }

        public void RestartGame()
        {
            _wavesSpawner.ClearWavesPool();

            _launcher.Launch();
            gameObject.SetActive(false);
        }

        private void ComeBackToMenu()
        {
            SceneManager.LoadScene(_startMenuScene);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _contunieForAds.onClick.RemoveAllListeners();
            _backToMenuButton.onClick.RemoveAllListeners();
        }
    }
}
