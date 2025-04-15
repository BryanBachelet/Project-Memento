using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project_Memento
{

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
        static void CreateQuestion(string questionText, string answerText)
        {
            QuestionData questionData = InitializeQuestion(questionText, answerText);
            questionData.id = DataManager.instance.questionQuantity;
            DataManager.instance.questionQuantity++;
            DataManager.instance.questionData[questionData.id] = questionData;
        }

        static QuestionData InitializeQuestion(string questionText, string answerText)
        {
            QuestionData questionData = new QuestionData();
            questionData.questionText = questionText;
            questionData.answerText = answerText;
            questionData.dateInitialization = DateTime.Now;
            questionData.nextDateTest = DateTime.Now;
            questionData.nextDateTest.AddDays(1);
            return questionData;

        }

        static void AddQuestionImage(QuestionData questionData, Sprite sprite)
        {
            questionData.answerImage.sprite = sprite;
        }
    }
}
