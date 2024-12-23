using UnityEngine;

public interface ISequenceController
{
    void OnPatchInteraction(Collider other);
    void OnDispenserInteraction(Collider other);
    void OnPlayerSits(Collider other);
    void OnTriggerEnter(Collider other);
}
