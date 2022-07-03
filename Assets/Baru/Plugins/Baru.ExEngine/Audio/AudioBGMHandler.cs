using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class AudioBGMHandler : AudioBaseHandler
{
    protected override void Awake()
    {
        base.Awake();

        SetProperty(true);
    }

    public override void Play(AudioClip audioClip)
    {
        if (IsPlaying())
        {
            mAudioSource.DOFade(0.0f, 2.0f).OnComplete(() =>
            {
                Stop();
                Play(audioClip);
            });
        }
        else
        {
            base.Play(audioClip);
            mAudioSource.DOFade(0.4f, 2.0f);
        }
    }
}
