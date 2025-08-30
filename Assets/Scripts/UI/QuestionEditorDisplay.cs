using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace Project_Memento
{


    public class QuestionEditorDisplay : MonoBehaviour
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



        #region Unity Function
        public void OnEnable()
        {
            QuestionData questionData = DataManager.GetQuestionData(DataManager.instance.questionBoardData.indexQuestionSelect);
            m_questionInputField.text = questionData.questionText;
            m_answerInputField.text = questionData.answerText;
        }
        #endregion

        public void ValideChangeQuestion()
        {
            EditQuestionData editQuestionData = new EditQuestionData();
            editQuestionData.idQuestion = DataManager.instance.questionBoardData.indexQuestionSelect;
            editQuestionData.answerText = m_answerInputField.text;
            editQuestionData.questionText = m_questionInputField.text;

            EditQuestionResult editQuestionResult = new EditQuestionResult();
            QuestionManager.EditQuestion(editQuestionData, out editQuestionResult);

            if(editQuestionResult.isSuccess)
            {
                ShowValidFeedback(editQuestionResult.textFeedback);
            }
            else
            {
                ShowError(editQuestionResult.textFeedback);
            }

        }

        public void CancelQuestion()
        {
            QuestionData questionData = DataManager.GetQuestionData(DataManager.instance.questionBoardData.indexQuestionSelect);
            m_questionInputField.text = questionData.questionText;
            m_answerInputField.text = questionData.answerText;
        }

        public void ShowError(string errorText)
        {
            if (m_feedbackCoroutine != null)
                StopCoroutine(m_feedbackCoroutine);

            m_feedbackCoroutine = StartCoroutine(ShowFeedback(errorText, true));
        }

        public void ShowValidFeedback(string validFeedbackText)
        {
            if (m_feedbackCoroutine != null)
                StopCoroutine(m_feedbackCoroutine);

            m_feedbackCoroutine = StartCoroutine(ShowFeedback(validFeedbackText, false));
        }


        public IEnumerator ShowFeedback(string text, bool isError)
        {
            m_errorText.text = text;
            m_errorText.color = isError ? m_colorError : m_colorGoodFeedback;
            yield return new WaitForSeconds(m_duractionFeedback);
            m_errorText.text = "";
        }
    }

}
