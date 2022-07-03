using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UILyerType
{
    Empty,
    HUD,
    Scene,
    Popup,
    Notice,
    Tutorial,
    Loading,
    Cheat,
    Max
}

public enum UIVisibleState
{
    Appearing,
    Appeared,
    Disappearing,
    Disappeared
}

public enum UIVisibleTweenType 
{
    None,
    Fade,
    MoveLeftToCenter,
    MoveRightToCenter,
    MoveTopToCenter,
    MoveBottomToCneter,
    ScaleXY,
    ScaleX,
    ScaleY,
}