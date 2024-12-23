using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class showArchiveButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button buttonDelete;
    public Button buttonDownload;
    public Toggle toggleExpandable;
    public GameObject buttonStart;
    public Image expandImage;


    private Image buttonDeleteImage;
    private Image buttonDownloadImage;
    private Image buttonStartImage;
    private TextMeshProUGUI buttonStartText;
    private Button buttonDeleteButton;

    private void Awake()
    {
        buttonDeleteImage = buttonDelete.GetComponent<Image>();
        buttonDeleteButton = buttonDelete.GetComponent<Button>();
        buttonDownloadImage = buttonDownload.GetComponent<Image>();
        buttonStartImage = buttonStart.GetComponentInChildren<Image>();
        buttonStartText = buttonStart.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowButtons(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!toggleExpandable.isOn)
        {
            ShowButtons(false);
        }
    }

    public void ShowHideButtons()
    {
        expandImage.transform.Rotate(0, 0, 180);

        if (toggleExpandable.isOn)
        {
            ShowButtons(true);
        }else{
        expandImage.transform.Rotate(0, 0, 0);
        }
    }

    private void ShowButtons(bool show)
    {
        buttonDeleteImage.enabled = show;
        buttonDownloadImage.enabled = show;
        buttonStartImage.enabled = show;
        buttonStartText.enabled = show;

        if(buttonStart.activeSelf == false){
            buttonDeleteImage.enabled = false;
            buttonDeleteButton.enabled = false;
            buttonDownload.enabled = false;
            buttonDownloadImage.enabled = false;
        }
}
    }