using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;

//https://github.com/Unity-Technologies/Addressables-Sample
//https://www.programmersought.com/article/50262625456/
public class ResourceLoaderAddressable : MonoBehaviour, IResourceLoader, IResourceLoaderAsync, IResourceLoaderCoroutine
{
    public static bool gInit = false;

    // Remote
    public IEnumerator CoDownloadAsync(string key, System.Action<AsyncOperationHandle> funcReady, System.Action funcResult)
    {
        long downloadSize = 0;
        yield return StartCoroutine(CoGetDownloadSizeAsync(key, (long size) =>
        {
            downloadSize = size;
        }));

        if (downloadSize <= 0)
            yield break;

        yield return StartCoroutine(CoDownloadDependenciesAsync(key, funcReady, funcResult));
    }
    
    public IEnumerator CoGetDownloadSizeAsync(string key, System.Action<long> funcResult)
    {
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownloadSize;

        funcResult?.Invoke(getDownloadSize.Result);
    }

    public IEnumerator CoGetDownloadSizeAsync(List<string> keys, System.Action<long> funcResult)
    {
        long downloadSize = 0;
        foreach(string key in keys)
        {
            AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
            yield return getDownloadSize;

            downloadSize += getDownloadSize.Result;
        }

        funcResult?.Invoke(downloadSize);
    }

    // PercentComplete 사용해 다운로드 상황 체크 가능
    public IEnumerator CoDownloadDependenciesAsync(string key, System.Action<AsyncOperationHandle> funcReady, System.Action funcResult)
    {
        AsyncOperationHandle downHandle = Addressables.DownloadDependenciesAsync(key);
        funcReady?.Invoke(downHandle);
        yield return downHandle;
        funcResult?.Invoke();
    }

    public IEnumerator CoDownloadDependenciesAsync(List<string> keys, System.Action<string, AsyncOperationHandle> funcReady, System.Action funcResult)
    {
        foreach(string key in keys)
        {
            AsyncOperationHandle downHandle = Addressables.DownloadDependenciesAsync(key);
            funcReady?.Invoke(key, downHandle);
            yield return downHandle;
        }

        funcResult?.Invoke();
    }

    public IEnumerator CoClearDependencyCacheAsync(string key, System.Action funcResult)
    {
        AsyncOperationHandle<bool> clearHandle = Addressables.ClearDependencyCacheAsync(key, true);
        yield return clearHandle;
        funcResult?.Invoke();
    }

    public void LoadLocationsAsync<T>(string labelName, System.Action<T> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
        //AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(labelName);
        //yield return handle;

        Addressables.LoadResourceLocationsAsync(labelName).Completed += (AsyncOperationHandle<IList<IResourceLocation>> handle) =>
        {
            if(AsyncOperationStatus.Succeeded == handle.Status)
            {
                IList<IResourceLocation> resourceLocations = handle.Result;

                foreach(IResourceLocation resourceLocation in resourceLocations)
                {
                    Addressables.LoadAssetAsync<T>(resourceLocation).Completed += (instanceHandle) =>
                    {
                        funcSucceeded.Invoke(instanceHandle.Result as T);
                    };
                }
            }
            else
            {
                funcFailed?.Invoke();
            }

            //Addressables.Release(handle);
        };
    }

    public void InstantiateLocationsAsync<T>(string labelName, System.Action<T> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
    }

    #region IResourceLoader
    public void Init()
    {
        Addressables.InitializeAsync().Completed += (AsyncOperationHandle<IResourceLocator> handle) =>
        {
            gInit = true;
        };
    }

    public void Destroy()
    {

    }

    public T LoadAsset<T>(string url, string name) where T : UnityEngine.Object
    {
        return Addressables.LoadAssetAsync<T>(url).WaitForCompletion();
    }

    public IList<T> LoadAssets<T>(string url, System.Action<T> callback) where T : UnityEngine.Object
    {
        return Addressables.LoadAssetsAsync<T>(url, callback).WaitForCompletion();
    }

    public void UnloadAsset<T>(T obj) where T : UnityEngine.Object
    {
        Addressables.Release<T>(obj);
    }

    public GameObject InstantiateAsset(string url, string name, Vector3 pos, Quaternion rot)
    {
        return Addressables.InstantiateAsync(url, pos, rot).WaitForCompletion();
    }

    public void ReleaseAsset(GameObject obj)
    {
        Addressables.ReleaseInstance(obj);
    }

    public void LoadScene(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed)
    {
        Addressables.LoadSceneAsync(url, loadMode).Completed += (AsyncOperationHandle<SceneInstance> handle) =>
        {
            if (AsyncOperationStatus.Succeeded == handle.Status)
                funcSucceeded.Invoke(handle.Result.Scene, loadMode);
            else
                funcFailed.Invoke();

            //Addressables.Release(handle);
        };
    }

    public void UnloadScene(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed)
    {
        Addressables.UnloadSceneAsync(scene).Completed += (AsyncOperationHandle<SceneInstance> handle) =>
        {
            if (AsyncOperationStatus.Succeeded == handle.Status)
                funcSucceeded.Invoke(handle.Result.Scene);
            else
                funcFailed.Invoke();

            //Addressables.Release(handle);
        };
    }
    #endregion IResourceLoader

    #region IResourceLoaderAsync
    public void InitAsync()
    {
    }

    public void DestroyAsync()
    {
    }

    public AsyncOperationHandle<T> LoadAssetAsync<T>(string url, string name, System.Action<T> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(url);

        return handle;
    }

    public AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(string url, System.Action<T> callback, System.Action<IList<T>> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(url, callback);

        return handle;
    }

    public void UnloadAssetAsync<T>(T obj) where T : UnityEngine.Object
    {
        Addressables.Release(obj);
    }

    public AsyncOperationHandle<SceneInstance> LoadSceneAsync(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(url, loadMode);

        return handle;
    }

    public async void UnloadSceneAsync(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(scene);

        await handle.Task;
    }
    #endregion IResourceLoaderAsync

    #region IResourceLoaderCoroutine
    public void InitCoroutine()
    {
    }
    public void DestroyCoroutine()
    {
    }

    public IEnumerator CoLoadAsset<T>(string url, string name, System.Action<T> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(url);

        yield return handle;
    }

    public IEnumerator CoLoadAssets<T>(string url, System.Action<T> callback, System.Action<IList<T>> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(url, callback);

        yield return handle;
    }

    public void CoUnloadAsset<T>(T obj) where T : UnityEngine.Object
    {
        Addressables.Release(obj);
    }

    public IEnumerator CoInstantiateAsset(string url, string name, Vector3 pos, Quaternion rot, System.Action<GameObject> funcSucceeded, System.Action funcFailed)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(url, pos, rot);

        yield return handle;
    }

    public void CoReleaseAsset(GameObject obj)
    {
        Addressables.ReleaseInstance(obj);
    }

    public IEnumerator CoLoadScene(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(url, loadMode);

        yield return handle;
    }

    public IEnumerator CoUnloadScene(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(scene);

        yield return handle;
    }
    #endregion IResourceLoaderCoroutine
}
