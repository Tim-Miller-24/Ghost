using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MillerSoft.Ghost
{
    public class PlayButton : MonoBehaviour
    {
        private string _gamePlayScene;

        private Button _playButton;

        private void Start()
        {
            _gamePlayScene = "PlayScene";

            _playButton = GetComponent<Button>();

            _playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(_gamePlayScene);
            });
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
        }
    }
}