using Project_Memento;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string m_path = "/ApplicationData.json";

    public static void LoadData()
    {
        string saveFilePath = Application.persistentDataPath + m_path;
        string data = File.ReadAllText(saveFilePath);
        QuestionGlobalDataSerialize questionGlobalDataSerialize = JsonUtility.FromJson<QuestionGlobalDataSerialize>(data);
        if (questionGlobalDataSerialize == null)
            return;
        QuestionManager.DeSerializeQuestionGlobalData(questionGlobalDataSerialize);
    }

    public static void SaveData()
    {
        QuestionGlobalDataSerialize questionGlobalDataSerialize = QuestionManager.SerializeQuestionGlobalData();
        string data = JsonUtility.ToJson(questionGlobalDataSerialize);
        string saveFilePath = Application.persistentDataPath + m_path;
        Debug.Log(saveFilePath);
        File.WriteAllText(saveFilePath, data);  
    }
}
