using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class PlayerScore : MonoBehaviour
    {
        private PlayerController _player;

        private Text _myScore;

        private int _score = 0;

        private WaitForSeconds _timerBetweenScoreAdding;

        public void ActivateUIScore()
        {
            _timerBetweenScoreAdding = new WaitForSeconds(0.8f);

            _player = FindObjectOfType<PlayerController>();
            _myScore = gameObject.GetComponent<Text>();

            StartCoroutine(WaitBeforeAddingScore());
        }

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
    }
}