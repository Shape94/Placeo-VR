using System.Collections;
using UnityEngine;

public class PatchController : MonoBehaviour
{
    public GameObject patch; // Assegna il materiale tramite l'editor

    // Metodo pubblico per avviare l'animazione dell'opacità
    public void ApplyPatch()
    {
        patch.SetActive(true);
    }
}
