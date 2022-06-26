using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

using UniLinq;

[RequireComponent(typeof(Canvas))]
public class UIRootBase : MonoBehaviour
{
    public const string gUIPath = "Prefabs/UI";
    public static bool IsUITool = false;

    public Camera mCamera;
    public Canvas mCanvas;
    public CanvasScaler mCanvasScaler;
    public GameObject mRawImageBG;

    RectTransform mRectRansform;
    public RectTransform mCanvasRectTransform;

    //Dictionary<Type, UIWndBase> mStaticUIWnds = new Dictionary<Type, UIWndBase>();

    protected virtual void Awake()
    {
        LogSystem.MessageClassFuncName();

    }

    protected virtual void Start()
    {
        LogSystem.MessageClassFuncName();
    }


    protected virtual void Update()
    {
        
    }

    protected virtual void OnDestroy()
    {

    }
}
