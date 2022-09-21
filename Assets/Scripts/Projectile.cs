using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Fire(float speed, Vector3 direction)
    {
        _rb.velocity = direction * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        IDamageable ouch = other.gameObject.GetComponent<IDamageable>();

        if (player == null && ouch != null)
        {
            ouch.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
