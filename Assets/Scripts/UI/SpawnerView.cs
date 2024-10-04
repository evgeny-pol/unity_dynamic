using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private SpawnerInfo _spawner;
    [SerializeField] private TextMeshProUGUI _spawnedCountText;
    [SerializeField] private TextMeshProUGUI _createdCountText;
    [SerializeField] private TextMeshProUGUI _activeCountText;

    private void OnEnable()
    {
        _spawner.ObjectSpawned += UpdateCounts;
        _spawner.ObjectDespawned += UpdateCounts;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= UpdateCounts;
        _spawner.ObjectDespawned -= UpdateCounts;
    }

    private void UpdateCounts(GameObject obj)
    {
        _spawnedCountText.text = _spawner.SpawnedCount.ToString();
        _createdCountText.text = _spawner.CreatedCount.ToString();
        _activeCountText.text = _spawner.ActiveCount.ToString();
    }
}
