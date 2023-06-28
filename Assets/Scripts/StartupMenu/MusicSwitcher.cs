using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost
{
    public class MusicSwitcher : MonoBehaviour
    {
        [SerializeField] private AudioSource _backMusic;

        private Button _musicSwitcherButton;

        private void Start()
        {
            _backMusic.enabled = true;

            _musicSwitcherButton = GetComponent<Button>();

            _musicSwitcherButton.onClick.AddListener(() =>
            {
                _backMusic.mute = !_backMusic.mute;
            });
        }
    }
}