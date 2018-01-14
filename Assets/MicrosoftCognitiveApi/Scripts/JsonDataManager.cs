using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using JsonDataHandler;

public class JsonDataManager : MonoBehaviour 
{

    JsonData itemData;

    public static JsonDataManager instance;

    private void Start()
    {
        instance = this;


    }
    public JsonData JsonPerser(string jsonString)
    {
        itemData = JsonMapper.ToObject(jsonString);
        //JsonMapper.ToObject<Wrapper<RootObject>>(jsonString);
        //Person thomas = JsonMapper.ToObject<Person>(json);
        return itemData;
    }

   public void LoadJsonData(string jsonString)
    {
        // JsonData data = JsonMapper.ToObject(jsonString);

        RootObject obj = JsonMapper.ToObject<RootObject>(jsonString);
//        print(obj.regions[0].lines[0].words[0]);

        List<string> wordList = new List<string>();
        foreach (var reg in obj.regions)
        {
            foreach (var line in reg.lines)
            {
                foreach (var word in line.words)
                {
                    wordList.Add(word.text);
                }
            }
        }

        string texttoread = string.Join(" ", wordList.ToArray());
        TTSibm.instance.PlayVoice(texttoread);
        print(texttoread);
    }

}
namespace JsonDataHandler
{
    public class Word
    {
        public string boundingBox { get; set; }
        public string text { get; set; }
    }

    public class Line
    {
        public string boundingBox { get; set; }
        public List<Word> words { get; set; }
    }

    public class Region
    {
        public string boundingBox { get; set; }
        public List<Line> lines { get; set; }
    }

    [System.Serializable]
    public class RootObject
    {
        public string language { get; set; }
        public string orientation { get; set; }
        public double textAngle { get; set; }
        public List<Region> regions { get; set; }
    }
}
