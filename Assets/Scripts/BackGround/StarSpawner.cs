using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _star;

    private WaitForSeconds _myTimer;

    private List<GameObject> _starsPool;

    private float _xPosition = 11f;
    private Vector2 _position;
    private float _speed = 8f;

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
        _position = new Vector2(_xPosition, Random.Range(-6f, 6f));
        myStar = Instantiate(_star, _position, Quaternion.identity, transform);
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
