using System.Collections;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField]
    private StarScript _star;

    private WaitForSeconds _myTimer;

    private float _xPosition = 11f;
    private Vector2 _position;

    private void Start()
    {
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

    private void SpawnStar()
    {
        _position = new Vector2(_xPosition, Random.Range(-6f, 6f));
        Instantiate(_star, _position, Quaternion.identity, transform);
    }
}
