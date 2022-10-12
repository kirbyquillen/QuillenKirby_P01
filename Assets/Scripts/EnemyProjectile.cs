using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        Health pHealth = other.gameObject.GetComponent<Health>();
        Enemy me = other.gameObject.GetComponent<Enemy>();

        if (me == null && player != null && pHealth != null)
        {
            pHealth.TakeDamage(1);
            Destroy(gameObject);
        }
        else if (me == null && pHealth != null)
            Destroy(gameObject);
 
    }
}
