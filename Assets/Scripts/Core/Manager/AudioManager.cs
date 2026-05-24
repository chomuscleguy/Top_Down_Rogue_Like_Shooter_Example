using UnityEngine;

public class AudioManager : MonoBehaviour, IManager
{
    public void Init()
    {
    }

    public void PlayerSFX(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}
