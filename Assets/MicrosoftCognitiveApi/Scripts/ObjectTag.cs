using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectTag : MonoBehaviour
{
    const string apiKey = "3a82a2012f2a4aa9841a0e4e1bc1bfac";
    const string url = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/describe";
    string imagePath;
    string fileName;
    Object jsonx;

    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("RecoText", 5, 5);
        fileName = Path.Combine(Application.streamingAssetsPath, "pix.png");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator sendReq()
    {
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", apiKey },
            { "Content-Type", "application/octet-stream" }
        };
        byte[] bytes = getImageAsByteArray(fileName);
        WWW res = new WWW(url, bytes, headers);
        yield return res;
        string json = res.text;
        Debug.Log(json);
        //Handle json
    }

    static byte[] getImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        return binaryReader.ReadBytes((int)fileStream.Length);
    }

    public void RecoText()
    {
        StartCoroutine(sendReq());
    }
}
