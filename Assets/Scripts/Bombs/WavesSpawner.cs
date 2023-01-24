using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _wavePrefab;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private PlayerController _player;

    [SerializeField] private int _amountToPool;

    private List<GameObject> _wavesInPool;

    private enum PositionsVariants
    {
        top, middle, bottom
    }

    private Dictionary<PositionsVariants, Vector2> BombPosition;

    private readonly float _xPosition = 15f;
    private WaitForSeconds _timeBetweenReActivate;

    private void Awake()
    {
        _wavesInPool = new List<GameObject>();
        GameObject oneWave;

        BombPosition = new Dictionary<PositionsVariants, Vector2>()
        {
            {PositionsVariants.top, new Vector2(_xPosition, 3f) },
            {PositionsVariants.middle, new Vector2(_xPosition, 0) },
            {PositionsVariants.bottom, new Vector2(_xPosition, -3f) },
        };

        for (int i = 0; i < _amountToPool; i++)
        {
            oneWave = Instantiate(_wavePrefab, Vector2.zero, Quaternion.identity, transform);
            SetPositionAndSpawnWave(oneWave);
            oneWave.SetActive(false);
            _wavesInPool.Add(oneWave);
        }
    }

    private void Start()
    {
        _timeBetweenReActivate = new WaitForSeconds(0.8f);

        StartCoroutine(SetIntervalBetweenActivate());
    }

    private List<GameObject> FindInactiveWave()
    {
        List<GameObject> inActiveWaves = new List<GameObject>();

        foreach (var wave in _wavesInPool)
        {
            if (!wave.gameObject.activeInHierarchy)
            {
                inActiveWaves.Add(wave);
            }
        }

        return inActiveWaves;
    }

    private void ActivateWave()
    {
        List<GameObject> inactiveWaves = FindInactiveWave();

        int randomWave = Random.Range(0, inactiveWaves.Count);

        inactiveWaves[randomWave].SetActive(true);
    }

    IEnumerator SetIntervalBetweenActivate()
    {
        while (_player.isAlive)
        {
            yield return _timeBetweenReActivate;
            ActivateWave();
        }
    }
    private void SpawnBomb(Vector2 position, GameObject wave)
    {
        Instantiate(_bombPrefab, position, Quaternion.identity, wave.transform);
    }

    private void SetPositionAndSpawnWave(GameObject wave)
    {
        int rnd = Random.Range(0, 3);

        switch (rnd)
        {
            case 0:
                SpawnBomb(BombPosition[PositionsVariants.top], wave);
                SpawnBomb(BombPosition[PositionsVariants.middle], wave);
                break;
            case 1:
                SpawnBomb(BombPosition[PositionsVariants.top], wave);
                SpawnBomb(BombPosition[PositionsVariants.bottom], wave);
                break;
            case 2:
                SpawnBomb(BombPosition[PositionsVariants.middle], wave);
                SpawnBomb(BombPosition[PositionsVariants.bottom], wave);
                break;
        }
    }
}
