using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{
    public class EvaluationData
    {
        public int questionQuantity;
        public QuestionData[] questionArray;

    }

    public class SpecificEvaluationData
    {
        public List<QuestionData> questionList;
        public int goodAnswerSession;
        public int totalQuestion;
    }


    public class EvaluationManager
    {

        public static EvaluationData LoadTodayQuestion()
        {
            return QuestionManager.LoadQuestionAtData(DateTime.Today, DataManager.instance.GetQuestionGlobalData());
        }

        public static SpecificEvaluationData GenerateSpecificEvaluationData(EvaluationData evaluationData)
        {
            SpecificEvaluationData specificEvaluationData = new SpecificEvaluationData();
            specificEvaluationData.questionList = new List<QuestionData>(evaluationData.questionArray);
            specificEvaluationData.totalQuestion = specificEvaluationData.questionList.Count;
            return specificEvaluationData;
        }

        public static QuestionData GetRandomQuestion(SpecificEvaluationData specificEvaluationData)
        {

            int indexQuesiton = UnityEngine.Random.Range(0, specificEvaluationData.questionList.Count);

            QuestionData questionData = specificEvaluationData.questionList[indexQuesiton];

            specificEvaluationData.questionList.RemoveAt(indexQuesiton);

            return questionData;

        }

      

        public static void UpdateQuestion(QuestionData questionData, bool isSuccessful, SpecificEvaluationData specificEvaluationData)
        {
            if (!isSuccessful)
            {
                
                QuestionManager.ResetQuestion(questionData.id);
            }
            else
            {
                specificEvaluationData.goodAnswerSession++;
                QuestionManager.UpdateQuestionDate(questionData.id);
            }
        }

        public static bool IsEvaluationFinish(SpecificEvaluationData specificEvaluationData)
        {
            return specificEvaluationData.questionList.Count == 0;
        }

        public static void FinishEvaluation(EndScreenInterface.EndScreenData endScreenData)
        {
            EvaluationInterface.CloseEvaluationInterface(endScreenData);
        }
    }
}
