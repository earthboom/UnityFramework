using UnityEngine;

namespace Extension
{
    public static class ExtensionGameObject
    {
        public static T GetComponentForce<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (null == component)
                component = gameObject.AddComponent<T>();
            return component;
        }
    }
}

