using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Extension;

[RequireComponent(typeof(AudioSource))]
public class AudioBaseHandler : MonoBehaviour
{
    [SerializeField]
    protected AudioSource mAudioSource;

    protected virtual void Awake()
    {
        mAudioSource = gameObject.GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        
    }

    public virtual void Load()
    {

    }

    public virtual void Play()
    {
        if (IsPlaying(mAudioSource.clip))
            mAudioSource.Play();
    }

    public virtual void Play(AudioClip audioClip)
    {
        if (null == audioClip)
            return;

        if (IsPlaying(audioClip))
        {
            mAudioSource.clip = audioClip;
            mAudioSource.Play();
        }
    }

    public virtual void PlayDelay(float delay)
    {
        mAudioSource.PlayDelayed(delay);
    }

    public virtual void PlayDelay(AudioClip audioClip, float delay)
    {
        if (null == audioClip)
            return;

        mAudioSource.clip = audioClip;
        mAudioSource.PlayDelayed(delay);
    }

    public virtual void PlayOneShot(AudioClip audioClip)
    {
        mAudioSource.PlayOneShot(audioClip);
    }

    public virtual void PlayOneShot(AudioClip audioClip, float volumeScale = 1.0f)
    {
        mAudioSource.PlayOneShot(audioClip, volumeScale);
    }

    public virtual void Stop()
    {
        mAudioSource.Stop();
    }

    public void SetProperty(bool loop, float volume = -1.0f)
    {
        mAudioSource.loop = loop;
        if (volume >= 0.0f)
            mAudioSource.volume = volume;
    }

    public void SetMute(bool mute)
    {
        mAudioSource.mute = mute;
    }

    public bool IsPlaying()
    {
        return mAudioSource.clip && mAudioSource.isPlaying;
    }

    protected bool IsPlaying(AudioClip audioClip, bool sameClipPlay = true)
    {
        if (false == IsPlaying())
            return true;
        if (false == sameClipPlay && mAudioSource.clip == audioClip)
            return false;

        return true;
    }
}
