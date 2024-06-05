using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooter : MonoBehaviour, IShooter 
{
    [SerializeField] private string pooledBulletTag;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float rateTime = 0.2f;

    private void Start() {
        StartCoroutine(AutoShoot());
    }

    public IEnumerator AutoShoot()
    {
        while (true)
        {
            GameObject bullet = ObjectPooler.Instance.GetObjectFromPool(pooledBulletTag);
            if (bullet != null)
            {
                
                //setup the bullet
                bullet.transform.SetPositionAndRotation(transform.position, quaternion.Euler(0,0,0));
                bullet.SetActive(true);

                //Shoot the bullet
                Rigidbody rb2d = bullet.GetComponent<Rigidbody>();
                rb2d.velocity = Vector2.up * bulletSpeed;
                
            }
            yield return new WaitForSeconds(rateTime);
        }
    }
}
