using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Spawner<PoolableObject> _bombContainerSpawner;

    private void OnEnable()
    {
        _bombContainerSpawner.ObjectDespawned += OnBombContainerDeactivated;
    }

    private void OnDisable()
    {
        _bombContainerSpawner.ObjectDespawned -= OnBombContainerDeactivated;
    }

    private void OnBombContainerDeactivated(GameObject bombContainer)
    {
        Bomb bomb = Spawn();
        bomb.transform.SetPositionAndRotation(bombContainer.transform.position, bombContainer.transform.rotation);
        bomb.Explode();
    }
}
