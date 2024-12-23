using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private LayoutElement targetLayoutElement; // Layout Element da modificare
    [SerializeField] private float smallWidth = 3.75f; // Larghezza flessibile quando premuto una volta
    [SerializeField] private float largeWidth = 100000f; // Larghezza flessibile quando premuto nuovamente
    [SerializeField] private float animationDuration = 0.5f; // Durata dell'animazione in secondi

    private bool isLarge = false; // Flag per tenere traccia dello stato corrente

    private void Start()
    {
        // Aggiungi un listener al bottone
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
      
        openClosehamburger();
    }

    private void UpdateWidth(float newWidth)
    {
        targetLayoutElement.flexibleWidth = newWidth;
    }

    public void openClosehamburger(){
        // Inverti lo stato
        isLarge = !isLarge;

        // Calcola la larghezza flessibile da utilizzare
        float targetWidth = isLarge ? largeWidth : smallWidth;

        // Seleziona l'easing appropriato in base allo stato corrente
        string easingType = isLarge ? "easeInExpo" : "easeOutExpo";

        // Crea l'animazione utilizzando iTween
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", targetLayoutElement.flexibleWidth,
            "to", targetWidth,
            "time", animationDuration,
            "onupdate", "UpdateWidth",
            "easetype", easingType
        ));
        }

        public void closeHamburger(){
        if(isLarge == false){
        // Inverti lo stato
        isLarge = !isLarge;

        // Calcola la larghezza flessibile da utilizzare
        float targetWidth = isLarge ? largeWidth : smallWidth;

        // Seleziona l'easing appropriato in base allo stato corrente
        string easingType = isLarge ? "easeInExpo" : "easeOutExpo";

        // Crea l'animazione utilizzando iTween
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", targetLayoutElement.flexibleWidth,
            "to", targetWidth,
            "time", animationDuration,
            "onupdate", "UpdateWidth",
            "easetype", easingType
        ));
        }}
}
