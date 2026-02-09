using SFB;
using UnityEngine;
using BorsalinoTools;
using System.Runtime.CompilerServices;


namespace Project_Memento
{
    public class QuestionBoardData
    {
        public int indexQuestion;
        public int indexQuestionSelect;
    }



    public class QuestionBoard : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private GameObject m_questionEditorInterfaceObject;
        [SerializeField] private GameObject[] m_questionLineObject;


        #region Unity Functions
        public void OnEnable()
        {
            QuestionBoardData questionBoardData = DataManager.instance.questionBoardData;
            questionBoardData.indexQuestion = 0;
            SetQuestionLines(questionBoardData.indexQuestion);
        }

        public void OnDisable()
        {

        }
        #endregion

        public void SetQuestionLines(int indexStart)
        {
            QuestionData[] questionDataArray = DataManager.instance.questionGlobalData.questionData;
            int indexQuesitonLine = 0;
            for (int i = indexStart; i < DataManager.instance.questionGlobalData.questionQuantity && indexQuesitonLine < m_questionLineObject.Length; i++)
            {
                m_questionLineObject[indexQuesitonLine].SetActive(true);
                QuestionLineUI questionLineUI = m_questionLineObject[indexQuesitonLine].GetComponent<QuestionLineUI>();
                questionLineUI.SetupQuestionBoard(this);
                questionLineUI.SetupQuestion(questionDataArray[i]);
                indexQuesitonLine++;
            }
            if (indexQuesitonLine < m_questionLineObject.Length)
            {
                for (int i = indexQuesitonLine; i < m_questionLineObject.Length; i++)
                {
                    m_questionLineObject[i].SetActive(false);
                }
            }
        }

        public void IncreaseQuestionBoardIndex()
        {
            QuestionBoardData questionBoardData = DataManager.instance.questionBoardData;

            if (questionBoardData.indexQuestion + m_questionLineObject.Length < DataManager.GetQuestionCount())
            {
                questionBoardData.indexQuestion += m_questionLineObject.Length;
                SetQuestionLines(questionBoardData.indexQuestion);
            }
        }

        public void DecreaseQuestionBoardIndex()
        {
            QuestionBoardData questionBoardData = DataManager.instance.questionBoardData;

            if (questionBoardData.indexQuestion - m_questionLineObject.Length >= 0)
            {
                questionBoardData.indexQuestion -= m_questionLineObject.Length;
                SetQuestionLines(questionBoardData.indexQuestion);
            }
            else
            {
                questionBoardData.indexQuestion = 0;
                SetQuestionLines(questionBoardData.indexQuestion);
            }
        }

        public void DeleteQuestion(int idInstance)
        {
            int indexQuestionButton = 0;
            for (int i = 0; i < m_questionLineObject.Length; i++)
            {
                if (m_questionLineObject[i].GetInstanceID() == idInstance)
                {
                    indexQuestionButton = i;
                    break;
                }
            }
            QuestionBoardData questionBoardData = DataManager.instance.questionBoardData;


            QuestionManager.DeleteQuestion(questionBoardData.indexQuestion + indexQuestionButton);

            if (questionBoardData.indexQuestion + 1 > DataManager.GetQuestionCount())
            {
                DecreaseQuestionBoardIndex();
            }
            else
            {
                SetQuestionLines(questionBoardData.indexQuestion);
            }



        }

        public void EditQuestion(int idGameObjectInstance)
        {
            int indexQuestionButton = 0;
            for (int i = 0; i < m_questionLineObject.Length; i++)
            {
                if (m_questionLineObject[i].GetInstanceID() == idGameObjectInstance)
                {
                    indexQuestionButton = i;
                    break;
                }
            }
            QuestionBoardData questionBoardData = DataManager.instance.questionBoardData;
            questionBoardData.indexQuestionSelect = indexQuestionButton;
            m_questionEditorInterfaceObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        public void GetFileCSV()
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("Get CSV", Application.dataPath, "csv", false);

            if (paths[0] == string.Empty) return;


            CsvMetaInfo csvMetaInfo = new CsvMetaInfo();
            csvMetaInfo.sepearator = ',';
            csvMetaInfo.hasColumnName = false;
            csvMetaInfo.hasRowNames = false;
            csvMetaInfo.hasId = false;
            csvMetaInfo.path = paths[0];

            CsvInfo csvInfo = CsvTools.ReadCSVFile(csvMetaInfo);
            string[] questionValues = CsvTools.GetColumnValue(0, csvInfo);
            string[] answerValues = CsvTools.GetColumnValue(1, csvInfo);
            
            int minSize = Mathf.Min(answerValues.Length, questionValues.Length);
            if (minSize == 0) return;

            for (int i = 0; i < minSize; i++)
            {

                string feedback = "";
                QuestionManager.CreateQuestion(questionValues[i], answerValues[i], "", out feedback);
            }


        }
        
        public void RemoveDuplicateQuestion()
        {
            QuestionManager.DeleteDuplicateQuestion();
        }

    }
}