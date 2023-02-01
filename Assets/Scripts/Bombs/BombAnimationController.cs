using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class BombAnimationController : InitializableBase
    {
        private Animator _animator;
        private WaitForSeconds _timeToResetBombState;

        private bool _isBounced;
        private bool _isChangedColor;

        public override void Initialize()
        {
            _timeToResetBombState = new WaitForSeconds(3f);

            _animator = gameObject.GetComponent<Animator>();
        }

        public void SetBombAnimation(int animationNumber)
        {
            if (animationNumber == 0)
            {
                _isBounced = true;
                _animator.SetBool("isBounce", _isBounced);
            }
            else if (animationNumber == 1)
            {
                _isChangedColor = true;
                _animator.SetBool("isChangingColor", _isChangedColor);
            }

            StartCoroutine(ResetBombState());
        }

        private IEnumerator ResetBombState()
        {
            yield return _timeToResetBombState;
            _isBounced = false;
            _isChangedColor = false;

            _animator.SetBool("isBounce", _isBounced);
            _animator.SetBool("isChangingColor", _isChangedColor);
        }
    }
}
