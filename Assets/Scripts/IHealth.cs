using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    public void TakeDamage(float damage);
    public void RecoverHeath(float recoverValue);
    public void Die();
}
