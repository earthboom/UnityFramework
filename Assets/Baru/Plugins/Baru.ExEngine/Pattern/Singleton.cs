using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _Instance;

    public static T Instance
    {
        get
        {
            if(null == _Instance)
            {
                GameObject obj = GameObject.Find(typeof(T).Name);
                if(null == obj)
                    obj = new GameObject(typeof(T).Name);

                _Instance = obj.GetComponent<T>();
            }

            return _Instance;
        }
    }

    private static bool applicationIsQuitting = false;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
