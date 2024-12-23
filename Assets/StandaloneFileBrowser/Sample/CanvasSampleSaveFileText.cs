using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using TMPro;

[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler {
    public Text output;
    public TextMeshProUGUI json;
    public GameObject nameSimulation;  


    // Sample text data
    private string _data = "Example text created by StandaloneFileBrowser";

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        var bytes = Encoding.UTF8.GetBytes(json.text);
        DownloadFile(gameObject.name, "OnFileDownload", "sample.txt", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
        //output.text = "File Successfully Downloaded";
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    // Listen OnClick event in standlone builds
    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() {
        string sanitizedFileName = nameSimulation.name.Replace(":", "-").Replace("/", "-"); //I ":" e "/" sono presenti nell'orario, è un carattere non permesso nei nomi dei file

        var path = StandaloneFileBrowser.SaveFilePanel("Scegli la posizione in cui salvare il file, NON cambiare il nome", "", sanitizedFileName, "json");
        if (!string.IsNullOrEmpty(path)) {
            File.WriteAllText(path, json.text);
        }
    }
#endif
}