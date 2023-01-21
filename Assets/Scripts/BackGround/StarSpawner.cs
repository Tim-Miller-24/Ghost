using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private StarScript _star;

    private float _xPosition = 11f;
    private Vector2 _position;
    private bool _isSpawned = false;

    private void Update()
    {
        if (_isSpawned) return;

        SpawnStar();
    }

    IEnumerator SetTimeBetweenSpawn()
    {
        yield return new WaitForSeconds(0.05f);

        _isSpawned = false;
    }

    private void SpawnStar()
    {
        StartCoroutine(SetTimeBetweenSpawn());

        _position = new Vector2(_xPosition, Random.Range(-6f, 6f));
        Instantiate(_star, _position, Quaternion.identity, transform);

        _isSpawned = true;
    }
}
