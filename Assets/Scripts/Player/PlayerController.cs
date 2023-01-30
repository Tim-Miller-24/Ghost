﻿using System.Collections;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class PlayerController : InitializableBase
    {
        [SerializeField] 
        private PlayerLifes _playerLifes;
        [SerializeField]
        private EndGamePopUp _endGamePopUp;

        private Camera _camera;
        private Animator _animator;

        private readonly float _shakeRangeWithMove = 0.05f;
        private readonly float _shakeRangeWithDamage = 0.3f;

        public bool IsAlive { get; private set; }
        public bool IsImmortal { get; private set; }

        private WaitForSeconds _immortalTime;

        private enum PositionsVariants
        {
            top, middle, bottom
        }

        private PositionsVariants PlayerPosition = PositionsVariants.middle;

        public override void Initialize()
        {
            IsAlive = true;
            IsImmortal = false;

            _immortalTime = new WaitForSeconds(2f);

            _animator = gameObject.GetComponentInParent<Animator>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            if (IsAlive) MovePLayer();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag is "Bomb")
            {
                if (IsImmortal) return;
                TakeDamage();
            }
        }

        private IEnumerator ShakeCamera(float shakeRange, float myDurantion = 0.5f)
        {
            float xPos;
            float yPos;

            float timeLeft = Time.time;
            float duration = myDurantion;

            Vector3 originalPosition = _camera.transform.position;

            while ((timeLeft + duration) > Time.time)
            {
                xPos = Random.Range(-shakeRange, shakeRange);
                yPos = Random.Range(-shakeRange, shakeRange);

                _camera.transform.position = new Vector3(xPos, yPos, _camera.transform.position.z);
                yield return null;
            }

            _camera.transform.position = originalPosition;
        }

        private void TakeDamage()
        {
            if (!IsAlive) return;

            _playerLifes.DecreaseHealthAndUpdateUI(1);

            IsImmortal = true;
            _animator.SetBool("isImmortal", true);

            StartCoroutine(ShakeCamera(_shakeRangeWithDamage));

            StartCoroutine(SetImmortalTime());

            if (_playerLifes.PlayerLifesCount == 0)
            {
                IsAlive = false;
                _animator.SetBool("isDead", true);
                _endGamePopUp.gameObject.SetActive(true);
            }
        }

        private IEnumerator SetImmortalTime()
        {
            yield return _immortalTime;

            IsImmortal = false;

            _animator.SetBool("isImmortal", false);
        }

        private void MovePLayer()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (PlayerPosition == PositionsVariants.middle)
                {
                    transform.position = new Vector2(transform.position.x, 3f);
                    PlayerPosition = PositionsVariants.top;
                }
                else if (PlayerPosition == PositionsVariants.bottom)
                {
                    transform.position = new Vector2(transform.position.x, 0);
                    PlayerPosition = PositionsVariants.middle;
                }
                StartCoroutine(ShakeCamera(_shakeRangeWithMove, 0.15f));
                return;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (PlayerPosition == PositionsVariants.middle)
                {
                    transform.position = new Vector2(transform.position.x, -3f);
                    PlayerPosition = PositionsVariants.bottom;

                }
                else if (PlayerPosition == PositionsVariants.top)
                {
                    transform.position = new Vector2(transform.position.x, 0);
                    PlayerPosition = PositionsVariants.middle;
                }
                StartCoroutine(ShakeCamera(_shakeRangeWithMove, 0.15f));
                return;
            }
        }
    }
}