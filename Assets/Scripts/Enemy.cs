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
        Health health = this.gameObject.GetComponent<Health>();

        if (player != null)
        {
            PlayerImpact(player);
            health.TakeDamage(1);
        }
    }

    public void PlayerImpact(Player player)
    {
        Health health = player.GetComponent<Health>();
        health.TakeDamage(1);
    }
}
