using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase : MonoBehaviour
{
    #region [ GameMode Accessor ]
    [HideInInspector]
    public GameModeBase mGameMode;

    public TGameMode GetGameMode<TGameMode>() where TGameMode : GameModeBase
    {
        return mGameMode as TGameMode;
    }
    #endregion

    protected virtual void Awake()
    {

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
