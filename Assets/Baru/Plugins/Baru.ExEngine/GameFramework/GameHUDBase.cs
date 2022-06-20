using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHUDBase : MonoBehaviour
{
    #region [ GameMode Accessor ]
    [HideInInspector]
    public GameModeBase mGameMode;

    public TGameMode GetGameMode<TGameMode>() where TGameMode : GameModeBase
    {
        return mGameMode as TGameMode;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
