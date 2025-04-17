

using System;
using UnityEngine;
using UnityEngine.UI;



namespace Project_Memento
{
    public class QuestionGlobalData
    {
        public int questionQuantity;
        public QuestionData[] questionData;
        public bool questionDataDebugActive;
        public bool questionDebugContentActive;

    }

    [Serializable]
    public class QuestionData
    {
        public int id;
        public string questionText;
        public string answerText;
        public Image answerImage;
        public string answerImagePath;
        public DateTime dateInitialization;
        public DateTime nextDateTest;
        public bool isLearningFinish;

    }

    [Serializable]
    public class QuestionDataSerialize
    {
        public int id;
        public string questionText;
        public string answerText;
        public string answerImagePath;
        public string dateInitializationString;
        public string nextDateTest;
        public bool isLearningFinish;
    }
    public class QuestionGlobalDataSerialize
    {
        public int questionQuantity;
        public QuestionDataSerialize[] questionData;
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
            QuestionData questionData = InitializeQuestion(questionText, answerText, questionGlobalData.questionDebugContentActive);

            questionData.id = questionGlobalData.questionQuantity;
            questionGlobalData.questionQuantity++;
            questionGlobalData.questionData[questionData.id] = questionData;

            if (questionGlobalData.questionDataDebugActive)
                Debug.Log("New question has been add. ID = " + questionData.id);

            SaveManager.SaveData();

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

        #region Save functions

        // TODO : Tested result
        private static QuestionDataSerialize SerializeQuestionData(QuestionData questionData)
        {
            QuestionDataSerialize questionDataSerialize = new QuestionDataSerialize();

            questionDataSerialize.id = questionData.id;

            questionDataSerialize.questionText = questionData.questionText; 
            questionDataSerialize.answerText = questionData.answerText; 
            questionDataSerialize.answerImagePath = questionData.answerImagePath; 

            questionDataSerialize.dateInitializationString = questionData.dateInitialization.Date.ToShortDateString(); 
            questionDataSerialize.nextDateTest = questionData.nextDateTest.Date.ToShortDateString();

            questionDataSerialize.isLearningFinish = questionData.isLearningFinish; 

            return questionDataSerialize;
        }
         
        public static QuestionGlobalDataSerialize SerializeQuestionGlobalData()
        {
            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();
            QuestionGlobalDataSerialize questionGlobalDataSerialize = new QuestionGlobalDataSerialize();  
            
            questionGlobalDataSerialize.questionQuantity = questionGlobalData.questionQuantity; 

            questionGlobalDataSerialize.questionData = new QuestionDataSerialize[questionGlobalData.questionQuantity];
            for (int i = 0; i < questionGlobalData.questionQuantity; i++)
            {
                questionGlobalDataSerialize.questionData[i]  = SerializeQuestionData(questionGlobalData.questionData[i]);   
            }

                
            return questionGlobalDataSerialize;
        }

        #endregion
    }
}
