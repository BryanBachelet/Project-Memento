using Project_Memento;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//Info persistant data path : C:\Users\<user>\AppData\LocalLow\<company name>


public class SaveManager : MonoBehaviour
{
    private const string m_path = "/ApplicationData.json";
    private const string m_tagPath= "/TagData.json";

    public static void LoadData()
    {
        string saveFilePath = Application.persistentDataPath + m_path;
        if (File.Exists(saveFilePath))
        {
            string data = File.ReadAllText(saveFilePath);
            QuestionGlobalDataSerialize questionGlobalDataSerialize = JsonUtility.FromJson<QuestionGlobalDataSerialize>(data);
            if (questionGlobalDataSerialize == null)

            {
                
            }else
            {
                QuestionManager.DeSerializeQuestionGlobalData(questionGlobalDataSerialize);
            }
        }

        saveFilePath = Application.persistentDataPath + m_tagPath;
        if (File.Exists(saveFilePath))
        {
            string data = File.ReadAllText(saveFilePath);
     
              Project_Memento.TagsData tagsData  = JsonUtility.FromJson<Project_Memento.TagsData>(data);

            if(tagsData != null)
            {
                DataManager.instance.tagsData = tagsData;
            }

           
        }
    }

    public static void SaveData()
    {
        QuestionGlobalDataSerialize questionGlobalDataSerialize = QuestionManager.SerializeQuestionGlobalData();
        string questionData = JsonUtility.ToJson(questionGlobalDataSerialize,true);

        string saveFilePath = Application.persistentDataPath + m_path;
        File.WriteAllText(saveFilePath, questionData);
        Debug.Log(saveFilePath);

        string tagsData = JsonUtility.ToJson(DataManager.instance.tagsData,true);
        saveFilePath = Application.persistentDataPath + m_tagPath;
        File.WriteAllText(saveFilePath, tagsData);

       
    }
}
