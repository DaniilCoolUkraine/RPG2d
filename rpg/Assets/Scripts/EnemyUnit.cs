using System;
using UnityEngine;

public class EnemyUnit : Unit
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<IDamageable>() != null)
        {
            col.gameObject.GetComponent<IDamageable>().TakeDamage(1);
        }
    }
}