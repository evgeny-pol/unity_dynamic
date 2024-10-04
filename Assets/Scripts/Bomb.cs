using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
[RequireComponent(typeof(Renderer))]
public class Bomb : PoolableObject, IResetableComponent
{
    [SerializeField, Min(0f)] private float _minDelay;
    [SerializeField, Min(0f)] private float _maxDelay;

    private Coroutine _explosionCoroutine;
    private Explosion _explosion;
    private Renderer _renderer;

    private void OnValidate()
    {
        _maxDelay = Mathf.Max(_minDelay, _maxDelay);
    }

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
        _renderer = GetComponent<Renderer>();
    }

    public void Explode()
    {
        if (_explosionCoroutine != null)
            StopCoroutine(_explosionCoroutine);

        _explosionCoroutine = StartCoroutine(WaitAndExplode());
    }

    public void ResetComponentState()
    {
        SetAlpha(1);

        if (_explosionCoroutine != null)
            StopCoroutine(_explosionCoroutine);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }

    private IEnumerator WaitAndExplode()
    {
        float explosionDelay = Random.Range(_minDelay, _maxDelay);
        float timeRemaining = explosionDelay;

        while (timeRemaining > 0)
        {
            SetAlpha(timeRemaining / explosionDelay);
            yield return null;
            timeRemaining -= Time.deltaTime;
        }

        SetAlpha(0f);
        _explosion.Explode();
        Deactivate();
    }
}
