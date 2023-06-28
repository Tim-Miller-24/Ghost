using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField]
        protected InitializableBase[] initializables;

        private void Start()
        {
            Launch();

            DontDestroyOnLoad(this);
        }

        public void Launch()
        {
            foreach (var initializable in initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
