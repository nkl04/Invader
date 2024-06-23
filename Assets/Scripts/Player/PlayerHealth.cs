using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHeath;
    [SerializeField] private bool isInvulnerable;

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
        currentHeath = maxHeath;

        GameController.Instance.OnGameStateUpdated += Instance_OnGameStateUpdated;
    }

    private void Instance_OnGameStateUpdated(object sender, System.EventArgs e)
    {
        isInvulnerable = true;
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
        Debug.Log("Player Health: " + currentHeath);
        if (cameraShake != null)
        {
            cameraShake.Play(); // shake the camera
            //AudioManager.Instance.PlayShakeClip(); // play shake camera clip
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
}
