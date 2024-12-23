using UnityEngine;

public class AnimateScalePopInOut : MonoBehaviour
{
    private Vector3 originalScale;

    void Start()
    {
        // Salva la scala originale del GameObject
        originalScale = transform.localScale;
    }

    // Metodo per ridurre la scala del GameObject
    public void ReduceScale()
    {
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", Vector3.zero,
            "time", 1.0f,
            "easetype", iTween.EaseType.easeInOutSine
        ));
    }

    // Metodo per ripristinare la scala originale del GameObject
    public void RestoreScale()
    {
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", originalScale,
            "time", 1.0f,
            "easetype", iTween.EaseType.easeInOutSine
        ));
    }
}
