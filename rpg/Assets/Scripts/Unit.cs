using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            _healthBar.UpdateScale(health);
        }
    }

    [SerializeField] private HealthBar _healthBar;

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}