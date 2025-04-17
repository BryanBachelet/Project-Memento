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

        public void CreateNewQuestion()
        {
            QuestionManager.CreateQuestion(m_questionInputField.text, m_answerInputField.text);
        }
    }
}
