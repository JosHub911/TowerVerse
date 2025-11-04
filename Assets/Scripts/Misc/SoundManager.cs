using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip shootClip;
    public AudioClip placeTowerClip;
    
    public AudioClip playerDamageClip;
    public AudioClip gameOverClip;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Auto-play music when game starts
        PlayBackgroundMusic();
    }

    // SFX Methods
    public void PlayShoot() => PlaySound(shootClip);
    public void PlayPlaceTower() => PlaySound(placeTowerClip);

    public void PlayPlayerDamage() => PlaySound(playerDamageClip);
    public void PlayGameOver() => PlaySound(gameOverClip);

    private void PlaySound(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    // MUSIC METHODS
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic == null || musicSource == null) return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicSource?.Stop();
    }
}
