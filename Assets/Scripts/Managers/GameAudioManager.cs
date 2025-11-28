using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance { get; private set; }

    [Header("Fuentes de audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Música por defecto (opcional)")]
    public AudioClip defaultMusic;

    [Header("SFX generales")]
    public AudioClip jumpSFX;
    public AudioClip doorOpenSFX;
    public AudioClip cronoStepSFX;
    public AudioClip platformOnSFX;
    public AudioClip platformOffSFX;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Si quieres que MainMenu tenga música desde el inicio:
        if (musicSource != null && defaultMusic != null)
        {
            PlayMusic(defaultMusic);
        }
    }

    // ========== MÚSICA ==========
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource == null || clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying)
            return; // ya está sonando esta pista

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // ========== SFX ==========
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayJump()        => PlaySFX(jumpSFX);
    public void PlayDoorOpen()    => PlaySFX(doorOpenSFX);
    public void PlayCronoStep()   => PlaySFX(cronoStepSFX);
    public void PlayPlatformOn()  => PlaySFX(platformOnSFX);
    public void PlayPlatformOff() => PlaySFX(platformOffSFX);
}
