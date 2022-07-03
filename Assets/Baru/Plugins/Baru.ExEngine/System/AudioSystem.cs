using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Extension;

public class AudioValue
{
    public const string AudioName_UI_ButtonClick = "UI_ButtonClick";
}

[RequireComponent(typeof(AudioListener))]
public class AudioSystem : BaseSystem<AudioSystem>
{
    AudioBGMHandler audioBGMHandler;

    protected override void Awake()
    {

    }

    protected override void Start()
    {
        
    }

    protected override void OnDestroy()
    {
        
    }
}
