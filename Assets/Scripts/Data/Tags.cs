using System;
using UnityEngine;


namespace Project_Memento
{
    [Serializable]
    public struct Tag
    {
        public int id;
        public string name;
        public Color color;

        public Tag(string name, Color color)
        {
            this.name = name;
            this.color = color;
            this.id = -1;   
        }
    }
    [Serializable]
    public class TagsData
    {
        public int tagQuantity = 0;
        public Tag[] tags = new Tag[0];
    }



    public static class Tags 
    {
        public static void CreateTag(string name, Color color)
        {
           
            TagsData tagsData = DataManager.instance.tagsData;
            for (int i = 0; i < tagsData.tags.Length; i++)
            {
                if (tagsData.tags[i].name == name)
                {
                    return;
                }
            }

            Tag data = new Tag(name, color);

          

           
            data.id = tagsData.tagQuantity;
            tagsData.tagQuantity++;

            if (tagsData.tagQuantity >= tagsData.tags.Length)
            {
                Tag[] tagsTemp = tagsData.tags;
                tagsData.tags = new Tag[tagsTemp.Length + 5 ];
                Array.Copy(tagsTemp, tagsData.tags, tagsTemp.Length);
            }

            tagsData.tags[data.id] = data;

            SaveManager.SaveData();
        }

        public static void RemoveTag(string name)
        {
            TagsData tagsData = DataManager.instance.tagsData;
            for (int i = 0; i < tagsData.tags.Length; i++)
            {
                if (tagsData.tags[i].name == name)
                {
                    tagsData.tagQuantity--;
                    
                    Tag[] tagsTemp = tagsData.tags;
                    Array.Copy(tagsTemp, i + 1, tagsData.tags, i, tagsTemp.Length - (i + 1));

                    SaveManager.SaveData();
                    return;
                }
            }



        }
    }

   
}
