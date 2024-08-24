using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour, IResetableComponent
{
    private Color _initialColor;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void ResetInternalState()
    {
        _renderer.material.color = _initialColor;
    }
}
