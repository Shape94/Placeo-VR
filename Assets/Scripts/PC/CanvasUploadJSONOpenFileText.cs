using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using System.IO;

[RequireComponent(typeof(Button))]
public class CanvasUploadJSONOpenFileText : MonoBehaviour, IPointerDownHandler {

    public PlayfabManager playfabManager;
    public string fileName;
    //public Text output;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".json", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Scegli il file .json della Simulazione che vuoi caricare", "", "json", false);
        if (paths.Length > 0) {
            fileName = Path.GetFileNameWithoutExtension(paths[0]);

             // Sostituire i primi due "-" con "/"
        fileName = ReplaceFirst(fileName, "-", "/");
        fileName = ReplaceFirst(fileName, "-", "/");

        // Sostituire gli ultimi due "-" con ":"
        fileName = ReplaceLast(fileName, "-", ":");
        fileName = ReplaceLast(fileName, "-", ":");


            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif

    // Metodo per sostituire la prima occorrenza
public string ReplaceFirst(string text, string search, string replace) {
    int pos = text.IndexOf(search);
    if (pos < 0) {
        return text;
    }
    return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
}

// Metodo per sostituire l'ultima occorrenza
public string ReplaceLast(string text, string search, string replace) {
    int pos = text.LastIndexOf(search);
    if (pos < 0) {
        return text;
    }
    return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
}
    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        //output.text = loader.text;
        Debug.Log(loader.text);
        playfabManager.UploadJSON(fileName, loader.text);
    }
}