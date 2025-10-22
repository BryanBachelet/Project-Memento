using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project_Memento
{


    public class QuestionEditorDisplay : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private TMP_InputField m_questionInputField;
        [SerializeField] private TMP_InputField m_answerInputField;
        [SerializeField] private TMP_InputField m_questionInformationInputField;
        [SerializeField] private TMP_Text m_errorText;
        [SerializeField] private TMP_Dropdown m_tagDropdown;
        [SerializeField] private TMP_Text m_tagListText;
        [SerializeField] private TMP_InputField m_tagInputField;

        [Header("Color Feedback")]
        [SerializeField] private Color m_colorError = Color.white;
        [SerializeField] private Color m_colorGoodFeedback = Color.white;

        private Coroutine m_feedbackCoroutine;
        private const int m_duractionFeedback = 2;

        private List<TagsData> questionTags = new List<TagsData>();

        #region Unity Function
        public void OnEnable()
        {
            QuestionData questionData = DataManager.GetQuestionData(DataManager.instance.questionBoardData.indexQuestionSelect);
            m_questionInputField.text = questionData.questionText;
            m_answerInputField.text = questionData.answerText;
            m_questionInformationInputField.text = questionData.question_Information;

            UpdateTagDisplay(questionData);


            m_tagDropdown.ClearOptions();
            m_tagDropdown.AddOptions(DataManager.GetTagsList());
        }
        #endregion


        private void UpdateTagDisplay(QuestionData questionData)
        {
            m_tagListText.text = "";


            for (int i = 0; i < questionData.tagQuantity; i++)
            {
                m_tagListText.text += questionData.tagQuestion[i] + '\n';
            }
        }

        public void ValideChangeQuestion()
        {
            EditQuestionData editQuestionData = new EditQuestionData();
            editQuestionData.idQuestion = DataManager.instance.questionBoardData.indexQuestionSelect;
            editQuestionData.answerText = m_answerInputField.text;
            editQuestionData.questionText = m_questionInputField.text;
            editQuestionData.questionInformation = m_questionInformationInputField.text;

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
            m_questionInformationInputField.text = questionData.question_Information;
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

        public void AddTag()
        {
            Tag tagToAdd = DataManager.GetTagData(m_tagDropdown.value);

            EditQuestionData editQuestionData = new EditQuestionData();
            editQuestionData.idQuestion = DataManager.instance.questionBoardData.indexQuestionSelect;
            editQuestionData.tagQuestion = tagToAdd.name;

            EditQuestionResult editQuestionResult = new EditQuestionResult();

            QuestionManager.AddTagQuestion(editQuestionData, out editQuestionResult);

            if (editQuestionResult.isSuccess)
            {
                ShowValidFeedback(editQuestionResult.textFeedback);
            }
            else
            {
                ShowError(editQuestionResult.textFeedback);
            }


            QuestionData questionData = DataManager.GetQuestionData(DataManager.instance.questionBoardData.indexQuestionSelect);
            UpdateTagDisplay(questionData);
        }

        public void RemoveTag()
        {
            Tag tagToAdd = DataManager.GetTagData(m_tagDropdown.value);

            int idQuestion = DataManager.instance.questionBoardData.indexQuestionSelect;

            EditQuestionResult editQuestionResult = new EditQuestionResult();

            QuestionManager.RemoveTagQuestion(idQuestion, tagToAdd.name, out editQuestionResult);

            if (editQuestionResult.isSuccess)
            {
                ShowValidFeedback(editQuestionResult.textFeedback);
            }
            else
            {
                ShowError(editQuestionResult.textFeedback);
            }

            QuestionData questionData = DataManager.GetQuestionData(DataManager.instance.questionBoardData.indexQuestionSelect);
            UpdateTagDisplay(questionData);
        }


        public void CreateTag()
        {
            DataManager.CreateTag(m_tagInputField.text, Color.white);

            m_tagDropdown.ClearOptions();
            m_tagDropdown.AddOptions(DataManager.GetTagsList());
        }
    }

}
