using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


namespace Project_Memento
{


    public class QuestionLineUI : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private TMP_Text m_questionText;
        [SerializeField] private TMP_Text m_questionIDText;
        [SerializeField] private TMP_Text m_questionStepText;
        [SerializeField] private TMP_Text m_questionDateText;

        private QuestionBoard m_questionBoard = null;

        public void SetupQuestionBoard(QuestionBoard questionBoard) { m_questionBoard = questionBoard; }

        public void SetupQuestion(QuestionData questionData)
        {
            m_questionIDText.text = questionData.id.ToString();
            m_questionText.text = questionData.questionText.ToString();
          if(!questionData.isLearningFinish)
                m_questionStepText.text = questionData.questionStep.ToString();
          else
                m_questionStepText.text = "Finish";
            m_questionDateText.text = questionData.nextDateTest.ToString("d");
        }

        public void DeleteQuestion()
        {
            m_questionBoard.DeleteQuestion(gameObject.GetInstanceID());
        }

        public void EditQuestion()
        {
            m_questionBoard.EditQuestion(gameObject.GetInstanceID());
        }


    }
}
