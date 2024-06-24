using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooter : MonoBehaviour, IShooter
{
    public string PooledBulletTag { get => pooledBulletTag; set => pooledBulletTag = value; }

    [SerializeField] private string pooledBulletTag;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float rateTime = 0.2f;
    private List<Transform> shootingPointList;

    private void Start()
    {
        StartCoroutine(AutoShoot());
        PlayerManager.Instance.OnPlayerModelStatusChanged += PlayerManager_OnPlayerModelChanged;
    }

    private void PlayerManager_OnPlayerModelChanged(object sender, EventArgs e)
    {
        StopAllCoroutines();
        StartCoroutine(AutoShoot());
    }

    public IEnumerator AutoShoot()
    {
        while (true)
        {
            foreach (Transform shootingPoint in shootingPointList)
            {
                GameObject bullet = ObjectPooler.Instance.GetObjectFromPool(pooledBulletTag);
                if (bullet != null)
                {

                    //setup the bullet
                    bullet.transform.SetPositionAndRotation(shootingPoint.position, quaternion.Euler(0, 0, 0));
                    bullet.SetActive(true);

                    //Shoot the bullet
                    Rigidbody rb2d = bullet.GetComponent<Rigidbody>();
                    rb2d.velocity = Vector2.up * bulletSpeed;
                }
            }
            yield return new WaitForSeconds(rateTime);
        }
    }

    public void SetShootingPointList(List<Transform> shootingPointList)
    {
        this.shootingPointList = shootingPointList;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
