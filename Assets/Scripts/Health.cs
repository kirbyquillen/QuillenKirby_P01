using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _health;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] AudioClip _killSound;

    public void TakeDamage(int amt)
    {
        _health -= amt;
        Debug.Log("Health: " + _health);
        if (_impactParticles != null)
        {
            _impactParticles.Play();
        }
        if (_impactSound != null)
        {
            AudioHelper.Play2DClip(_impactSound, 1f);
        }

        if (_health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        AudioHelper.Play2DClip(_killSound, 1f);
        Destroy(this.gameObject);
    }
}
