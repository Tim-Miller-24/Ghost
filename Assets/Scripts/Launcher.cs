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

        [Header("Stars")]
        [SerializeField] 
        private InitializableBase[] _initializables;

        private void Start()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
