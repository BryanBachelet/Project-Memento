using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BorsalinoTools
{


    public class CsvTestScript : MonoBehaviour
    {
        [SerializeField]
        public CsvInfo csvInfo;
  

        // Start is called before the first frame update
        void Start()
        {
            string path = Application.dataPath + "/Temp/TestCSV.csv";
            
            CsvMetaInfo csvMetaInfo = new CsvMetaInfo();
            csvMetaInfo.path = path;
            csvMetaInfo.sepearator = ',';
            csvMetaInfo.hasColumnName = false;

            csvInfo = CsvTools.ReadCSVFile(csvMetaInfo);

            Debug.Log("Column Names : ");
            string[] columnName = CsvTools.GetColumnName(csvInfo);
            for (int i = 0; columnName != null && i < columnName.Length ; i++)
            {
                Debug.Log(columnName[i]);
            }

            Debug.Log("----- Column Player Names value  --------- ");

            string[] columnValue  = CsvTools.GetColumnValue(1, csvInfo);
            for (int i = 0; i < columnValue.Length; i++)
            {
                Debug.Log(columnValue[i]);
            }

            Debug.Log("----- Column Hours value  --------- ");

             columnValue = CsvTools.GetColumnValue(2, csvInfo);
            for (int i = 0; i < columnValue.Length; i++)
            {
                Debug.Log(columnValue[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}