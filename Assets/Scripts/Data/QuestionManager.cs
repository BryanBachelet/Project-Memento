

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

    public enum QuestionError
    {
        AnswerEmpty = 0,
        QuestionEmpty =1,
    }

    public class QuestionManager
    {

        public const int questionQuantityMax = 100;

        public static QuestionGlobalData CreateQuestionGlobalData()
        {
            QuestionGlobalData questionGlobalData = new QuestionGlobalData();
            questionGlobalData.questionData = new QuestionData[0];
            questionGlobalData.questionQuantity = 0;
            return questionGlobalData;
        }

        public static void CreateQuestion(string questionText, string answerText, out string errorQuesiton)
        {
            if (questionText == null || questionText == string.Empty )
            {
                errorQuesiton = QuestionError(Project_Memento.QuestionError.QuestionEmpty);
                return;
            }
               
            if( answerText == null || answerText == string.Empty)
            {
                errorQuesiton = QuestionError(Project_Memento.QuestionError.AnswerEmpty);
                return;
            }

            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();
            QuestionData questionData = InitializeQuestion(questionText, answerText, questionGlobalData.questionDebugContentActive);

            questionData.id = questionGlobalData.questionQuantity;
            questionGlobalData.questionQuantity++;

            if (questionGlobalData.questionQuantity >= questionGlobalData.questionData.Length)
            {
                QuestionData[] tempArray = questionGlobalData.questionData;
                questionGlobalData.questionData = new QuestionData[questionGlobalData.questionData.Length + 5];
                Array.Copy(tempArray, questionGlobalData.questionData, tempArray.Length);
            }

            questionGlobalData.questionData[questionData.id] = questionData;

            if (questionGlobalData.questionDataDebugActive)
                Debug.Log("New question has been add. ID = " + questionData.id);

            SaveManager.SaveData();

            ShowQuestionDebug();
            errorQuesiton = string.Empty;

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

        private static string QuestionError(QuestionError questionCodeError)
        {
            switch (questionCodeError)
            {
                case Project_Memento.QuestionError.AnswerEmpty:
                    Debug.LogError("Answer is empty");
                    return "Please fill the answer with text";
                    break;
                case Project_Memento.QuestionError.QuestionEmpty:
                    Debug.LogError("Question is empty");
                    return "Please fill the question with text";
                    break;
                default:
                    return "No Exception";
                    break;
            }
        }

        public static void ShowQuestionDebug()
        {
            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();

            if (!questionGlobalData.questionDebugContentActive) 
                return;

            string debugString = "Question Gloabl Content:\n";
            debugString += "Question ID : " + questionGlobalData.questionQuantity + "\n";
            debugString += " - Question Detail :\n";
            for (int i = 0; i < questionGlobalData.questionQuantity; i++)
            {
                QuestionData questionData = questionGlobalData.questionData[i];
                debugString += "Question Content: \n Question ID : " + questionData.id + "\n Question Content : " + questionData.questionText + " \n Question Answer : " + questionData.answerText;
            }
            Debug.Log(debugString);
        }

        #region Save functions

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

        public static QuestionGlobalData DeSerializeQuestionGlobalData(QuestionGlobalDataSerialize questionGlobalDataSerialize)
        {
            QuestionGlobalData questionGlobalData =  DataManager.instance.GetQuestionGlobalData();
            questionGlobalData.questionQuantity = questionGlobalDataSerialize.questionQuantity;
            questionGlobalData.questionData = new QuestionData[questionGlobalData.questionQuantity];

            for(int i = 0; i < questionGlobalData.questionQuantity;i++)
            {
                questionGlobalData.questionData[i] = DeserializeQuestionDataSerialize(questionGlobalDataSerialize.questionData[i]);
            }

            return questionGlobalData;
        }

        private static QuestionData DeserializeQuestionDataSerialize(QuestionDataSerialize questionDataSerialize)
        {
            QuestionData questionData = new QuestionData();

            questionData.id = questionDataSerialize.id;
            questionData.questionText = questionDataSerialize.questionText;
            questionData.answerText = questionDataSerialize.answerText;
            questionData.answerImagePath = questionDataSerialize.answerImagePath;

            questionData.dateInitialization =  DateTime.Parse(questionDataSerialize.dateInitializationString);
            questionData.nextDateTest =  DateTime.Parse(questionDataSerialize.nextDateTest);

            questionData.isLearningFinish = questionDataSerialize.isLearningFinish;

            return questionData;
        }

        #endregion
    }
}
