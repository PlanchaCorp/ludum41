using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public Sprite mutedSprite;
    public Sprite unmutedSprite;
    private AudioSource _audioSource;
    private bool firstStart = true;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (SceneManager.GetActiveScene().name == "Startup")
        {
            _audioSource = GetComponent<AudioSource>();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public bool GetMusicState()
    {
        return _audioSource.isPlaying;
    }

    public Sprite GetCurrentMuteSprite()
    {
        if (_audioSource.isPlaying || firstStart)
        {
            firstStart = false;
            return unmutedSprite;
        } else
        {
            return mutedSprite;
        }
    }

    public bool ToggleMusic()
    {
        if (!_audioSource.isPlaying)
        {
            PlayMusic();
        } else
        {
            StopMusic();
        }
        return _audioSource.isPlaying;
    }
}
