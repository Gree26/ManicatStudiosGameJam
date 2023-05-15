using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private Page initialPage;
    [SerializeField]
    private GameObject FirstFocusItem;

    private Canvas RootCanvas;

    private Stack<Page> PageStack = new Stack<Page>();

    private void Awake()
    {
        RootCanvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (initialPage != null)
        {
            PushPage(initialPage);
        }
    }

    public bool IsStackEmpty()
    {
        if(PageStack.Count==0 || PageStack == null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Pop current page.
    /// </summary>
    private void OnCancel()
    {
        if (RootCanvas.enabled && RootCanvas.gameObject.activeInHierarchy)
        {
            if (PageStack.Count != 0)
            {
                PopPage();
            }
        }
    }

    /// <summary>
    /// Is this page in the stack of pages.
    /// </summary>
    /// <param name="page">The page to search for.</param>
    /// <returns>Is it in the stack?</returns>
    public bool IsPageInStack(Page page)
    {
        return PageStack.Contains(page);
    }

    /// <summary>
    /// Is this page on top of the stack.
    /// </summary>
    /// <param name="page">The page to compare to the top of thes stack.</param>
    /// <returns>Is it on top of the stack</returns>
    public bool IsPageOnTopOfStack(Page page)
    {
        return PageStack.Count > 0 && page == PageStack.Peek();
    }

    /// <summary>
    /// Add page to top of stack.
    /// </summary>
    /// <param name="page">Page to add.</param>
    public void PushPage(Page page)
    {
        page.Enter(true);

        if (PageStack.Count > 0)
        {
            Page currentPage = PageStack.Peek();

            if (currentPage.ExitOnNewPagePush)
            {
                currentPage.Exit(false);
            }
        }

        PageStack.Push(page);
    }

    /// <summary>
    /// Pops the page at the top of the stack.
    /// </summary>
    public void PopPage()
    {
        if (PageStack.Count > 1)
        {
            Page page = PageStack.Pop();
            page.Exit(true);

            Page newCurrentPage = PageStack.Peek();
            if (newCurrentPage.ExitOnNewPagePush)
            {
                newCurrentPage.Enter(false);
            }
        }
        else if (PageStack.Count == 1)
        {
            Page page = PageStack.Pop();
            page.Exit(true);
        }
        else
        {
            Debug.LogWarning("Trying to pop a page but (" + PageStack.Count + ") page(s) remains in the stack");
        }
    }

    /// <summary>
    /// Clear the page stack.
    /// </summary>
    public void PopAllPages()
    {
        for (int i = 1; i < PageStack.Count; i++)
        {
            PopPage();
        }
    }
}
