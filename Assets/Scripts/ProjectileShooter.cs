using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Projectile projectilePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var position = transform.position + transform.forward;
            var rotation = transform.rotation;
            var projectile = Instantiate(projectilePrefab, position, rotation);
            projectile.Fire(projectileSpeed, transform.forward);
        }
    }
}

