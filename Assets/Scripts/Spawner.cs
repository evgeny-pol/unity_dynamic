using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3 _area;
    [SerializeField] private float _interval;
    [SerializeField] private PoolableObject _objectToSpawn;

    private Coroutine _spawnCoroutine;
    private ObjectPool<PoolableObject> _pool;
    private Vector3 _spawnAreaCorner;

    private void Awake()
    {
        _spawnAreaCorner = transform.position - _area / 2;
        _pool = new ObjectPool<PoolableObject>(
            createFunc: CreateNew,
            actionOnGet: ActionOnGet,
            actionOnRelease: obj => obj.gameObject.SetActive(false),
            actionOnDestroy: DestroyObject);
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _area);
    }

    private IEnumerator Spawn()
    {
        var delay = new WaitForSeconds(_interval);

        while (enabled)
        {
            _pool.Get();
            yield return delay;
        }
    }

    private PoolableObject CreateNew()
    {
        PoolableObject newObj = Instantiate(_objectToSpawn);
        newObj.Deactivated += OnObjectDeactivated;
        return newObj;
    }

    private void DestroyObject(PoolableObject obj)
    {
        obj.Deactivated -= OnObjectDeactivated;
        Destroy(obj.gameObject);
    }

    private void ActionOnGet(PoolableObject obj)
    {
        SetRandomPosition(obj);
        obj.ResetInternalState();
        obj.gameObject.SetActive(true);
    }

    private void SetRandomPosition(PoolableObject obj)
    {
        Vector3 position = _spawnAreaCorner + VectorUtils.GetRandomVector(_area);
        obj.transform.SetPositionAndRotation(position, Random.rotation);
    }

    private void OnObjectDeactivated(PoolableObject obj)
    {
        _pool.Release(obj);
    }
}
