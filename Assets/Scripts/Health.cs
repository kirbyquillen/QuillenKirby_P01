using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int _health;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] AudioClip _killSound;

    public Slider slider;
    public GameObject panel;

    Rigidbody _rb;

    private void Awake()
    {
        panel.SetActive(false);
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        slider.value = _health;
    }
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
        if (panel != null)
        {

        }
        if (_health < 0)
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
