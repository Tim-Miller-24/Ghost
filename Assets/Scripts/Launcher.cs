using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost.GameBody
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField]
        private InitializableBase[] _initializables;

        private void Start()
        {
            Launch();
        }

        public void Launch()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
