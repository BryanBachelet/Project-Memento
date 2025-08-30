using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        [HideInInspector] public QuestionGlobalData questionGlobalData;
        [HideInInspector] public EvaluationData evaluationData;
        [HideInInspector] public SpecificEvaluationData specificEvaluationData;
        [HideInInspector] public QuestionBoardData questionBoardData;

        private QuestionData m_currentQuestionData;

        [Header("Question Debug Variables")]
        public bool questionDebugDataActive;
        public bool questionDebugContentActive;

        public void Awake()
        {
            instance = this;
            questionGlobalData = QuestionManager.CreateQuestionGlobalData();
            questionGlobalData.questionDataDebugActive = questionDebugDataActive;
            questionGlobalData.questionDebugContentActive = questionDebugContentActive;

            SaveManager.LoadData();
            QuestionManager.ShowQuestionDebug();

            evaluationData = EvaluationManager.LoadTodayQuestion();
            questionBoardData = new QuestionBoardData();
        }

       

        public void LaunchEvaluation()
        {
            evaluationData = EvaluationManager.LoadTodayQuestion();
            if (DataManager.instance.evaluationData.questionQuantity == 0)
            {
                Debug.Log("PM: No question for today ");
                return;
            }
            DataManager.instance.specificEvaluationData = EvaluationManager.GenerateSpecificEvaluationData(DataManager.instance.evaluationData);
            m_currentQuestionData = EvaluationManager.GetRandomQuestion(DataManager.instance.specificEvaluationData);
            Debug.Log("PM: Launch Evaluation ");

        }

        public void GetNewQuestion()
        {
            if (EvaluationManager.IsEvaluationFinish(specificEvaluationData))
            {
                FinishEvaluation();
                return;
            }
            m_currentQuestionData = EvaluationManager.GetRandomQuestion(DataManager.instance.specificEvaluationData);
            EvaluationInterface.DisplayQuestion(m_currentQuestionData);
        }

        public void FinishEvaluation()
        {
            Debug.Log("Finish Evaluation ");
            EvaluationManager.FinishEvaluation();
            SaveManager.SaveData();
        }


        public QuestionGlobalData GetQuestionGlobalData() { return questionGlobalData; }
        public QuestionData GetCurrentEvaluationQuestion() { return m_currentQuestionData; }

        public  static QuestionGlobalData GetQuestionGlobaldata() { return instance.questionGlobalData; }
        public static QuestionData GetQuestionData(int idQuestion) {  return instance.questionGlobalData.questionData[idQuestion]; }

        public static int GetQuestionCount() { return instance.questionGlobalData.questionQuantity; }
    }
}