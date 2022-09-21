using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }

    public void PlayerImpact(Player player)
    {
        player.DecreaseHealth(_damageAmount);
    }

    private void ImpactFeedback()
    {
        if(_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
        }
        if(_impactSound != null)
        {
            AudioHelper.Play2DClip(_impactSound, 1f);
        }
    }

    public void Die()
    {
        _impactParticles.Play();
        AudioHelper.Play2DClip(_impactSound, 1f);
        Destroy(this);
    }
}
