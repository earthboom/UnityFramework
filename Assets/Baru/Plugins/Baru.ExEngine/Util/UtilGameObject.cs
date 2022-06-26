using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

using Extension;

public class UtilGameObject
{
    public static GameObject FindGameObject(string name, bool donDestroy = false)
    {
        GameObject goFind = GameObject.Find(name);
        if (null == goFind)
            goFind = new GameObject(name);

        if (donDestroy)
            goFind.GetComponentForce<DontDestroyObject>();

        return goFind;
    }

    public static T GetChild<T>(GameObject gameObject) where T : Object
    {
        return gameObject.GetComponentInChildren<T>();
    }

    public static T[] GetChildren<T>(GameObject gameObject) where T : Object
    {
        return gameObject.GetComponentsInChildren<T>();
    }

    public static T GetChild<T>(GameObject gameObject, string name) where T : Object
    {
        T[] array = gameObject.GetComponentsInChildren<T>();
        foreach(T go in array)
        {
            if (go.name == name)
                return go;
        }

        return null;
    }

    public static T GetParent<T>(GameObject gameObject) where T : Object
    {
        return gameObject.GetComponentInParent<T>();
    }

    public static T[] GetParents<T>(GameObject gameObject) where T : Object
    {
        return gameObject.GetComponentsInParent<T>();
    }

    public static T GetParent<T>(GameObject gameObject, string name) where T : Object
    {
        T[] array = gameObject.GetComponentsInParent<T>();
        foreach(T go in array)
        {
            if (go.name == name)
                return go;
        }

        return null;
    }


    public static void SetActive(Transform transform, bool active)
    {
        if (null != transform)
            SetActive(transform.gameObject, active);
    }

    public static void SetActiveRecursive(Transform transform, bool active)
    {
        if (null != transform)
            SetActiveRecursive(transform.gameObject, active);
    }

    public static void SetActive(Component component, bool active)
    {
        if(null != component)
            SetActive(component.gameObject, active);
    }

    public static void SetActiveRecursive(Component component, bool active)
    {
        if (null != component)
            SetActiveRecursive(component.gameObject, active);
    }

    public static void SetActive(GameObject go, bool active)
    {
        if (null != go && go.activeSelf != active)
            go.SetActive(active);
    }

    public static void SetActiveRecursive(GameObject go, bool active)
    {
        if (null == go)
            return;

        Transform trans = go.transform;
        for(int i=0; i<trans.childCount; ++i)
        {
            GameObject child = trans.GetChild(i).gameObject;
            SetActive(child, active);
            SetActiveRecursive(child, active);
        }
    }

    public static void SafeDestroyGameObject(ref GameObject gameObject)
    {
        if (null == gameObject)
            return;

        GameObject.Destroy(gameObject);
        gameObject = null;
    }

    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst)
            dst = destination.AddComponent(type) as T;

        var fields = type.GetFields();
        foreach(var field in fields)
        {
            if (field.IsStatic)
                continue;
            field.SetValue(dst, field.GetValue(original));
        }

        var props = type.GetProperties();
        foreach(var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name")
                continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }

        return dst as T;
    }

    public static void SetDestroyComponent<T>(ref T comp) where T : Component
    {
        if (null == comp)
            return;

        MonoBehaviour.Destroy(comp);
        comp = null;
    }
}
