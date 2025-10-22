using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Project_Memento
{
    public class TagEditorUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown m_tagDropdown;
        [SerializeField] private TMP_InputField m_inputField;
        [SerializeField] private Button m_AddTagButton;

       [SerializeField][HideInInspector] private List<TMP_Dropdown.OptionData> drowdownOptions = new List<TMP_Dropdown.OptionData>();

        public void Start()
        {
            InitializeUI();
        }

        public void OnEnable()
        {
            InitializeUI();
        }

        public void InitializeUI()
        {
            drowdownOptions.Clear();
            string[] tagNameList = DataManager.GetTagsList().ToArray();
            for (int i = 0; i < tagNameList.Length; i++)
            {
                drowdownOptions.Add(new TMP_Dropdown.OptionData(tagNameList[i]));
            }
            m_tagDropdown.options = new List<TMP_Dropdown.OptionData>(drowdownOptions);
        }

        public void FilterDropdown()
        {

            m_tagDropdown.Hide();

            m_tagDropdown.ClearOptions();

            bool isContainTag = DataManager.GetTagsList().Contains(m_inputField.text);
            if (isContainTag)
            {
                m_AddTagButton.gameObject.SetActive(false);
            }
            else
            {
                m_AddTagButton.gameObject.SetActive(true);
            }

            List<TMP_Dropdown.OptionData> possibleOption = drowdownOptions.FindAll(option => option.text.IndexOf(m_inputField.text) >= 0);
            m_tagDropdown.options = possibleOption;
            if (possibleOption.Count != 0)
                m_tagDropdown.Show();

            m_inputField.caretPosition = m_inputField.text.Length;
            m_inputField.ActivateInputField();

        }

        public void AddTag()
        {
            DataManager.CreateTag(m_inputField.text, Color.white);
            drowdownOptions.Add(new TMP_Dropdown.OptionData(m_inputField.text));
            FilterDropdown();
        }

        public void RemoveTag()
        {
            DataManager.RemoveTag(drowdownOptions[m_tagDropdown.value].text);
            drowdownOptions.Remove(drowdownOptions[m_tagDropdown.value]);
            m_tagDropdown.value = 0;
            m_tagDropdown.options = new List<TMP_Dropdown.OptionData>(drowdownOptions);

        }
    }
}