using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour, IShooter 
{
    [SerializeField] private string pooledBulletTag;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float rateTime = 0.2f;
    private ObjectPooler objectPooler;


    private void Start() {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(AutoShoot());
    }

    public IEnumerator AutoShoot()
    {
        while (true)
        {
            GameObject bullet = objectPooler.GetObjectFromPool(pooledBulletTag);
            if (bullet != null)
            {
                
                //setup the bullet
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);

                //Shoot the bullet
                Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
                rb2d.velocity = transform.up * bulletSpeed;
                
            }
            yield return new WaitForSeconds(rateTime);
        }
    }
}
