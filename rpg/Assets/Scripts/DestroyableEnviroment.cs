using UnityEngine;

public class DestroyableEnviroment : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }
}
