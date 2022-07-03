using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMgr<T> where T : class
{
    static T _Instance;

    public static T Instance { get { return _Instance; } }

    public BaseMgr()
    {
        _Instance = this as T;
    }

    public virtual void Init()
    {
        LogSystem.MessageClassFuncName();
    }
        
    public virtual void Clear()
    {
        LogSystem.MessageClassFuncName();
    }

    public virtual void OnDestroy()
    {
        LogSystem.MessageClassFuncName();
    }
}
