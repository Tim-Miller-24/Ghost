using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class Launcher : MonoBehaviour
    {
        [Header("Player and UI")]
        [SerializeField] 
        private PlayerController _playerController;
        [SerializeField] 
        private PlayerLifes _uiPlayerLifes;
        [SerializeField] 
        private PlayerScore _uiPlayerScore;

        [Header("Bombs")]
        [SerializeField] 
        private WavesSpawner _wavesSpawner;
        [SerializeField]
        private MoveAndDisableWaveScript _moveAndDisableWaveScript;
        [SerializeField]
        private BombAnimationController _bombAnimationController;

        [Header("Stars")]
        [SerializeField]
        private StarController _starController;

        [Header("Initializator")]
        [SerializeField] 
        private InitializableBase[] _initializables;

        private void Start()
        {
            Launch();
        }

        public void Launch()
        {
            _initializables = new InitializableBase[]
            {
                _playerController, _uiPlayerLifes, _uiPlayerScore, _wavesSpawner, _moveAndDisableWaveScript, _bombAnimationController, _starController
            };

            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
