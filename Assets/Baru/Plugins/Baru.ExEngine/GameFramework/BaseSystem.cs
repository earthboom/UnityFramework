using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSystem<T> : MonoBehaviour where T : class
{
    public static T Instance;

    protected virtual void Awake()
    {
        LogSystem.Log($"{GetType().Name}::Awake", "BaseSystem");

        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void Start()
    {
        LogSystem.Log($"{GetType().Name}::Start", "BaseSystem");
    }

    protected virtual void OnDestroy()
    {
        
    }

    public virtual void Clear()
    {

    }
}
