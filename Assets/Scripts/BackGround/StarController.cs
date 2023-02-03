using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class StarController : InitializableBase
    {
        [SerializeField] 
        private GameObject _starPrefab;

        private WaitForSeconds _timeToSpawn;
        private WaitForSeconds _timeToDestroy;

        private Queue<GameObject> _starsQueue;

        private readonly float _xPosition = 11f;
        private readonly float _speed = 8f;
        private Vector2 _positionForSpawn;

        private bool _isCanSpawn;

        public override void Initialize()
        {
            _isCanSpawn = false;
            _starsQueue = new Queue<GameObject>();

            Debug.Log(_starsQueue.Count);

            _timeToSpawn = new WaitForSeconds(0.07f);
            _timeToDestroy = new WaitForSeconds(3f);

            StartCoroutine(SetTimeAndSpawn());
            _isCanSpawn = true;
        }

        private void Update()
        {
            if (!_isCanSpawn) return;
            MoveStars();
        }

        private void MoveStars()
        {
            foreach (var star in _starsQueue)
            {
                star.transform.position = new Vector2(star.transform.position.x - _speed * Time.deltaTime, star.transform.position.y);
            }
        }

        private IEnumerator SetTimeAndSpawn()
        {
            while (true)
            {
                yield return _timeToSpawn;
                SpawnStar();
                StartCoroutine(DestroyStar());
            }
        }

        private void SpawnStar()
        {
            _positionForSpawn = new Vector2(_xPosition, Random.Range(-6f, 6f));
            GameObject myStar = Instantiate(_starPrefab, _positionForSpawn, Quaternion.identity, transform);

            _starsQueue.Enqueue(myStar);
        }

        private IEnumerator DestroyStar()
        {
            yield return _timeToDestroy;

            Destroy(_starsQueue.Dequeue());
        }
    }
}
