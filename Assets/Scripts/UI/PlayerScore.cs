using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class PlayerScore : InitializableBase
    {
        private PlayerController _player;

        private Text _myScore;

        private int _score;

        private WaitForSeconds _timerBetweenScoreAdding;

        private void Awake()
        {
            _timerBetweenScoreAdding = new WaitForSeconds(0.8f);
        }

        public override void Initialize()
        {
            _score = 0;

            _player = FindObjectOfType<PlayerController>();
            _myScore = gameObject.GetComponent<Text>();

            StartCoroutine(AddAndUpdatePlayerScore());
        }

        private IEnumerator AddAndUpdatePlayerScore()
        {
            while (_player.IsAlive)
            {
                yield return _timerBetweenScoreAdding;

                _score++;
                _myScore.text = $"{_score}";
            }
        }
    }
}