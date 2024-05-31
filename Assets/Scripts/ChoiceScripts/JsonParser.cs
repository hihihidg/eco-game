using System.Collections.Generic;
using UnityEngine;

public class JsonParser
{
    [System.Serializable]
    public class JsonData
    {
        public int Node;
        public string Context;
        public string Option1Text;
        public string Option1Description;
        public string Option1Impact;
        public int Option1NextNode;
        public string Option2Text;
        public string Option2Description;
        public string Option2Impact;
        public int Option2NextNode;
    }


    [System.Serializable]
    public class JsonDataList
    {
        public JsonData[] JsonData;
    }

    public static JsonDataList ParseJson(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        if (jsonFile == null)
        {
            Debug.LogError("Json file not found at Resources/" + fileName);
        }

        JsonDataList dataArray = JsonUtility.FromJson<JsonDataList>(jsonFile.text);
        Debug.Log(dataArray.JsonData[0].Context);

        return dataArray;
    }
}
