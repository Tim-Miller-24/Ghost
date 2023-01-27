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
        private StarController _starSpawner;

        private void Start()
        {
            _playerController.InitialPlayer();
            _uiPlayerLifes.ActivateUILifes();
            _uiPlayerScore.ActivateUIScore();

            _wavesSpawner.SpawnWaves();
            _wavesSpawner.ActivateWaves();

            _starSpawner.ActivateStarSpawner();
        }
    }
}
