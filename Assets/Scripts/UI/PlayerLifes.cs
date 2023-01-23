using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifes : MonoBehaviour
{
    [SerializeField] private Text _myLifes;

    public int playerLifesCount { get; private set; }

    private void Start()
    {
        playerLifesCount = 3;
        _myLifes.text = $"{playerLifesCount}";
    }

    private void OnValidate()
    {
        _myLifes = gameObject.GetComponent<Text>();
    }

    public void UpdateLifesCount()
    {
        _myLifes.text = $"{playerLifesCount}";
    }

    public void IncreaseHealth(int damage)
    {
        playerLifesCount -= damage;
    }
}
