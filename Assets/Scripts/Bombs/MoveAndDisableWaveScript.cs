using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class MoveAndDisableWaveScript : InitializableBase
    {
        private readonly float _speed = 10f;

        private WaitForSeconds _timeToDisableWave;

        public override void Initialize()
        {
            _timeToDisableWave = new WaitForSeconds(3f);

            Debug.Log(_timeToDisableWave);
        }

        private void Update()
        {
            if (gameObject.activeInHierarchy) MoveWave();
        }

        private void MoveWave()
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
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