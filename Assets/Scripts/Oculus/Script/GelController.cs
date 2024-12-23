using System.Collections;
using UnityEngine;

public class GelController : MonoBehaviour
{
    public Material material; // Assegna il materiale tramite l'editor

    // Metodo pubblico per avviare l'animazione dell'opacità
    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    // Coroutine per gestire l'animazione dell'opacità
    private IEnumerator FadeOutCoroutine(float duration)
    {
        Color color = material.color;

        // Imposta l'opacità iniziale a 255
        color.a = 1f;
        material.color = color;

        float startOpacity = color.a;
        float endOpacity = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, endOpacity, elapsedTime / duration);
            color.a = newOpacity;
            material.color = color;
            yield return null;
        }

        // Assicurati che l'opacità finale sia esattamente 0
        color.a = endOpacity;
        material.color = color;
    }
}
