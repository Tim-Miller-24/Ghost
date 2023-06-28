using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost
{
    public class Launcher : MonoBehaviour
    {
        public static Launcher Instance;

        [SerializeField]
        protected InitializableBase[] initializables;

        private void Start()
        {
            Launch();

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            } 
            else
            {
                Destroy(gameObject);
            }
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
