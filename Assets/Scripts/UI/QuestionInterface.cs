using Project_Memento;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Project_Memento
{

    public class QuestionInterface : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private TMP_InputField m_questionInputField;
        [SerializeField] private TMP_InputField m_answerInputField;
        [SerializeField] private TMP_Text m_errorText;

        [Header("Color Feedback")]
        [SerializeField] private Color m_colorError = Color.white;
        [SerializeField] private Color m_colorGoodFeedback = Color.white;

        private Coroutine m_feedbackCoroutine;
        private const int m_duractionFeedback = 2;


        public void CreateNewQuestion()
        {
            string feedbackString = "";
           bool isCreate = QuestionManager.CreateQuestion(m_questionInputField.text, m_answerInputField.text, out feedbackString);

            if (!isCreate)
                ShowError(feedbackString);
            else
            {
                m_questionInputField.text = "";
                m_answerInputField.text = "";
                ShowValidFeedback(feedbackString);
            }
        }

        public void ShowError(string errorText)
        {
          if(m_feedbackCoroutine != null) 
                StopCoroutine(m_feedbackCoroutine);

            m_feedbackCoroutine = StartCoroutine( ShowFeedback(errorText, true));
        }

        public void ShowValidFeedback(string validFeedbackText)
        {
            if (m_feedbackCoroutine != null)
                StopCoroutine(m_feedbackCoroutine);

            m_feedbackCoroutine = StartCoroutine(ShowFeedback(validFeedbackText, false));
        }


        public IEnumerator ShowFeedback(string text ,bool isError)
        {
            m_errorText.text = text;
            m_errorText.color = isError ? m_colorError : m_colorGoodFeedback;
            yield return new WaitForSeconds(m_duractionFeedback);
            m_errorText.text = "";
        }
    }
}
