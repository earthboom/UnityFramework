using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectHandler : AudioBaseHandler
{
    protected override void Awake()
    {
        base.Awake();

        SetProperty(false);
    }
}
