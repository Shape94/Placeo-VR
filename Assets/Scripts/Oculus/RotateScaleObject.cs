using UnityEngine;

public class RotateScaleObject : MonoBehaviour
{
    public bool isRotating = false;
    public float rotationSpeed = 50f;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation(float stopAngle)
    {
        isRotating = false;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y = stopAngle;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
}
