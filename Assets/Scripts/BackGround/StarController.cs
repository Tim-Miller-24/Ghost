using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _starPrefab;

        private WaitForSeconds _timeToSpawn;
        private WaitForSeconds _timeToDestroy;

        private Queue<GameObject> _starsQueue;

        private readonly float _spawnPositionX = 11f;
        private readonly float _speedOfStars = 8f;

        private Vector2 _positionForSpawn;

        private void Awake()
        {
            _starsQueue = new Queue<GameObject>();

            _timeToSpawn = new WaitForSeconds(0.07f);
            _timeToDestroy = new WaitForSeconds(3f);
        }

        private void Start()
        {
            StartCoroutine(SetTimeToSpawn());
        }

        private void Update()
        {
            MoveStars();
        }

        private void MoveStars()
        {
            foreach (var star in _starsQueue)
            {
                star.transform.position = new Vector2(star.transform.position.x - _speedOfStars * Time.deltaTime, star.transform.position.y);
            }
        }

        private IEnumerator SetTimeToSpawn()
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
            _positionForSpawn = new Vector2(_spawnPositionX, Random.Range(-6f, 6f));
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
