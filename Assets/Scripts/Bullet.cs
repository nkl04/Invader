using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;

    //set active true all children if have
    private void OnEnable() {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Update() {
        StartCoroutine(ToggleObject());
    }

    IEnumerator ToggleObject()
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(bulletSO.ExistTime);
            Hide();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Bullet>() && bulletSO.Tag.ToString().CompareTo(other.gameObject.tag) != 0)
        {
            if (other.gameObject.TryGetComponent<IHealth>(out var otherHeath))
            {
                otherHeath.TakeDamage(bulletSO.Damage);
                Debug.Log("Hit" + other.gameObject.name);
            }
            Hide();
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
