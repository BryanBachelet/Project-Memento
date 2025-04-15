using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{
    public class DataManager : MonoBehaviour
    {
        public  static DataManager instance;
        public int questionQuantity;
        public QuestionData[] questionData;
        private const int m_questionQuantityMax =100;

        public void Awake()
        {
            instance = this;
            questionData = new QuestionData[m_questionQuantityMax];
            questionQuantity = 0;
        }
    }
}