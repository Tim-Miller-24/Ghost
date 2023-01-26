using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWavesScript : MonoBehaviour
{
    private float _speed = 10f;

    private WaitForSeconds _timeToDisableBomb;

    private void Update()
    {
        if (gameObject.activeInHierarchy) MoveWave();
    }

    private void MoveWave()
    {
        _timeToDisableBomb = new WaitForSeconds(3f);

        transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        StartCoroutine(DisableBomb());
    }

    private IEnumerator DisableBomb()
    {
        yield return _timeToDisableBomb;

        gameObject.SetActive(false);
        transform.position = Vector2.zero;
    }
}
