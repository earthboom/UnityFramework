using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseInstance<T> : Singleton<T> where T : MonoBehaviour
{
    protected bool bFocusEvent = false;
    protected bool bFocus;

    public BaseInstance()
    {
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
