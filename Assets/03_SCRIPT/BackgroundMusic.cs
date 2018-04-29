using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour {
    private AudioSource _audioSource;
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
