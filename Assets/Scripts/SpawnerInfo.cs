using System;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    public event Action<GameObject> ObjectSpawned;
    public event Action<GameObject> ObjectDespawned;

    public int SpawnedCount { get; set; }
    public int CreatedCount { get; set; }
    public int ActiveCount { get; set; }

    public void NotifyObjectSpawned(GameObject obj) => ObjectSpawned?.Invoke(obj);

    public void NotifyObjectDespawned(GameObject obj) => ObjectDespawned?.Invoke(obj);
}
