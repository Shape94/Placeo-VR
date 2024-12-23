using UnityEngine;

public class GameObjectReference : MonoBehaviour
{
    public static GameObjectReference Instance;
    public Transform targetObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional if you want it to persist
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
