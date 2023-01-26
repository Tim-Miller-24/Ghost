using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] private GameObject _starPrefab;

        private WaitForSeconds _timeToSpawn;
        private WaitForSeconds _timeToDestroy;

        private Queue<GameObject> _starsQueue;

        private readonly float _xPosition = 11f;
        private readonly float _speed = 8f;
        private Vector2 _positionForSpawn;

        public void ActivateStarSpawner()
        {
            _starsQueue = new Queue<GameObject>();

            _timeToSpawn = new WaitForSeconds(0.07f);
            _timeToDestroy = new WaitForSeconds(3f);

            StartCoroutine(SetTimeBetweenSpawn());
        }

        private IEnumerator SetTimeBetweenSpawn()
        {
            while (true)
            {
                yield return _timeToSpawn;
                SpawnStar();
                StartCoroutine(DestroyStar());
            }
        }

        private void Update()
        {
            MoveStars();
        }

        private void SpawnStar()
        {
            GameObject myStar;

            _positionForSpawn = new Vector2(_xPosition, Random.Range(-6f, 6f));
            myStar = Instantiate(_starPrefab, _positionForSpawn, Quaternion.identity, transform);

            _starsQueue.Enqueue(myStar);
        }

        private void MoveStars()
        {
            foreach (var star in _starsQueue)
            {
                star.transform.position = new Vector2(star.transform.position.x - _speed * Time.deltaTime, star.transform.position.y);
            }
        }

        private IEnumerator DestroyStar()
        {
            yield return _timeToDestroy;

            Destroy(_starsQueue.Dequeue());
        }
    }
}
