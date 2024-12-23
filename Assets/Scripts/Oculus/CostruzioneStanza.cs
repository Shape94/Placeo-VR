using UnityEngine;

public class CostruzioneStanza : MonoBehaviour
{
    //Inserisci tutti i prefab base
    public GameObject poltronaPrefab;

    //Questo da eliminare quando c'è JSON 
    public bool deveAggiungerePoltrone = true;


    void Start()
    {
        CostruisciStanza();
    }

    void CostruisciStanza() //questo sarà con i parametri del JSON
    {      
        GameObject[] emptyPoltrone = GameObject.FindGameObjectsWithTag("Poltrona");

        if (deveAggiungerePoltrone) //questo sarà con il parametro/i del JSON
        {
            foreach (GameObject emptyPoltrona in emptyPoltrone)
            {
                Instantiate(poltronaPrefab, emptyPoltrona.transform.position, emptyPoltrona.transform.rotation).transform.localScale = emptyPoltrona.transform.localScale;
                Destroy(emptyPoltrona); // Elimina l'istanza originale, se necessario
            }
        }else{
            foreach (GameObject emptyPoltrona in emptyPoltrone)
            {
                Destroy(emptyPoltrona); // Elimina l'istanza originale, se necessario
            }
        }
    }
}
