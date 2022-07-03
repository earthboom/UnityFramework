using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum EResourceFailType
{
    OK,
    Common,
    CommonLoadAsset,
    CommonLoadAssets,
    CommonInstantiateAsset,
    CommonLoadScene,

    Resource,
    Addressable,
    AssetBundle,
    AssetDatabase,
}

interface IResourceLoader
{
    void Init();
    void Destroy();

    T LoadAsset<T>(string url, string name) where T : UnityEngine.Object;
    IList<T> LoadAssets<T>(string url, System.Action<T> callback) where T : UnityEngine.Object;
    void UnloadAsset<T>(T obj) where T : UnityEngine.Object;

    GameObject InstaniateAsset(string url, string name, Vector3 pos, Quaternion rot);
    void ReleaseAsset(GameObject obj);

    void LoadScene(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed);

    void UnloadScene(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed);
}

interface IResourceLoaderAsync
{
    void InitAsync();
    void DestroyAsync();

    AsyncOperationHandle<T> LoadAssetAsync<T>(string url, string name, System.Action<T> funcSuccessded, System.Action funcFailed) where T : UnityEngine.Object;
    AsyncOperationHandle<IList<T>> LoadAssetAsync<T>(string url, System.Action<T> callback, System.Action<IList<T>> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object;
    void UnloadAssetAsync<T>(T obj) where T : UnityEngine.Object;

    AsyncOperationHandle<SceneInstance> LoadSceneAsync(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed);

    void UnloadSceneAsync(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed);
}

interface IResourceLoaderCoroutine
{
    void initCoroutine();
    void DestroyCoroutine();

    IEnumerator CoLoadAsset<T>(string url, string name, System.Action<T> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object;
    IEnumerator ColoadAssets<T>(string url, System.Action<T> callback, System.Action<IList<T>> funcSucceeded, System.Action funcFailed) where T : UnityEngine.Object;
    void CoUnloadAsset<T>(T obj) where T : UnityEngine.Object;

    IEnumerator CoInstantiateAsset(string url, string name, Vector3 pos, Quaternion rot, System.Action<GameObject> funcSucceeded, System.Action funcFailed);
    void CoReleaseAsset(GameObject obj);

    IEnumerator CoLoadScene(string url, string sceneName, LoadSceneMode loadMode, System.Action<Scene, LoadSceneMode> funcSucceeded, System.Action funcFailed);

    IEnumerator CoUnloadScene(SceneInstance scene, System.Action<Scene> funcSucceeded, System.Action funcFailed);
}