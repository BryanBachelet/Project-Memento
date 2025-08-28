using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private GameObject m_EvaluationObj;
        [SerializeField] private GameObject m_MainMenuObj;
        private static EvaluationInterface instance;

       
        public void OnEnable()
        {
            instance = this;
            DataManager.instance.LaunchEvaluation();
            DisplayQuestion(DataManager.instance.GetCurrentEvaluationQuestion());

            m_validateButton.gameObject.SetActive(false);
            m_cancelButton.gameObject.SetActive(false);
            m_answerText.enabled = false;
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


            m_answerText.enabled = false;
            m_validateButton.gameObject.SetActive(false);
            m_cancelButton.gameObject.SetActive(false);
        }

        public void DisplayAnswer(QuestionData questionData)
        {
            m_answerButton.gameObject.SetActive(false);
            m_validateButton.gameObject.SetActive(true);
            m_cancelButton.gameObject.SetActive(true);

            m_answerText.enabled = true;
            m_answerText.text = questionData.answerText;
        }

        public void ValidateAnswer()
        {
            DisplayAnswer(DataManager.instance.GetCurrentEvaluationQuestion());
        }

        public void IsAnswerCorrect(bool isCorrect)
        {
            EvaluationManager.UpdateQuestion(DataManager.instance.GetCurrentEvaluationQuestion(), isCorrect);
            DataManager.instance.GetNewQuestion();
        }


        public static void CloseEvaluationInterface()
        {
            instance.ReturnToMenu();
        }


        public void ReturnToMenu()
        {
            m_EvaluationObj.SetActive(false);
            m_MainMenuObj.SetActive(true);
        }


    }

}
