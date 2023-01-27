using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class PlayerScore : InitializableBase
    {
        private PlayerController _player;

        private Text _myScore;

        private int _score = 0;

        private WaitForSeconds _timerBetweenScoreAdding;

        private IEnumerator WaitBeforeAddingScore()
        {
            while (_player.IsAlive)
            {
                yield return _timerBetweenScoreAdding;
                AddAndUpdatePlayerScore();
            }
        }

        private void AddAndUpdatePlayerScore()
        {
            _score++;
            _myScore.text = $"{_score}";
        }

        public override void Initialize()
        {
            _timerBetweenScoreAdding = new WaitForSeconds(0.8f);

            _player = FindObjectOfType<PlayerController>();
            _myScore = gameObject.GetComponent<Text>();

            StartCoroutine(WaitBeforeAddingScore());
        }
    }
}