using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpawnerInfo))]
public class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private T _objectToSpawn;

    private ObjectPool<T> _pool;
    private SpawnerInfo _spawnerInfo;

    protected virtual void Awake()
    {
        _spawnerInfo = GetComponent<SpawnerInfo>();
        _pool = new ObjectPool<T>(
            createFunc: CreateNew,
            actionOnGet: ActionOnGet,
            actionOnRelease: obj => obj.gameObject.SetActive(false),
            actionOnDestroy: DestroyObject);
    }

    protected T Spawn()
    {
        T obj = _pool.Get();
        _spawnerInfo.SpawnedCount++;
        _spawnerInfo.ActiveCount++;
        _spawnerInfo.NotifyObjectSpawned(obj.gameObject);
        return obj;
    }

    protected virtual void ActionOnGet(T obj)
    {
        obj.ResetInternalState();
        obj.gameObject.SetActive(true);
    }

    private T CreateNew()
    {
        T newObj = Instantiate(_objectToSpawn);
        newObj.Deactivated += OnObjectDeactivated;
        _spawnerInfo.CreatedCount++;
        return newObj;
    }

    private void DestroyObject(T obj)
    {
        obj.Deactivated -= OnObjectDeactivated;
        Destroy(obj.gameObject);
    }

    private void OnObjectDeactivated(PoolableObject obj)
    {
        _pool.Release(obj as T);
        _spawnerInfo.ActiveCount--;
        _spawnerInfo.NotifyObjectDespawned(obj.gameObject);
    }
}
