using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


namespace Project_Memento
{
    public class QuestionLineUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_questionText;
        [SerializeField] private TMP_Text m_questionIDText;
        [SerializeField] private TMP_Text m_questionStepText;
        [SerializeField] private TMP_Text m_questionDateText;

        public void SetupQuestion(QuestionData questionData)
        {
            m_questionIDText.text = questionData.id.ToString();
            m_questionText.text = questionData.questionText.ToString();
            m_questionStepText.text = questionData.questionStep.ToString();
            m_questionDateText.text = questionData.nextDateTest.ToString("d");
        }

        public void EditQuestion()
        {

        }

            
    }
}
