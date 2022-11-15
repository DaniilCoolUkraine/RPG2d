using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] private ParticleSystem onTakeDamageParticles;
    [SerializeField] private AudioSource[] _hurtSounds;
    
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Instantiate(onTakeDamageParticles, transform.position, Quaternion.identity);
        
        _hurtSounds[Random.Range(0, _hurtSounds.Length)].Play();
        
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
