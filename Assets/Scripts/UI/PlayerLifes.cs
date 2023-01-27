﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class PlayerLifes : InitializableBase
    {
        [SerializeField] 
        private Text _myLifes;

        public int PlayerLifesCount { get; private set; }

        private void OnValidate()
        {
            _myLifes = gameObject.GetComponent<Text>();
        }

        public void DecreaseHealthAndUpdateUI(int damage)
        {
            PlayerLifesCount -= damage;
            _myLifes.text = $"{PlayerLifesCount}";
        }

        public override void Initialize()
        {
            PlayerLifesCount = 3;
            _myLifes.text = $"{PlayerLifesCount}";
        }
    }
}