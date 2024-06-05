using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHeath;
    // [SerializeField] private ParticleSystem deathEffect;
    private float currentHeath;
    private bool isDead;

    private void OnEnable() {
        isDead = false;
        currentHeath = maxHeath;
    }

    public void TakeDamage(float damage)
    {
        currentHeath -= damage;
        if (currentHeath <= 0)
        {
            if (!isDead)
            {
                //Die
                isDead = true;

                //Play death effect for enemy
                //PlayDeathEffect();
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
        throw new System.NotImplementedException();
    }

    // private void PlayDeathEffect()
    // {
    //     if (deathEffect != null)
    //     {
    //         ParticleSystem ptcEffect = Instantiate(deathEffect,transform.position,Quaternion.identity);
    //         Destroy(ptcEffect.gameObject,ptcEffect.main.duration + ptcEffect.main.startLifetime.constantMax);
    //     }
    // }
}
