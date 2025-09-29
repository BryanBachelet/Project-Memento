using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Project_Memento
{
    public class EvaluationInterface : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_questionText;
        [SerializeField] private TMP_Text m_answerText;
        [SerializeField] private TMP_InputField m_answerInputField;
        [SerializeField] private Image m_answerButton;
        [SerializeField] private Image m_validateButton;
        [SerializeField] private Image m_cancelButton;
        [SerializeField] private GameObject m_evaluationObj;
        [SerializeField] private GameObject m_endScreenObj;

        [Header("Question Information Reference")]
        public GameObject m_questionInfoTitle;
        public GameObject m_questionInfoScrollView;
        public TMP_Text m_questionInfoText;

        [Header("Step Question Variables")]
        [SerializeField] private TMP_Text m_stepText;
        [SerializeField] private string m_stepLegendText;



        private static EvaluationInterface instance;


        public void OnEnable()
        {
            instance = this;
            DataManager.instance.LaunchEvaluation();
            if (DataManager.instance.evaluationData.questionQuantity != 0)
            {
                DisplayQuestion(DataManager.instance.GetCurrentEvaluationQuestion());

                m_validateButton.gameObject.SetActive(false);
                m_cancelButton.gameObject.SetActive(false);
                m_answerText.enabled = false;
            }


        }

        public static void DisplayQuestion(QuestionData questionData)
        {
            instance._DisplayQuestion(questionData);
        }

        public void _DisplayQuestion(QuestionData questionData)
        {
            m_questionText.text = questionData.questionText;
            m_answerInputField.text = "";

            m_answerButton.gameObject.SetActive(true);
            m_stepText.text = $"{m_stepLegendText} : {questionData.questionStep +1 } / 8";

            m_answerText.enabled = false;
            m_validateButton.gameObject.SetActive(false);
            m_cancelButton.gameObject.SetActive(false);

            m_questionInfoTitle.SetActive(false);
            m_questionInfoScrollView.SetActive(false);
        }

        public void DisplayAnswer(QuestionData questionData)
        {
            m_answerButton.gameObject.SetActive(false);
            m_validateButton.gameObject.SetActive(true);
            m_cancelButton.gameObject.SetActive(true);

            m_answerText.enabled = true;
            m_answerText.text = questionData.answerText;

            m_questionInfoTitle.SetActive(true);
            m_questionInfoScrollView.SetActive(true);
            m_questionInfoText.text = questionData.question_Information;
        }

        public void ValidateAnswer()
        {
            DisplayAnswer(DataManager.instance.GetCurrentEvaluationQuestion());
        }

        public void IsAnswerCorrect(bool isCorrect)
        {
            EvaluationManager.UpdateQuestion(DataManager.instance.GetCurrentEvaluationQuestion(), isCorrect, DataManager.instance.specificEvaluationData);
            DataManager.instance.GetNewQuestion();
        }


        public static void CloseEvaluationInterface(EndScreenInterface.EndScreenData endScreenData)
        {
            instance.GoodEndScreen(endScreenData);
        }


        public void GoodEndScreen(EndScreenInterface.EndScreenData endScreenData)
        {
            m_evaluationObj.SetActive(false);
            m_endScreenObj.SetActive(true);

            EndScreenInterface endScreenInterface = m_endScreenObj.GetComponent<EndScreenInterface>();  
            if (endScreenInterface)
            {

                endScreenInterface.SetupEndScren(endScreenData);
            }
        }


    }

}
