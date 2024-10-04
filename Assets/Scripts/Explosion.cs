using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _force = 1f;
    [SerializeField, Min(0f)] private float _radius = 1f;

    public void Explode()
    {
        Collider[] affectedColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in affectedColliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
                rigidbody.AddExplosionForce(_force, transform.position, _radius);
        }
    }
}
