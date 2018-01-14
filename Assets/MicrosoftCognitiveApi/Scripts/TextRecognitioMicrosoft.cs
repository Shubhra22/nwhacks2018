using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;


public class TextRecognitioMicrosoft : MonoBehaviour 
{
    string apiKey = "3a82a2012f2a4aa9841a0e4e1bc1bfac";
    string url = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/ocr";
    string fileName;


	// Use this for initialization
	void Start () 
    {
        fileName = Path.Combine(Application.persistentDataPath, "test.png"); 
       
        //InvokeRepeating("RecoText",5,5);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButtonDown(0))
        {
            RecoText();
        }
	}

    IEnumerator MakeOCRequest()
    {
        yield return new WaitForSeconds(3);
        byte[] bytes = GetImageAsByteArray(fileName);

        //byte[] bytes = CameraImageAccess.instance.pixelInfo;//GetImageAsByteArray(fileName);

        var headers = new Dictionary<string, string>() {             { "Ocp-Apim-Subscription-Key", apiKey },
            { "Content-Type", "application/octet-stream" }         }; 
        //string reqParam = "language=unk&detectOrientation=true";
        //string uri = url + "?" + reqParam;
         WWW www = new WWW(url, bytes, headers);
        yield return www;

        JsonDataManager.instance.LoadJsonData(www.text);
    }


    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
        BinaryReader binaryReader = new BinaryReader(fileStream);
        return binaryReader.ReadBytes((int)fileStream.Length);
    }

    public void RecoText()
    {
        // System.IO.File.WriteAllBytes(fileName, CameraImageAccess.instance.pixelInfo);
        //Application.CaptureScreenshot(fileName);
        CameraCapture.instance.SaveImage();
        StartCoroutine(MakeOCRequest());
    }
}
