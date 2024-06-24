using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public IItem IItem { get => item; set => item = value; }

    [SerializeField] private IItem item;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;

    private void OnEnable()
    {
        Instantiate();
        StartCoroutine(ToggleObject());
        StartCoroutine(MoveDown());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveDown()
    {
        while (true)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ToggleObject()
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(item.existTime);
            Hide();
        }
    }

    private void Instantiate()
    {
        GameObject dropITem = Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
        dropITem.transform.SetParent(transform);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
