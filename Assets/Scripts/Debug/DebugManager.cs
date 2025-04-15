using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{
    public class DebugManager : MonoBehaviour
    {
        public static DebugManager instance;
      

        public void Awake()
        {
            instance = this;
        }
    }
}
