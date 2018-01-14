using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraCapture : MonoBehaviour {

    public RectTransform rectT; // Assign the UI element which you wanna capture
    int width; // width of the object to capture
    int height; // height of the object to capture
    string fileName;

    public static CameraCapture instance;
	// Use this for initialization
	void Start () 
    {
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);	
        fileName = Path.Combine(Application.persistentDataPath, "test.png");
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame(); // it must be a coroutine 

        Vector2 temp = rectT.transform.position;
        var startX = temp.x - width / 2;
        var startY = temp.y - height / 2;

        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(fileName, bytes);
        print(Application.persistentDataPath);

    }

    public void SaveImage()
    {
        StartCoroutine(takeScreenShot());
    }
}
