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
        [HideInInspector] public TagsData tagsData;

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
            tagsData = new TagsData();

           

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

            EndScreenInterface.EndScreenData endScreenData = new EndScreenInterface.EndScreenData();
            endScreenData.totalQuestion = specificEvaluationData.totalQuestion;
            endScreenData.goodAnswer = specificEvaluationData.goodAnswerSession;

            EvaluationManager.FinishEvaluation(endScreenData);
            SaveManager.SaveData();
        }


        public QuestionGlobalData GetQuestionGlobalData() { return questionGlobalData; }
        public QuestionData GetCurrentEvaluationQuestion() { return m_currentQuestionData; }

        public static QuestionGlobalData GetQuestionGlobaldata() { return instance.questionGlobalData; }
        public static QuestionData GetQuestionData(int idQuestion) { return instance.questionGlobalData.questionData[idQuestion]; }

        public static int GetQuestionCount() { return instance.questionGlobalData.questionQuantity; }


        #region Tags Functions
        public static List<string> GetTagsList()
        {
            List<string> tags = new List<string>();
            for (int i = 0; i < instance.tagsData.tagQuantity; i++)
            {
                tags.Add(instance.tagsData.tags[i].name);
            }

            return tags;
        }

        public static Tag GetTagData(int idTag)
        {
            return instance.tagsData.tags[idTag];
        }

        public static Tag GetTagData(string tagName)
        {
            for (int i = 0; i < instance.tagsData.tagQuantity; i++)
            {
               if(tagName == instance.tagsData.tags[i].name)
                    return instance.tagsData.tags[i];
            }

            Debug.LogError("No tags");
            return new Tag() ;
        }


        public static bool HasTag(string tagName)
        {
            for (int i = 0; i < instance.tagsData.tagQuantity; i++)
            {
                if (tagName == instance.tagsData.tags[i].name)
                    return true;
            }

            return false;
        }

  
        
        public static void CreateTag(string tag, Color color)
        {
            if (HasTag(tag)) return;

            Tags.CreateTag(tag, color);
        }

        public static void RemoveTag(string tag)
        {
            if (!HasTag(tag)) return;

            Tags.RemoveTag(tag);
        }

        #endregion
    }
}