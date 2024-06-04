using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHeath;
    [SerializeField] private bool isInvulnerable;
    private float currentHeath;
    private bool isDead;
    // private CameraShake cameraShake;
    private void Start() {
        isDead = false;
        // cameraShake = GetComponent<CameraShake>();
        currentHeath = maxHeath;
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
        // if (useHitCameraEffect && cameraShake != null)
        // {
        //     cameraShake.Play(); // shake the camera
        //     AudioManager.Instance.PlayShakeClip(); // play shake camera clip
        // }
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
