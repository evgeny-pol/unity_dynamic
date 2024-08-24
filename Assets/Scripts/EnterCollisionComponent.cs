using UnityEngine;
using UnityEngine.Events;

public class EnterCollisionComponent : MonoBehaviour, IResetableComponent
{
    [SerializeField] private string _tag;
    [Tooltip("Максимальное количество активаций события. 0 - количество не ограничено.")]
    [SerializeField] private int _maxActivations;
    [SerializeField] private UnityEvent Entered;

    private int _activationsCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (_maxActivations > 0 && _activationsCount == _maxActivations)
            return;

        if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag))
            return;

        if (_maxActivations > 0)
            ++_activationsCount;

        Entered?.Invoke();
    }

    public void ResetInternalState()
    {
        _activationsCount = 0;
    }
}
