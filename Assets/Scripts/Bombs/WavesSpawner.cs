using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class WavesSpawner : InitializableBase
    {
        [SerializeField] 
        private GameObject _wavePrefab;
        [SerializeField] 
        private GameObject _bombPrefab;
        [SerializeField] 
        private PlayerController _player;

        [SerializeField] 
        private int _amountToPool;

        private List<GameObject> _wavesInPool;

        private enum PositionsVariants
        {
            top, middle, bottom
        }

        private Dictionary<PositionsVariants, Vector2> _bombPosition;

        private readonly float _xPosition = 15f;
        private WaitForSeconds _timeBetweenReActivate;


        public override void Initialize()
        {
            _wavesInPool = new List<GameObject>();

            _bombPosition = new Dictionary<PositionsVariants, Vector2>()
            {
                {PositionsVariants.top, new Vector2(_xPosition, 3f) },
                {PositionsVariants.middle, new Vector2(_xPosition, 0) },
                {PositionsVariants.bottom, new Vector2(_xPosition, -3f) },
            };

            GameObject oneWave;

            for (int i = 0; i < _amountToPool; i++)
            {
                oneWave = Instantiate(_wavePrefab, Vector2.zero, Quaternion.identity, transform);
                SetPositionAndSpawnWave(oneWave);
                oneWave.SetActive(false);
                _wavesInPool.Add(oneWave);
            }

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

        private IEnumerator SetIntervalBetweenActivate()
        {
            while (_player.IsAlive)
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
    }
}