

using System;
using UnityEngine;
using UnityEngine.UI;



namespace Project_Memento
{
    public struct QuestionGlobalData
    {
        public int questionQuantity;
        public QuestionData[] questionData;
        public bool questionDataActive;
        public bool questionContentActive;

    }


    public struct QuestionData
    {
        public int id;
        public string questionText;
        public string answerText;
        public Image answerImage;
        public DateTime dateInitialization;
        public DateTime nextDateTest;
        public bool isLearningFinish;

    }

    public class QuestionManager
    {

        public const int questionQuantityMax = 100;
        public static QuestionGlobalData CreateQuestionGlobalData()
        {
            QuestionGlobalData questionGlobalData = new QuestionGlobalData();
            questionGlobalData.questionData = new QuestionData[questionQuantityMax];
            questionGlobalData.questionQuantity = 0;
            return questionGlobalData;
        }

        public static void CreateQuestion(string questionText, string answerText)
        {
            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();
            QuestionData questionData = InitializeQuestion(questionText, answerText, questionGlobalData.questionContentActive);

            questionData.id = questionGlobalData.questionQuantity;
            questionGlobalData.questionQuantity++;
            questionGlobalData.questionData[questionData.id] = questionData;

            if (questionGlobalData.questionDataActive) 
                Debug.Log("New question has been add. ID = " + questionData.id);

        }

        private static QuestionData InitializeQuestion(string questionText, string answerText, bool debugActive)
        {
            QuestionData questionData = new QuestionData();
            questionData.questionText = questionText;
            questionData.answerText = answerText;
            questionData.dateInitialization = DateTime.Now;
            questionData.nextDateTest = DateTime.Now;
            questionData.nextDateTest.AddDays(1);

            if (debugActive)
                Debug.Log("Question Content: \n Question ID : " + questionData.id + "\n Question Content : " + questionText + " \n Question Answer : " + questionData.answerText);

            return questionData;

        }

        public static void AddQuestionImage(QuestionData questionData, Sprite sprite)
        {
            questionData.answerImage.sprite = sprite;
        }
    }
}
