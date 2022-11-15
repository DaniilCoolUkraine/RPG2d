using UnityEngine;

public class DestroyableEnvironment : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }
}
