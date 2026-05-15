using UnityEngine;

public class AudioManager : MonoBehaviour, IManager
{
    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void PlayerSFX(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}
