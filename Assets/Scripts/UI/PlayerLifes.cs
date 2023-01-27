using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MillerSoft.Ghost.GameBody
{
    public class PlayerLifes : MonoBehaviour
    {
        [SerializeField] 
        private Text _myLifes;

        public int PlayerLifesCount { get; private set; }

        public void ActivateUILifes()
        {
            PlayerLifesCount = 3;
            _myLifes.text = $"{PlayerLifesCount}";
        }

        private void OnValidate()
        {
            _myLifes = gameObject.GetComponent<Text>();
        }

        public void UpdateLifesCount()
        {
            _myLifes.text = $"{PlayerLifesCount}";
        }

        public void IncreaseHealth(int damage)
        {
            PlayerLifesCount -= damage;
        }
    }
}