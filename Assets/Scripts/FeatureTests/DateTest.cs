using System;
using System.Globalization;
using UnityEngine;

public class DateTest : MonoBehaviour
{
    public int[] dayDifference = { 0, 1, 2, 4, 7, 14, 28 };
    // Start is called before the first frame update
    void Start()
    {
        DateTime today = DateTime.Now;
        DateTime test = new DateTime(2025, 2, 14);
        Debug.Log("A: " + today.ToString());
        Debug.Log("B: " + test.ToString());
        for (int i = 0; i < dayDifference.Length; i++)
        {
            DateTime evaluationDay = today.AddDays(dayDifference[i]);
            if (evaluationDay.Date == test.Date)
            {
                Debug.Log("Good day");
            }
            Debug.Log("evaluationDay " + i+ " : " + evaluationDay.ToString());
        }
        
    }

}


