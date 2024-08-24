using System;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public event Action<PoolableObject> Deactivated;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }

    public void ResetInternalState()
    {
        foreach (IResetableComponent resetableComponent in GetComponents<IResetableComponent>())
            resetableComponent.ResetInternalState();
    }
}
