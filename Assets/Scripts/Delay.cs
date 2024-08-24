using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Delay : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _min;
    [SerializeField, Min(0f)] private float _max;
    [SerializeField] private UnityEvent _delayPassed;

    private Coroutine _delayCoroutine;

    private void OnValidate()
    {
        _max = Mathf.Max(_min, _max);
    }

    public void Activate()
    {
        if (_delayCoroutine != null)
            StopCoroutine(_delayCoroutine);

        _delayCoroutine = StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Random.Range(_min, _max));
        _delayPassed?.Invoke();
    }
}
