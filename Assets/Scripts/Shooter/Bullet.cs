using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";
    [SerializeField] private BulletSO bulletSO;

    //set active true all children if have
    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        StartCoroutine(ToggleObject());
    }

    //private void Update()
    //{
    //    StartCoroutine(ToggleObject());
    //}

    IEnumerator ToggleObject()
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(bulletSO.ExistTime);
            Hide();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Bullet>() && bulletSO.Tag.ToString().CompareTo(other.gameObject.tag) != 0 && !other.CompareTag("Item"))
        {

            if (other.gameObject.CompareTag(PLAYER_TAG))
            {
                IHealth otherHealth = other.GetComponentInParent<IHealth>();

                otherHealth?.TakeDamage(bulletSO.Damage);
            }
            if (other.gameObject.CompareTag(ENEMY_TAG))
            {
                IHealth otherHealth = other.GetComponent<IHealth>();

                otherHealth?.TakeDamage(bulletSO.Damage);
            }
            Hide();
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
