using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioUIHandler : AudioBaseHandler
{
    public List<AudioClip> mAudioClipList = new List<AudioClip>();
    public Dictionary<string, AudioClip> mAudioClips = new Dictionary<string, AudioClip>();

    protected override void Awake()
    {
        base.Awake();

        SetProperty(false);
    }

    public override void Load()
    {
        base.Load();

        foreach (AudioClip clip in mAudioClipList)
            mAudioClips.Add(clip.name, clip);


    }

    public bool Play(string name)
    {
        AudioClip audioClip;
        if (false == mAudioClips.TryGetValue(name, out audioClip))
            return false;

        PlayOneShot(audioClip);
        return true;
    }
}
