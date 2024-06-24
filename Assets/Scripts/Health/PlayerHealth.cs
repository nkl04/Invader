using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float MaxHealth { get => maxHeath; set => maxHeath = value; }
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHeath;
    [SerializeField] private bool isInvulnerable = false;

    //Camera 
    private Camera cam;
    private CameraShake cameraShake;

    //Health
    private float currentHeath;
    private bool isDead;
    private void Awake()
    {
        cam = Camera.main;
        isDead = false;
        cameraShake = cam.GetComponent<CameraShake>();

    }

    private void Start()
    {
        currentHeath = maxHeath;
        healthBar.SetMaxHeath(maxHeath);
    }



    public void TakeDamage(float damage)
    {
        // if player is invulnerable, can't take damage
        if (isInvulnerable)
        {
            return;
        }
        // take damage
        currentHeath -= damage;
        healthBar.SetHeath(currentHeath);
        if (cameraShake != null)
        {
            cameraShake.Play(); // shake the camera
        }
        if (currentHeath <= 0)
        {
            if (!isDead)
            {
                //Die
                isDead = true;
                Die();
            }
        }

    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    public void RecoverHeath(float recoverValue)
    {
        currentHeath += recoverValue;
        if (currentHeath >= maxHeath)
        {
            currentHeath = maxHeath;
        }
    }

    public void ResetHealth()
    {
        healthBar.SetMaxHeath(maxHeath);
        currentHeath = maxHeath;
    }
}
