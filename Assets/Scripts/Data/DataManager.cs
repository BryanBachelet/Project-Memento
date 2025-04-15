using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{
    public class DataManager : MonoBehaviour
    {
        public  static DataManager instance;
        [HideInInspector] public QuestionGlobalData questionGlobalData;
        
        [Header("Question Debug Variables")]
        public bool questionDebugDataActive;
        public bool questionDebugContentActive;

        public void Awake()
        {
            instance = this;
            questionGlobalData = QuestionManager.CreateQuestionGlobalData();
            questionGlobalData.questionDataActive = questionDebugDataActive;
            questionGlobalData.questionContentActive = questionDebugContentActive;
        }

        public QuestionGlobalData GetQuestionGlobalData() { return questionGlobalData; }
    }
}