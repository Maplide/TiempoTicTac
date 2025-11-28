using UnityEngine;

public class SceneMusicSetter : MonoBehaviour
{
    public AudioClip sceneMusic;
    public bool loop = true;

    void Start()
    {
        if (GameAudioManager.Instance != null && sceneMusic != null)
        {
            GameAudioManager.Instance.PlayMusic(sceneMusic, loop);
        }
    }
}
