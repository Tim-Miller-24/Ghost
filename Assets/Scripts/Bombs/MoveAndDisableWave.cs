using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost
{
    public class MoveAndDisableWave : MonoBehaviour
    {
        private readonly float _waveSpeed = 12f;

        private WaitForSeconds _timeToDisableWave;

        private void Awake()
        {
            _timeToDisableWave = new WaitForSeconds(3f);
        }

        private void Update()
        {
            if (gameObject.activeInHierarchy) MoveWave();
        }

        private void MoveWave()
        {
            transform.position = new Vector2(transform.position.x - _waveSpeed * Time.deltaTime, transform.position.y);
            StartCoroutine(DisableWave());
        }

        private IEnumerator DisableWave()
        {
            yield return _timeToDisableWave;
            gameObject.SetActive(false);
            transform.position = Vector2.zero;
        }
    }
}