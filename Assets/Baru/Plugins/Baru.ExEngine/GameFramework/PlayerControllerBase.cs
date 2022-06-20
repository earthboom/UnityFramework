using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Extension;

[RequireComponent(typeof(GameModeBase))]
public class PlayerControllerBase : MonoBehaviour
{
    public GameHUDBase mGameHUD;
    public GameInputBase mGameInput;
    public PlayerCameraManagerBase mPlayerCameraManager;

    #region [ GameMode Accessor ]
    [HideInInspector]
    public GameModeBase mGameMode;

    public TGameMode GetGameMode<TGameMode>() where TGameMode : GameModeBase
    {
        return mGameMode as TGameMode;
    }
    #endregion

    #region [ Framework Accessors ]
    public T GetGameHUD<T>() where T : GameHUDBase
    {
        return mGameHUD as T;
    }

    public T GetGameInput<T>() where T : GameInputBase
    {
        return mGameInput as T;
    }

    public T GetPlayerCameraManager<T>() where T : PlayerCameraManagerBase
    {
        return mPlayerCameraManager as T;
    }

    protected void Add<TGameHUD, TGameInput, TPlayerCameraManager>()
        where TGameHUD : GameHUDBase
        where TGameInput : GameInputBase
        where TPlayerCameraManager : PlayerCameraManagerBase
    {
        mGameHUD = gameObject.GetComponentForce<TGameHUD>();
        if (mGameHUD)
            mGameHUD.mGameMode = mGameMode;

        mGameInput = gameObject.GetComponentForce<TGameInput>();
        if (mGameInput)
            mGameInput.mGameMode = mGameMode;

        mPlayerCameraManager = gameObject.GetComponentForce<TPlayerCameraManager>();
        if (mPlayerCameraManager)
            mPlayerCameraManager.mGameMode = mGameMode;
    }

    protected void Set<TGameHUD, TGameInput, TPlayerCameraManager>()
        where TGameHUD : GameHUDBase
        where TGameInput : GameInputBase
        where TPlayerCameraManager : PlayerCameraManagerBase
    {
        mGameHUD = gameObject.GetComponent<TGameHUD>();
        if (mGameHUD)
            mGameHUD.mGameMode = mGameMode;

        mGameInput = gameObject.GetComponent<TGameInput>();
        if (mGameInput)
            mGameInput.mGameMode = mGameMode;

        mPlayerCameraManager = gameObject.GetComponent<TPlayerCameraManager>();
        if (mPlayerCameraManager)
            mPlayerCameraManager.mGameMode = mGameMode;
    }
    #endregion

    protected virtual void Awake()
    {
        Set<GameHUDBase, GameInputBase, PlayerCameraManagerBase>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnDestroy()
    {

    }
}
