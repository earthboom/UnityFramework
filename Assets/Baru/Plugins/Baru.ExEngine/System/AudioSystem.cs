using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Extension;

public class AudioValue
{
    public const string AudioName_UI_ButtonClick = "UI_ButtonClick";
    public const string AudioName_UI_ButtonClick_Disable = "UI_ButtonClick_Disable";
    public const string AudioName_UI_WndOpen = "UI_WndOpen";
    public const string AudioName_UI_WndClose = "UI_WndClose";
    public const string AudioName_UI_Message = "UI_Message";
    public const string AudioName_UI_Notice = "UI_Notice";
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
