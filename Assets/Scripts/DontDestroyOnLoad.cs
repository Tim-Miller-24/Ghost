using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MillerSoft.Ghost
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        } 
    }
}

