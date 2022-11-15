using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] private ParticleSystem particles;
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Instantiate(particles, transform.position, Quaternion.identity);
    }
}
