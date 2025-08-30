

using System;
using System.Collections.Generic;
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
        public int questionStep;
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
        public int questionStep;
    }
    public class QuestionGlobalDataSerialize
    {
        public int questionQuantity;
        public QuestionDataSerialize[] questionData;
    }


    public enum QuestionError
    {
        AnswerEmpty = 0,
        QuestionEmpty = 1,
    }

    public struct EditQuestionData
    {
        public int idQuestion;
        public string questionText;
        public string answerText;

    }

    public struct EditQuestionResult
    {
        public bool isSuccess;
        public string textFeedback;
    }



    public class QuestionManager
    {


        public static readonly int[] dayStep = { 0, 1, 2, 4, 7, 14, 28 };
        public const int questionQuantityMax = 100;

        public static QuestionGlobalData CreateQuestionGlobalData()
        {
            QuestionGlobalData questionGlobalData = new QuestionGlobalData();
            questionGlobalData.questionData = new QuestionData[0];
            questionGlobalData.questionQuantity = 0;
            return questionGlobalData;
        }

        public static bool CreateQuestion(string questionText, string answerText, out string feedbackQuestionText)
        {
            if (questionText == null || questionText == string.Empty)
            {
                feedbackQuestionText = QuestionError(Project_Memento.QuestionError.QuestionEmpty);
                return false;
            }

            if (answerText == null || answerText == string.Empty)
            {
                feedbackQuestionText = QuestionError(Project_Memento.QuestionError.AnswerEmpty);
                return false;
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
            feedbackQuestionText = "Question sucessfully created";
            return true;

        }

        private static QuestionData InitializeQuestion(string questionText, string answerText, bool debugActive)
        {
            QuestionData questionData = new QuestionData();
            questionData.questionText = questionText;
            questionData.answerText = answerText;
            questionData.dateInitialization = DateTime.Now;
            questionData.questionStep = 0;
            questionData.nextDateTest = DateTime.Now;
            questionData.nextDateTest = questionData.nextDateTest.AddDays(dayStep[questionData.questionStep]);
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

        public static void DeleteQuestion(int idQuestion)
        {

            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();

            Debug.Log("Delete the question " + questionGlobalData.questionData[idQuestion].questionText + " with the id " + questionGlobalData.questionData[idQuestion].id.ToString());

            questionGlobalData.questionQuantity--;
            questionGlobalData.questionData[idQuestion] = null;
            for (int i = idQuestion; i < questionGlobalData.questionQuantity; i++)
            {
                questionGlobalData.questionData[i] = questionGlobalData.questionData[i + 1];
                questionGlobalData.questionData[i].id--;
                questionGlobalData.questionData[i + 1] = null;
            }

            SaveManager.SaveData();
        }

        public static void EditQuestion(EditQuestionData questionData, out EditQuestionResult resultData)
        {
            resultData = new EditQuestionResult();
            resultData.isSuccess = true;
            if (questionData.questionText == null || questionData.questionText == string.Empty)
            {
                resultData.textFeedback = QuestionError(Project_Memento.QuestionError.QuestionEmpty);
                resultData.isSuccess = false;
            }

            if (questionData.answerText == null || questionData.answerText == string.Empty)
            {

                resultData.textFeedback = QuestionError(Project_Memento.QuestionError.AnswerEmpty);
                resultData.isSuccess = false;
            }
            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();
            QuestionData questionInstanceData = questionGlobalData.questionData[questionData.idQuestion];
            questionInstanceData.answerText = questionData.answerText;
            questionInstanceData.questionText = questionData.questionText;

            resultData.textFeedback = "Question sucessfully edited";
            SaveManager.SaveData();
        }


        public static void UpdateQuestionDate(int idQuestion)
        {

            QuestionGlobalData questionGlobalData = DataManager.GetQuestionGlobaldata();
            QuestionData questionData = questionGlobalData.questionData[idQuestion];

            questionData.questionStep++;
            if (questionData.questionStep == dayStep.Length)
            {
                questionData.isLearningFinish = true;
            }
            else
            {
                questionData.nextDateTest = questionData.dateInitialization;
                questionData.nextDateTest = questionData.dateInitialization.AddDays(dayStep[questionData.questionStep]);
            }
           
        }
    

        public static void ResetQuestion(int idQuestion)
        {
            QuestionGlobalData questionGlobalData = DataManager.GetQuestionGlobaldata();
            QuestionData questionData = questionGlobalData.questionData[idQuestion];

            questionData.questionStep = 0;
            questionData.dateInitialization = DateTime.Now;
            questionData.nextDateTest = questionData.dateInitialization.AddDays(dayStep[questionData.questionStep]);
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

            questionDataSerialize.questionStep = questionData.questionStep;

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
                questionGlobalDataSerialize.questionData[i] = SerializeQuestionData(questionGlobalData.questionData[i]);
            }


            return questionGlobalDataSerialize;
        }

        public static QuestionGlobalData DeSerializeQuestionGlobalData(QuestionGlobalDataSerialize questionGlobalDataSerialize)
        {
            QuestionGlobalData questionGlobalData = DataManager.instance.GetQuestionGlobalData();
            questionGlobalData.questionQuantity = questionGlobalDataSerialize.questionQuantity;
            questionGlobalData.questionData = new QuestionData[questionGlobalData.questionQuantity];

            for (int i = 0; i < questionGlobalData.questionQuantity; i++)
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

            questionData.dateInitialization = DateTime.Parse(questionDataSerialize.dateInitializationString);
            questionData.nextDateTest = DateTime.Parse(questionDataSerialize.nextDateTest);

            questionData.questionStep = questionDataSerialize.questionStep;
            questionData.isLearningFinish = questionDataSerialize.isLearningFinish;

            return questionData;
        }

        #endregion

        public static EvaluationData LoadQuestionAtData(DateTime date, QuestionGlobalData questionGlobalData)
        {
            EvaluationData evaluationData = new EvaluationData();
            List<QuestionData> questionData = new List<QuestionData>();
            for (int i = 0; i < questionGlobalData.questionQuantity; i++)
            {

                if (questionGlobalData.questionData[i].isLearningFinish) continue;

                DateTime nextDate = questionGlobalData.questionData[i].nextDateTest;

                TimeSpan result = nextDate.Date - date.Date;
                TimeSpan refDayLimit = new TimeSpan(-3, 0, 0, 0);
                if (result.Days < refDayLimit.Days)
                {
                    ResetQuestion(i);
                }
                if (result.Days <= 0)
                {
                    questionData.Add(questionGlobalData.questionData[i]);
                }

            }

            evaluationData.questionArray = questionData.ToArray();
            evaluationData.questionQuantity = questionData.Count;
            return evaluationData;
        }
    }
}
