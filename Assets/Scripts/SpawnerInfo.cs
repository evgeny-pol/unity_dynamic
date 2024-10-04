using System;
using UnityEngine;

public abstract class SpawnerInfo : MonoBehaviour
{
    public event Action<GameObject> ObjectSpawned;
    public event Action<GameObject> ObjectDespawned;

    public abstract int SpawnedCount { get; }

    public abstract int CreatedCount { get; }

    public abstract int ActiveCount { get; }

    protected void NotifyObjectSpawned(GameObject obj) => ObjectSpawned?.Invoke(obj);

    protected void NotifyObjectDespawned(GameObject obj) => ObjectDespawned?.Invoke(obj);
}
