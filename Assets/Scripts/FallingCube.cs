using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FallingCube : PoolableObject, IResetableComponent
{
    [SerializeField, Min(0f)] private float _minDelay;
    [SerializeField, Min(0f)] private float _maxDelay;

    private bool _hasCollided;
    private Coroutine _deactivationCoroutine;
    private Color _initialColor;
    private Renderer _renderer;

    private void OnValidate()
    {
        _maxDelay = Mathf.Max(_minDelay, _maxDelay);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided)
            return;

        if (collision.gameObject.TryGetComponent(out Platform _) == false)
            return;

        _hasCollided = true;
        SetRandomColor();
        _deactivationCoroutine = StartCoroutine(WaitAndDeactivate());
    }

    public void ResetComponentState()
    {
        _hasCollided = false;
        _renderer.material.color = _initialColor;

        if (_deactivationCoroutine != null)
            StopCoroutine(_deactivationCoroutine);
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
        Deactivate();
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
