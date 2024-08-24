using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ResetableRigidbody : MonoBehaviour, IResetableComponent
{
    public void ResetInternalState()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
