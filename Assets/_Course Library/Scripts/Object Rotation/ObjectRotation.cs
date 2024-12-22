using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public GameObject rotateObject;
    public float rotationSpeed = 60f;

    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        rotateObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

