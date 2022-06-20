using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputBase : MonoBehaviour
{
    #region [ GameMode Accessor ]
    [HideInInspector]
    public GameModeBase mGameMode;

    public TGameMode GetGameMode<TGameMode>() where TGameMode : GameModeBase
    {
        return mGameMode as TGameMode;
    }
    #endregion
}
