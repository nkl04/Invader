using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


public class EnemyShooter : MonoBehaviour, IShooter
{
    [Header("General")]
    [SerializeField] private string pooledBulletTag;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] [Range(3,5f)] float maximumRate;
    [SerializeField] [Range(1f,1.5f)] float minimumRate;
    
    private float rateTime;

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

                //shoot the bullet down
                Rigidbody rb2d = bullet.GetComponent<Rigidbody>();
                rb2d.velocity = Vector2.down * bulletSpeed;

            }
            
            rateTime = UnityEngine.Random.Range(minimumRate, maximumRate);
            
            yield return new WaitForSeconds(rateTime);
        }
    }
}
