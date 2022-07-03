using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

public class ResourceLoaderResources : MonoBehaviour, IResourceLoader
{
    Dictionary<string, Object> mResourcesLoadeds = new Dictionary<string, Object>();


    System.Action<Scene, LoadSceneMode> sceneLoaded;
    System.Action<Scene> sceneUnloaded;

    public void Clear()
    {
        mResourcesLoadeds.Clear();

        Resources.UnloadUnusedAssets();
    }

    public void Init()
    {

    }

    public void Destroy()
    {
        Clear();
    }

    public T LoadAsset<T>(string url, string name) where T : UnityEngine.Object
    {
        Object findObject;
        if(false == mResourcesLoadeds.TryGetValue(url, out findObject))
        {
            findObject = Resources.Load(url);
            if (null != findObject)
                mResourcesLoadeds.Add(url, findObject);
        }

        return findObject as T;
    }

    public IList<T> LoadAssets<T>(string url, System.Action<T> callback) where T : UnityEngine.Object
    {
        T[] loads = Resources.LoadAll(url) as T[];
        if (null != loads)
            return loads.ToList();
        else
            return null;
    }

    public void UnloadAsset<T>(T obj) where T : UnityEngine.Object
    {
        foreach(var itr in mResourcesLoadeds)
        {
            if(itr.Value.name == obj.name)
            {
                mResourcesLoadeds.Remove(itr.Key);
                break;
            }
        }

        Resources.UnloadAsset(obj as Object);
    }

    public GameObject InstaniateAsset(string url, string name, Vector3 pos, Quaternion rot)
    {
        var go = LoadAsset<GameObject>(url, name);
        if (null != go)
            return GameObject.Instantiate(go, pos, rot);
        else
            return null;
    }

    public void ReleaseAsset(GameObject obj)
    {
        Resources.UnloadAsset(obj);
    }

    public void LoadScene(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed)
    {
        sceneLoaded = funcSucceeded;
        SceneManager.sceneLoaded += HanderSceneLoaded;

        SceneManager.LoadScene(sceneName, loadMode);

        sceneLoaded = null;
        SceneManager.sceneLoaded -= HanderSceneLoaded;
    }

    public void UnloadScene(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed)
    {
        sceneUnloaded = funcSucceeded;
        SceneManager.sceneUnloaded += HanderSceneUnloaded;

        SceneManager.UnloadScene(scene.Scene);

        sceneUnloaded = null;
        SceneManager.sceneUnloaded -= HanderSceneUnloaded;
    }

    void HanderSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        sceneLoaded?.Invoke(scene, loadSceneMode);
    }

    void HanderSceneUnloaded(Scene scene)
    {
        sceneUnloaded?.Invoke(scene);
    }

    // Async
    public ResourceRequest LoadAssetAsync<T>(string url, string name, System.Action<T> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
        Object findObject;
        if(true == mResourcesLoadeds.TryGetValue(url, out findObject))
        {
            funcSucceeded?.Invoke(findObject as T);
            return null;
        }

        var handle = Resources.LoadAsync<T>(url);
        handle.completed += (AsyncOperation oper) =>
        {
            if(null != handle.asset)
            {
                mResourcesLoadeds.Add(url, handle.asset);
                funcSucceeded?.Invoke(handle.asset as T);
            }
        };

        return handle;
    }

    public ResourceRequest InstantiateAssetAsync(string url, string name, Vector3 pos, Quaternion rot, System.Action<GameObject> funcSucceeded, System.Action funcFailed)
    {
        var handle = LoadAssetAsync(url, name, (GameObject go) =>
        {
            if(null != go)
            {
                var goInst = GameObject.Instantiate(go, pos, rot);
                funcSucceeded?.Invoke(goInst);
            }
        }, funcFailed);

        return handle;
    }
}
