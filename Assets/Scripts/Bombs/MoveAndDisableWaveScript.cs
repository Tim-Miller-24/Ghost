using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class MoveAndDisableWaveScript : InitializableBase
    {
        private readonly float _waveSpeed = 10f;

        private WaitForSeconds _timeToDisableWave;

        public override void Initialize()
        {
            
        }

        private void Start()
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