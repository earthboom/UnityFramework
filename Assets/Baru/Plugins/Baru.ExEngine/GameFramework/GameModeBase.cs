using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Extension;

public class GameModeBase : MonoBehaviour
{
    #region [ Framework Accessors ] 

    public GameStateBase mGameState;
    public PlayerControllerBase mPlayerController;

    public T GetGameState<T>() where T : GameStateBase
    {
        return mGameState as T;
    }

    public T GetPlayerController<T>() where T : PlayerControllerBase
    {
        return mPlayerController as T;
    }

    public T GetGameHUD<T>() where T : GameHUDBase
    {
        return mPlayerController.GetGameHUD<T>();
    }

    public T GetGameInput<T>() where T : GameInputBase
    {
        return mPlayerController.GetGameInput<T>();
    }

    public T GetPlayerCameraManager<T>() where T : PlayerCameraManagerBase
    {
        return mPlayerController.GetPlayerCameraManager<T>();
    }

    protected void Add<TGameState, TPlayerController>()
        where TGameState : GameStateBase
        where TPlayerController : PlayerControllerBase
    {
        mGameState = gameObject.GetComponentForce<TGameState>();
        if (mGameState)
            mGameState.mGameMode = this;

        mPlayerController = gameObject.GetComponentForce<TPlayerController>();
        if (mPlayerController)
        {
            mPlayerController.mGameMode = this;
            if (mPlayerController.mGameHUD)
                mPlayerController.mGameHUD.mGameMode = this;
            if (mPlayerController.mGameInput)
                mPlayerController.mGameInput.mGameMode = this;
            if (mPlayerController.mPlayerCameraManager)
                mPlayerController.mPlayerCameraManager.mGameMode = this;
        }
    }

    protected void Set<TGameState, TPlayerController>()
        where TGameState : GameStateBase
        where TPlayerController : PlayerControllerBase
    {
        mGameState = gameObject.GetComponent<TGameState>();
        if (mGameState)
            mGameState.mGameMode = this;

        mPlayerController = gameObject.GetComponent<TPlayerController>();
        if (mPlayerController)
        {
            mPlayerController.mGameMode = this;
            if (mPlayerController.mGameHUD)
                mPlayerController.mGameHUD.mGameMode = this;
            if (mPlayerController.mGameInput)
                mPlayerController.mGameInput.mGameMode = this;
            if (mPlayerController.mPlayerCameraManager)
                mPlayerController.mPlayerCameraManager.mGameMode = this;
        }
    }
    #endregion

    public ScreenOrientation mScreenOrientation;
    public Vector2Int mResolutionUI;
    public AudioClip mBGM;
    bool mIsFocusGame = true;
    bool mIsPauseGame = false;

    protected virtual void Awake()
    {
        Add<GameStateBase, PlayerControllerBase>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;

        System.GC.Collect();
    }

    private void OnApplicationFocus(bool isFocus)
    {
        if (isFocus)
        {
            mIsFocusGame = true;
            FocusGame();
        }
        else
        {
            if (mIsFocusGame)
            {
                OutFocusGame();
                mIsFocusGame = false;
            }
        }
    }

    protected virtual void FocusGame()
    {
        LogSystem.Log($"{GetType().Name}::FocusGame", "GameModeBase");
    }

    protected virtual void OutFocusGame()
    {
        LogSystem.Log($"{GetType().Name}::OutFocusGame", "GameModeBase");
    }

    protected virtual void PauseGame()
    {
        LogSystem.Log($"{GetType().Name}::PauseGame", "GameModeBase");
    }

    protected virtual void ResumeGame()
    {
        LogSystem.Log($"{GetType().Name}::ResumeGame", "GameModeBase");
    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LogSystem.Log($"{GetType().Name}::OnSceneLoaded", $"{scene.name}");

        if(LoadSceneMode.Single == mode)
        {
            //mPlayerController.mGameHUD.mUIRoot.SceneLoaded();
        }
        else if(LoadSceneMode.Additive == mode)
        {
            SceneManager.SetActiveScene(scene);

            GameObject[] gos = scene.GetRootGameObjects();
            foreach(GameObject go in gos)
            {
                
            }
        }
    }

    protected virtual void OnSceneUnLoaded(Scene scene)
    {
        LogSystem.Log($"{GetType().Name}::OnSceneUnLoaded", $"{scene.name}");
    }

    public virtual IEnumerable CoSceneLoadedComplete(string sceneName)
    {
        LogSystem.Log($"{GetType().Name}::CoSceneLoadedComplete", sceneName);

        yield break;
    }
}
