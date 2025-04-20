using Project_Memento;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Project_Memento
{

    public class QuestionInterface : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_questionInputField;
        [SerializeField] private TMP_InputField m_answerInputField;
        [SerializeField] private TMP_Text m_errorText;

        public void CreateNewQuestion()
        {
            string errorString = "";
            QuestionManager.CreateQuestion(m_questionInputField.text, m_answerInputField.text, out errorString);

            ShowError(errorString);
        }

        public void ShowError(string errorText)
        {
            m_errorText.text = errorText;
        }
    }
}
