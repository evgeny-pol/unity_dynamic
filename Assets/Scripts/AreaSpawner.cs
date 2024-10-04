using System.Collections;
using UnityEngine;

public class AreaSpawner : Spawner<PoolableObject>
{
    [SerializeField] private Vector3 _area;
    [SerializeField] private float _interval;

    private Coroutine _spawnCoroutine;
    private Vector3 _spawnAreaCorner;

    protected override void Awake()
    {
        _spawnAreaCorner = transform.position - _area / 2;
        base.Awake();
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnRepeatedly());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    protected override void ActionOnGet(PoolableObject obj)
    {
        SetRandomPosition(obj);
        base.ActionOnGet(obj);
    }

    private void SetRandomPosition(PoolableObject obj)
    {
        Vector3 position = _spawnAreaCorner + VectorUtils.GetRandomVector(_area);
        obj.transform.SetPositionAndRotation(position, Random.rotation);
    }

    private IEnumerator SpawnRepeatedly()
    {
        var delay = new WaitForSeconds(_interval);

        while (enabled)
        {
            Spawn();
            yield return delay;
        }
    }
}
