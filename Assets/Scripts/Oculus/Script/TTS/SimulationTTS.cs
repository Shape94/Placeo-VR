using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using TMPro;

public class SimulationTTS : MonoBehaviour
{
    public string inputField;
    public AudioSource audioSource;
    public Animator animator; // Riferimento all'animator

    private const string SubscriptionKey = ""; // Inserisci SubscriptionKey MySpeechServices
    private const string Region = "westeurope"; // Inserisci Region MySpeechServices
    private const int SampleRate = 24000;
    public string voiceName;

    private bool waitingForSpeak;
    private TimeSpan audioDuration;
    public string message;

    private SpeechConfig speechConfig;
    private SpeechSynthesizer synthesizer;

    void Start()
    {
        if (inputField == null)
        {
            message = "inputField property is null! Assign a UI InputField element to it.";
            UnityEngine.Debug.LogError(message);
        }

        message = "Click button to synthesize speech";

        speechConfig = SpeechConfig.FromSubscription(SubscriptionKey, Region);
        speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw24Khz16BitMonoPcm);
        speechConfig.SpeechSynthesisVoiceName = voiceName;

        synthesizer = new SpeechSynthesizer(speechConfig, null);

        synthesizer.SynthesisCanceled += (s, e) =>
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(e.Result);
            message = $"CANCELED:\nReason=[{cancellation.Reason}]\nErrorDetails=[{cancellation.ErrorDetails}]\nDid you update the subscription info?";
        };
    }

    public void Speak(string message)
    {
        inputField = message;
        StartCoroutine(SpeakCoroutine());
    }

    private IEnumerator SpeakCoroutine()
    {
        waitingForSpeak = true;

        string newMessage = null;
        var startTime = DateTime.Now;

        var task = synthesizer.StartSpeakingTextAsync(inputField);
        
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

        synthesizer.SynthesisCompleted += (s, e) =>
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(e.Result);
            message = $"SynthesisCompleted:\nAudioData: {e.Result.AudioData.Length} bytes\nAudioDuration: {e.Result.AudioDuration}";

            // Ottieni la durata dell'audio come TimeSpan
            audioDuration = e.Result.AudioDuration;
        };

        // Avvia la coroutine per attendere la durata dell'audio e quindi impostare il trigger
        StartCoroutine(WaitAndTrigger());

        waitingForSpeak = false;

        yield return null; // Completa la coroutine
    }

    void OnDestroy()
    {
        if (synthesizer != null)
        {
            synthesizer.Dispose();
        }
    }

    // Coroutine per attendere la durata dell'audio
    private IEnumerator WaitAndTrigger()
    {
        yield return new WaitForSeconds(2.0f);
        yield return new WaitForSeconds(((float)audioDuration.TotalSeconds - 2.0f));
        animator.SetTrigger("StopTalk");
    }
}
