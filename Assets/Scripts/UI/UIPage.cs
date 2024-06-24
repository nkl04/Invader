using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPage : MonoBehaviour
{
    public GameState gameState;
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
