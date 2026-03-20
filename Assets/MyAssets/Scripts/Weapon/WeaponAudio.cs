using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponAudio : MonoBehaviour
{

    AudioSource audioSource => GetComponent<AudioSource>();
    AudioClip metalClick;
    AudioClip fire;
    AudioClip tossThing;
    AudioClip shellCasing;

    void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayMetalClick()
    {
        PlayAudio(metalClick);
    }
    public void PlayFire()
    {
        PlayAudio(fire);
    }
    public void PlayTossThing()
    {
        PlayAudio(tossThing);
    }
    public void PlayShellCasing()
    {
        PlayAudio(shellCasing);
    }
}
