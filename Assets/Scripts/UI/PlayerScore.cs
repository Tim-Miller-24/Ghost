using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private PlayerController _player;

    private Text _myScore;

    private int _score = 0;

    private WaitForSeconds _timerBetweenScoreAdding;

    private void Start()
    {
        _timerBetweenScoreAdding = new WaitForSeconds(0.8f);

        _player = FindObjectOfType<PlayerController>();
        _myScore = gameObject.GetComponent<Text>();

        StartCoroutine(WaitBeforeAddingScore());
    }

    private IEnumerator WaitBeforeAddingScore()
    {
        while (_player.isAlive)
        {
            yield return _timerBetweenScoreAdding;
            UpdatePlayerScore();
        }
    }

    private void UpdatePlayerScore()
    {
        _score++;
        _myScore.text = $"{_score}";
    }
}