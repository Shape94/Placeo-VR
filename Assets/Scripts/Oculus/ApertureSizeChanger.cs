

using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ApertureSizeChanger : MonoBehaviour
{
    public TunnelingVignetteController vignetteController;
    public Transform player;
    public GameObject[] roomBoundaries;
    public GameObject[] doors;

    private Collider[] ceilingColliders;
    private BoxCollider[] doorColliders;
    private MethodInfo updateMethod;

    public float vignetteDistanceThreshold = 0.2f;
    private float updateTime = 0.5f;

    private void Start()
    {
        // Assicurati che il vignetteController sia assegnato
        if (vignetteController == null)
        {
            vignetteController = GetComponent<TunnelingVignetteController>();
        }

        InitializeColliders();
        CacheUpdateMethod();

        vignetteController.currentParameters.featheringEffect = 0.2f;
        StartCoroutine(ChangeApertureSize());
    }

    private void InitializeColliders()
    {
        ceilingColliders = new Collider[roomBoundaries.Length];
        doorColliders = new BoxCollider[doors.Length];

        for (int i = 0; i < roomBoundaries.Length; i++)
        {
            var boundaryColliders = roomBoundaries[i].GetComponentsInChildren<Collider>();
            ceilingColliders[i] = boundaryColliders[boundaryColliders.Length - 1];
        }

        if (doors.Length > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doorColliders[i] = doors[i].GetComponent<BoxCollider>();
            }
        }
    }

    private void CacheUpdateMethod()
    {
        updateMethod = typeof(TunnelingVignetteController).GetMethod(
            "UpdateTunnelingVignette",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
    }

    private IEnumerator ChangeApertureSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTime);

            var (isInsideRoom, isInsideDoor, minDistance, nearestExitDistance) = CheckPlayerPosition();

            UpdateVignetteParameters(isInsideRoom, isInsideDoor, minDistance, nearestExitDistance);

            // Aggiorna il tempo tra le verifiche
            updateTime = vignetteController.currentParameters.apertureSize != 1 ? 0.1f : 0.5f;
        }
    }

    private (bool, bool, float, float) CheckPlayerPosition()
    {
        bool isInsideRoom = false;
        bool isInsideDoor = false;
        float minDistance = float.MaxValue;
        float nearestExitDistance = float.MaxValue;

        Vector3 playerPositionXZ = new Vector3(player.position.x, 0, player.position.z);

        // Calcola la distanza dal soffitto
        foreach (Collider ceilingCollider in ceilingColliders)
        {
            var bounds = ceilingCollider.bounds;
            Vector3 ceilingCenterXZ = new Vector3(bounds.center.x, 0, bounds.center.z);
            Vector3 ceilingSizeXZ = new Vector3(bounds.size.x / 2, 0, bounds.size.z / 2);

            float distance = CalculateDistance(playerPositionXZ, ceilingCenterXZ, ceilingSizeXZ);

            if (distance > 0)
            {
                isInsideRoom = true;
                minDistance = Mathf.Min(minDistance, distance);
            }
            else
            {
                nearestExitDistance = Mathf.Min(nearestExitDistance, distance);
            }
        }

        // Verifica se il giocatore è dentro una porta
        if (doorColliders.Length > 0)
        {
            foreach (BoxCollider doorCollider in doorColliders)
            {
                if (doorCollider == null) continue;

                var bounds = doorCollider.bounds;
                Vector3 doorCenterXZ = new Vector3(bounds.center.x, 0, bounds.center.z);
                Vector3 doorSizeXZ = new Vector3(bounds.size.x / 2, 0, bounds.size.z / 2);

                float distance = CalculateDistance(playerPositionXZ, doorCenterXZ, doorSizeXZ);

                if (distance > 0)
                {
                    isInsideDoor = true;
                    break;
                }
            }
        }

        return (isInsideRoom, isInsideDoor, minDistance, nearestExitDistance);
    }

    private float CalculateDistance(Vector3 playerPositionXZ, Vector3 centerXZ, Vector3 sizeXZ)
    {
        float distanceX = Mathf.Max(0, sizeXZ.x - Mathf.Abs(playerPositionXZ.x - centerXZ.x));
        float distanceZ = Mathf.Max(0, sizeXZ.z - Mathf.Abs(playerPositionXZ.z - centerXZ.z));
        return Mathf.Min(distanceX, distanceZ);
    }

    private void UpdateVignetteParameters(bool isInsideRoom, bool isInsideDoor, float minDistance, float nearestExitDistance)
    {
        if (isInsideRoom && nearestExitDistance > -vignetteDistanceThreshold && !isInsideDoor)
        {
            vignetteController.currentParameters.featheringEffect = 0.2f;
            StartCoroutine(GraduallyChangeApertureSize(Mathf.Clamp01(minDistance / vignetteDistanceThreshold)));
        }
        else if (!isInsideRoom)
        {
            vignetteController.currentParameters.featheringEffect = 0f;
            vignetteController.currentParameters.apertureSize = 0f;
        }
        else
        {
            StartCoroutine(GraduallyIncreaseApertureSize());
        }
    }

    private IEnumerator GraduallyIncreaseApertureSize()
    {
        yield return GraduallyChangeApertureSize(1f);
    }

    private IEnumerator GraduallyChangeApertureSize(float targetApertureSize)
    {
        float duration = 0.2f;
        float startValue = vignetteController.currentParameters.apertureSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            vignetteController.currentParameters.apertureSize = Mathf.Lerp(startValue, targetApertureSize, elapsedTime / duration);
            InvokeUpdateMethod();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignetteController.currentParameters.apertureSize = targetApertureSize;
        InvokeUpdateMethod();
    }

    private void InvokeUpdateMethod()
    {
        updateMethod.Invoke(vignetteController, new object[] { vignetteController.currentParameters });
    }
}
