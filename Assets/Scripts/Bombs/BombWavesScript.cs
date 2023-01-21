using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWavesScript : MonoBehaviour
{
    private float _speed = 10f;

    private float _startPositionX;

    private void Start()
    {
        _startPositionX = transform.position.x;
    }

    public void MoveWave()
    {
        transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        StartCoroutine(DisableBomb());
    }

    private IEnumerator DisableBomb()
    {
        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);
    }
}
