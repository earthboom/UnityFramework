using System;

using UnityEngine;
using UnityEngine.UI;

//using DG.Tweening;

[RequireComponent(typeof(Image))]
public partial class UIWndBase : MonoBehaviour
{
    [HideInInspector]
    public bool mInit;
    [HideInInspector]
    public bool mOpen;

    [HideInInspector]
    public RectTransform mRectTransform;
    [HideInInspector]
    public Image mBG;

    [HideInInspector]
    public Action<UIWndBase> mOpenAction = null;
    [HideInInspector]
    public Action<UIWndBase> mCloseAction = null;

    public bool bDrag = false;
    public bool mStatic = false;
    public bool mIsOpenClose = true;    //Unique Wnd (ex MessageBox)
    public bool mUseAlphaTween;
    public bool mIsFullScreen;

    UIRootBase mUIRoot;
    Rect mRestoreRect;

    UIWndBase PreTopMostUIWnd;

    // 3D Target
    Transform targetTrans;
    Camera targetCamera;

    public UIRootBase UIRoot
    {
        get { return mUIRoot; }
        set { mUIRoot = value; }
    }

    public T GetUIRoot<T>() where T : UIRootBase
    {
        return mUIRoot as T;
    }

    protected virtual void Awake()
    {
        MakeUIControl();
    }


    protected virtual void Start()
    {
        MakeUIControl();

        mInit = false;
        mOpen = false;

        if(mStatic || UIRootBase.IsUITool)
        {
            if (!IsOpen())
                Open(null);
        }
    }

    protected virtual void OnDestroy()
    {
        Clear();
    }

    public virtual bool Init(UIRootBase uiRoot, params object[] param)
    {
        mUIRoot = uiRoot;
        if (true == mInit)
            LogSystem.Log($"{GetType().Name}::Init - Error", "minit true");
        else
            mInit = true;

        SetButtonClickEvent((Button button) =>
        {

        });

        return true;
    }

    public virtual void Clear()
    {

    }

    public virtual bool Open(UIWndBase preMostUIWnd)
    {
        mOpen = true;

        UtilGameObject.SetActive(gameObject, true);
        return true;
    }

    public virtual void Close()
    {

    }

    public bool IsOpen()
    {
        return gameObject.activeSelf;
    }

    public void SetButtonClickEvent(System.Action<Button> action)
    {
        var buttons = mRectTransform.GetComponentsInChildren<Button>();
        foreach(var button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                action?.Invoke(button);
            });
        }
    }

    void MakeUIControl()
    {
        if (!mRectTransform)
        {
            mRectTransform = transform as RectTransform;
            mRestoreRect = new Rect(mRectTransform.rect);

            if (!mBG)
                mBG = gameObject.GetComponent<Image>();
        }
    }
}
