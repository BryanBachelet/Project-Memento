using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Memento
{

    public class EndScreenInterface : MonoBehaviour
    {
        public struct EndScreenData
        {
            public int goodAnswer;
            public int totalQuestion;
        }

        [Header("Interface Reference")]
        [SerializeField] private TMPro.TMP_Text m_goodAnswerText;

        [Header("Variables")]
        [SerializeField] private string m_goodAnswerLabel = "";




        public void SetupEndScren(EndScreenData data)
        {
            m_goodAnswerText.text = $"{m_goodAnswerLabel} {data.goodAnswer} / {data.totalQuestion}";
        }
    }

}