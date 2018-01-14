using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;

public class TTSibm : MonoBehaviour
{

    string username = "74d69ef1-b6cf-4b52-a498-7d11abf32360";
    string password = "RsYWWoTJHmQd";
    string url = "https://stream.watsonplatform.net/text-to-speech/api";
    public static TTSibm instance;
   // string _testString = "This is a test";
    TextToSpeech _textToSpeech;
    void Start()
    {
        Credentials credentials = new Credentials( username,  password ,  url );
        _textToSpeech = new TextToSpeech(credentials);
        instance = this;
    }

    private void Synthesize()
    {
       // _textToSpeech.Voice = < voice - type >;
        //if (!_textToSpeech.ToSpeech(OnSynthesize, OnFail, < text - to - synthesize >, < use - post >))
            //Log.Debug("ExampleTextToSpeech.ToSpeech()", "Failed to synthesize!");
    }

    private void OnSynthesize(AudioClip clip, Dictionary<string, object> customData)
    {
        PlayClip(clip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null)
        {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            Destroy(audioObject, clip.length);
        }
    }

    public void PlayVoice(string _textString)
    {
        _textToSpeech.Voice = VoiceType.en_US_Allison;
        _textToSpeech.ToSpeech(HandleToSpeechCallback, OnFail, _textString, true);
    }

    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData = null)
    {
        PlayClip(clip);
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleTextToSpeech.OnFail()", "Error received: {0}", error.ToString());
    }
    // Update is called once per frame
    void Update()
    {

    }
}
