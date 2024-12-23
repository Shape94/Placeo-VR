using UnityEngine;
using System.Collections;

public class ScaleLoop : MonoBehaviour
{
    public float maxScaleX = 2f;  // Scala massima per l'asse X
    public float minScaleY = 0.5f; // Scala minima per l'asse Y
    public float duration = 1f;     // Durata della transizione

    private void Start()
    {
        // Inizia il loop di scaling
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        while (true)
        {
            // Scala da X massimo a Y minimo
            iTween.ScaleTo(gameObject, iTween.Hash(
                "x", maxScaleX,
                "y", minScaleY,
                "time", duration,
                "easetype", iTween.EaseType.easeInOutSine
            ));
            yield return new WaitForSeconds(duration);

            // Scala da Y massimo a X minimo
            iTween.ScaleTo(gameObject, iTween.Hash(
                "x", minScaleY,
                "y", maxScaleX,
                "time", duration,
                "easetype", iTween.EaseType.easeInOutSine
            ));
            yield return new WaitForSeconds(duration);
        }
    }
}
