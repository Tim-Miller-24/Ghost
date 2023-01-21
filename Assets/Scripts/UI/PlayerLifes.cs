using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifes : MonoBehaviour
{
    private Text _myLifes;

    public int playerLifesCount;

    private void Start()
    {
        playerLifesCount = 3;
        _myLifes = gameObject.GetComponent<Text>();
        _myLifes.text = $"{playerLifesCount}";
    }

    public void UpdateLifesCount()
    {
        _myLifes.text = $"{playerLifesCount}";
    }
}
