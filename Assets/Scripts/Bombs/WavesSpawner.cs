using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _wavePrefab;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private PlayerController _player;

    [SerializeField] private int _amountToPool;

    public List<GameObject> bobmWavesInPool;

    private enum _positionsVariants
    {
        top, middle, bottom
    }

    private readonly float _xPosition = 15f;
    private Vector2 _position;
    private bool _isActivated;

    private void Start()
    {
        bobmWavesInPool = new List<GameObject>();
        GameObject oneWave;
        for (int i = 0; i < _amountToPool; i++)
        {
            oneWave = Instantiate(_wavePrefab, new Vector2(0, 0), Quaternion.identity, transform);
            SetPositionAndSpawnWave(oneWave);
            oneWave.SetActive(false);
            bobmWavesInPool.Add(oneWave);
            _isActivated = false;
        }
    }
    private void Update()
    {
        if (_player.isAlive == false) Destroy(gameObject);

        if (_isActivated) return;

        ChangeWavesState();
    }

    IEnumerator SetIntervalBetweenActivate()
    {
        yield return new WaitForSeconds(0.8f);

        _isActivated = false;
    }

    private void ChangeWavesState()
    {
        int rnd = Random.Range(0, _amountToPool);

        GameObject temp = bobmWavesInPool[rnd];

        if (!temp.activeInHierarchy)
        {
            temp.SetActive(true);

            _isActivated = true;

            StartCoroutine(SetIntervalBetweenActivate());
        }
    }

    private void SpawnBomb(_positionsVariants position, GameObject wave)
    {
        if (position == _positionsVariants.top)
        {
            _position = new Vector2(_xPosition, 3f);
        }
        else if (position == _positionsVariants.middle)
        {
            _position = new Vector2(_xPosition, 0);
        }
        else if (position == _positionsVariants.bottom)
        {
            _position = new Vector2(_xPosition, -3f);
        }

        Instantiate(_bombPrefab, _position, Quaternion.identity, wave.transform);
    }

    private void SetPositionAndSpawnWave(GameObject wave)
    {
        int rnd = Random.Range(0, 3);

        switch (rnd)
        {
            case 0:
                SpawnBomb(_positionsVariants.bottom, wave);
                SpawnBomb(_positionsVariants.middle, wave);
                break;
            case 1:
                SpawnBomb(_positionsVariants.bottom, wave);
                SpawnBomb(_positionsVariants.top, wave);
                break;
            case 2:
                SpawnBomb(_positionsVariants.middle, wave);
                SpawnBomb(_positionsVariants.top, wave);
                break;
        }
    }
}
