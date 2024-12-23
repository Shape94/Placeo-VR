using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpandableSimulation : MonoBehaviour
{
    public Toggle toggle;
    public GameObject targetObject;
    public float animationDuration = 0.5f;
    private int maxHeight;

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        OnToggleValueChanged(toggle.isOn);
        int innerSettingsCount = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).childCount - 1;
        Debug.Log(innerSettingsCount);
        maxHeight = 75 + innerSettingsCount * (3+60);
        maxHeight = 450; //da cambiare
    }

    private void OnToggleValueChanged(bool isToggleOn)
    {
        float newHeight = isToggleOn ? maxHeight : 75f;
        ResizeObjectHeight(newHeight);
    }

    private void ResizeObjectHeight(float newHeight)
    {
        if (targetObject.GetComponent<RectTransform>() != null)
        {
            // Se l'oggetto è un UI element
            RectTransform rectTransform = targetObject.GetComponent<RectTransform>();
            rectTransform.DOSizeDelta(new Vector2(rectTransform.sizeDelta.x, newHeight), animationDuration);
        }
        else
        {
            // Se l'oggetto è un oggetto 3D
            Vector3 currentScale = targetObject.transform.localScale;
            targetObject.transform.DOScaleY(newHeight / 100f, animationDuration);
        }
    }
}