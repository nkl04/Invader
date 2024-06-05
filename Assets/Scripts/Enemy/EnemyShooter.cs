using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
                bullet.SetActive(true);

                //shoot the bullet down
                Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
                rb2d.velocity = transform.up * bulletSpeed;

            }
            
            rateTime = Random.Range(minimumRate, maximumRate);
            
            yield return new WaitForSeconds(rateTime);
        }
    }
}
