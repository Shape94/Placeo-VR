
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using TMPro;

public class TutorialTTS : MonoBehaviour
{
    public TextMeshProUGUI inputField;
    public Button speakButton;
    public AudioSource audioSource;

    private const string SubscriptionKey = "";
    private const string Region = "westeurope";
    private const int SampleRate = 24000;

    private bool waitingForSpeak;
    private string message;

    private SpeechConfig speechConfig;
    private SpeechSynthesizer synthesizer;

    public void ButtonClick()
    {
        if (!waitingForSpeak)
        {
            StartCoroutine(SpeakCoroutine());
        }
    }

    private IEnumerator SpeakCoroutine()
    {
        waitingForSpeak = true;

        string newMessage = null;
        var startTime = DateTime.Now;

        var task = synthesizer.StartSpeakingTextAsync(inputField.text);
        
        // Aspetta il completamento del task
        while (!task.IsCompleted)
        {
            yield return null; // Attendi un frame
        }

        var result = task.Result;

        var audioDataStream = AudioDataStream.FromResult(result);
        var isFirstAudioChunk = true;

        var audioClip = AudioClip.Create(
            "Speech",
            SampleRate * 600,
            1,
            SampleRate,
            true,
            (float[] audioChunk) =>
            {
                var chunkSize = audioChunk.Length;
                var audioChunkBytes = new byte[chunkSize * 2];
                var readBytes = audioDataStream.ReadData(audioChunkBytes);
                if (isFirstAudioChunk && readBytes > 0)
                {
                    var endTime = DateTime.Now;
                    var latency = endTime.Subtract(startTime).TotalMilliseconds;
                    newMessage = $"Speech synthesis succeeded!\nLatency: {latency} ms.";
                    isFirstAudioChunk = false;
                }

                for (int i = 0; i < chunkSize; ++i)
                {
                    if (i < readBytes / 2)
                    {
                        audioChunk[i] = (short)(audioChunkBytes[i * 2 + 1] << 8 | audioChunkBytes[i * 2]) / 32768.0F;
                    }
                    else
                    {
                        audioChunk[i] = 0.0f;
                    }
                }
            });

        audioSource.clip = audioClip;
        audioSource.Play();

        if (newMessage != null)
        {
            message = newMessage;
        }

        waitingForSpeak = false;

        yield return null; // Completa la coroutine
    }

    void Start()
    {
        if (inputField == null)
        {
            message = "inputField property is null! Assign a UI InputField element to it.";
            UnityEngine.Debug.LogError(message);
        }
        else if (speakButton == null)
        {
            message = "speakButton property is null! Assign a UI Button to it.";
            UnityEngine.Debug.LogError(message);
        }
        else
        {
            message = "Click button to synthesize speech";
            speakButton.onClick.AddListener(ButtonClick);

            speechConfig = SpeechConfig.FromSubscription(SubscriptionKey, Region);
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw24Khz16BitMonoPcm);
            speechConfig.SpeechSynthesisVoiceName = "it-IT-IsabellaNeural";

            synthesizer = new SpeechSynthesizer(speechConfig, null);

            synthesizer.SynthesisCanceled += (s, e) =>
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(e.Result);
                message = $"CANCELED:\nReason=[{cancellation.Reason}]\nErrorDetails=[{cancellation.ErrorDetails}]\nDid you update the subscription info?";
            };
        }
    }

    void Update()
    {
        if (speakButton != null)
        {
            speakButton.interactable = !waitingForSpeak;
        }
    }

    void OnDestroy()
    {
        if (synthesizer != null)
        {
            synthesizer.Dispose();
        }
    }
}

