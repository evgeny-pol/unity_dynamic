using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : SpawnerInfo where T : PoolableObject
{
    [SerializeField] private T _objectToSpawn;

    private ObjectPool<T> _pool;
    private int _spawnedCount;

    public override int SpawnedCount => _spawnedCount;

    public override int CreatedCount => _pool.CountAll;

    public override int ActiveCount => _pool.CountActive;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateNew,
            actionOnGet: ActionOnGet,
            actionOnRelease: obj => obj.gameObject.SetActive(false),
            actionOnDestroy: DestroyObject);
    }

    protected T Spawn()
    {
        T obj = _pool.Get();
        NotifyObjectSpawned(obj.gameObject);
        return obj;
    }

    protected virtual void ActionOnGet(T obj)
    {
        obj.ResetInternalState();
        obj.gameObject.SetActive(true);
        ++_spawnedCount;
    }

    private T CreateNew()
    {
        T newObj = Instantiate(_objectToSpawn);
        newObj.Deactivated += OnObjectDeactivated;
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
        NotifyObjectDespawned(obj.gameObject);
    }
}
