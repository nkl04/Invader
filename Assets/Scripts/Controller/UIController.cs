using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIController : Singleton<UIController>
{
    private Stack<UIPage> uipageStack = new Stack<UIPage>();
    private UIPage initialPage;

    //push a new page and show the page
    public void PushAndShow(UIPage page)
    {
        if (uipageStack.Count > 0)
        {
            uipageStack.Peek().Hide();
        }
        page.Show();
        uipageStack.Push(page);
        GameController.Instance.UpdateGameState(page.gameState);
    }

    //go back to the previous page
    public void PopAndReturn()
    {
        if (uipageStack.Count == 1)
        {
            Debug.LogError("There is no previous page to return to.");
            return;
        }
        uipageStack.Pop().Hide();
        UIPage page = uipageStack.Peek();
        page.Show();
        GameController.Instance.UpdateGameState(page.gameState);

    }

    //push a new page and show the page but not hide the previous page
    public void PushAndShowWithoutHidingPrevious(UIPage page)
    {
        page.Show();
        uipageStack.Push(page);
        GameController.Instance.UpdateGameState(page.gameState);

    }

    //clear the stack and set a new initial page
    public void ClearStackAndSetInitialPage(UIPage page)
    {
        //hide all pages
        foreach (var item in uipageStack)
        {
            item.Hide();
        }
        uipageStack.Clear();
        PushAndShow(page);
        initialPage = page;
    }

    // check if the page is on the top of the stack
    public bool IsOnTop(UIPage page)
    {
        return uipageStack.Peek() == page;
    }

    //set and get the initial page
    public UIPage InitialPage
    {
        get { return initialPage; }
        set { initialPage = value; }
    }

}
