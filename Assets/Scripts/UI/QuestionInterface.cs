using Project_Memento;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project_Memento
{

    public class QuestionInterface : MonoBehaviour
    {
        public void CreateNewQuestion()
        {
            QuestionManager.CreateQuestion("Test A", "Text B");
        }
    }
}
