using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] private GameObject _starPrefab;

        private WaitForSeconds _myTimer;

        private List<GameObject> _starsPool;

        private readonly float _xPosition = 11f;
        private readonly float _speed = 8f;
        private Vector2 _positionForSpawn;

        public void ActivateStarSpawner()
        {
            _starsPool = new List<GameObject>();

            _myTimer = new WaitForSeconds(0.07f);
            StartCoroutine(SetTimeBetweenSpawn());
        }

        private IEnumerator SetTimeBetweenSpawn()
        {
            while (true)
            {
                yield return _myTimer;
                SpawnStar();
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
            _starsPool.Add(myStar);
        }

        private void MoveStars()
        {
            foreach (var star in _starsPool)
            {
                star.transform.position = new Vector2(star.transform.position.x - _speed * Time.deltaTime, star.transform.position.y);
            }
        }
    }
}
