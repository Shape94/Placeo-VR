using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : CustomUIComponent
{
    public ViewSO viewData;

    public GameObject containerTop;
    public GameObject containerCenter;
    public GameObject containerBottom;

    private Image imageTop;
    private Image imageCenter;
    private Image imageBottom;

    private VerticalLayoutGroup verticalLayoutGroup;

    /*private void Awake(){
        Init();
    }

    public void Init(){
        Setup();
        Configure();
    }*/

    public override void Setup(){
    verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

    if (containerTop != null)
        imageTop = containerTop.GetComponent<Image>();
    if (containerCenter != null)
        imageCenter = containerCenter.GetComponent<Image>();
    if (containerBottom != null)
        imageBottom = containerBottom.GetComponent<Image>();
}


    public override void Configure(){
        verticalLayoutGroup.padding = viewData.padding;
        verticalLayoutGroup.spacing = viewData.spacing;

        if (containerTop != null)
            imageTop.color = viewData.theme.primary_bg;;

        imageCenter.color = viewData.theme.secondary_bg;

        if (containerBottom != null)
            imageBottom.color = viewData.theme.tertiary_bg;;
    }

    /*public void OnValidate(){
        Init();
    }*/

}
