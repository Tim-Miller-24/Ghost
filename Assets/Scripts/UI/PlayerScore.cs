using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private PlayerController _player;

    private Text _myScore;

    private int _score = 0;
    private bool _isScoreAdded = false;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _myScore = gameObject.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (_player.isAlive == false) return;

        if (_isScoreAdded) return;

        UpdatePlayerScore();
    }

    IEnumerator WaitBeforeAddingScore()
    {
        yield return new WaitForSeconds(0.8f);

        _isScoreAdded = false;
    }

    private void UpdatePlayerScore()
    {
        StartCoroutine(WaitBeforeAddingScore());

        _score++;
        _myScore.text = $"{_score}";

        _isScoreAdded = true;
    }
}