using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost
{
    public class PlayerLifes : InitializableBase
    {
        [SerializeField] private Text _myLifes;

        public int PlayerLifesCount { get; private set; }

        public override void Initialize()
        {
            _myLifes = gameObject.GetComponent<Text>();
            PlayerLifesCount = 3;
            _myLifes.text = $"{PlayerLifesCount}";
        }

        public void DecreaseHealthAndUpdateUI(int damage)
        {
            PlayerLifesCount -= damage;
            _myLifes.text = $"{PlayerLifesCount}";
        }
    }
}