using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{
    public class GeneralUI : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();

#if UNITY_EDITOR
            Debug.Log("Quit application");
#endif
        }
    }
}