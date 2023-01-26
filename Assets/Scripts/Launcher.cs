using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("Player and UI")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerLifes _uiPlayerLifes;
    [SerializeField] private PlayerScore _uiPlayerScore;

    [Header("Bombs")]
    [SerializeField] private WavesSpawner _wavesSpawner;

    [Header("Stars")]
    [SerializeField] private StarSpawner _starSpawner;
   // [SerializeField] private StarScript _starScript;

    private void Start()
    {
        _playerController.InitialPlayer();
        _uiPlayerLifes.ActivateUILifes();
        _uiPlayerScore.ActivateUIScore();

        _wavesSpawner.SpawnWaves();
        _wavesSpawner.ActivateWaves();

        _starSpawner.ActivateStarSpawner();
      //  _starScript.StartMovingStars();
    }
}