using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost
{
    public class WavesSpawner : InitializableBase
    {
        [SerializeField] private GameObject _wavePrefab;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private PlayerController _player;

        private readonly int _amountToPool = 15;

        private List<GameObject> _wavesInPool;

        private enum PositionsVariants
        {
            top, middle, bottom
        }

        private Dictionary<PositionsVariants, Vector2> _bombPosition;

        private readonly float _spawnPositionX = 15f;
        private WaitForSeconds _timeBetweenReActivate;

        private void Awake()
        {
            _wavesInPool = new List<GameObject>();

            _bombPosition = new Dictionary<PositionsVariants, Vector2>()
            {
                {PositionsVariants.top, new Vector2(_spawnPositionX, 3f) },
                {PositionsVariants.middle, new Vector2(_spawnPositionX, 0) },
                {PositionsVariants.bottom, new Vector2(_spawnPositionX, -3f) },
            };

            _timeBetweenReActivate = new WaitForSeconds(0.6f);
        }

        public override void Initialize()
        {
            for (int i = 0; i < _amountToPool; i++)
            {
                GameObject oneWave = Instantiate(_wavePrefab, Vector2.zero, Quaternion.identity, transform);
                SetPositionAndSpawnWave(oneWave);
                oneWave.SetActive(false);
                _wavesInPool.Add(oneWave);
            }

            StartCoroutine(SetIntervalToActivateWave());
        }

        public void ClearWavesPool()
        {
            foreach (var wave in _wavesInPool)
            {
                Destroy(wave);
            }

            _wavesInPool.Clear();
        }

        private void SetPositionAndSpawnWave(GameObject wave)
        {
            int rnd = Random.Range(0, 3);

            switch (rnd)
            {
                case 0:
                    SpawnBomb(_bombPosition[PositionsVariants.top], wave);
                    SpawnBomb(_bombPosition[PositionsVariants.middle], wave);
                    break;
                case 1:
                    SpawnBomb(_bombPosition[PositionsVariants.top], wave);
                    SpawnBomb(_bombPosition[PositionsVariants.bottom], wave);
                    break;
                case 2:
                    SpawnBomb(_bombPosition[PositionsVariants.middle], wave);
                    SpawnBomb(_bombPosition[PositionsVariants.bottom], wave);
                    break;
            }
        }

        private void SpawnBomb(Vector2 position, GameObject wave)
        {
            Instantiate(_bombPrefab, position, Quaternion.identity, wave.transform);
        }

        private IEnumerator SetIntervalToActivateWave()
        {
            while (_player.IsAlive)
            {
                yield return _timeBetweenReActivate;
                ActivateWave();
            }
        }

        private void ActivateWave()
        {
            List<GameObject> inactiveWaves = FindInactiveWave();

            int randomWave = Random.Range(0, inactiveWaves.Count);

            inactiveWaves[randomWave].SetActive(true);
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
    }
}