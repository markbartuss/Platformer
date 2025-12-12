using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{


    //Audio sources
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource playSource;



    //Audio thats being used will be inputed in to these
    public AudioClip menu;
    public AudioClip game;
    public AudioClip Button;
    public AudioClip jump;
    public AudioClip dead;
    public static AudioManager instance;

    bool musicPlaying;


    //This checks to see if there is multiple of the audio manager
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        //plays the menu music
        musicSource.clip = menu;

        musicSource.Play();
    }


    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        //This plays the audio for the menu
        if (currentScene.name == "Menu" && musicPlaying == false)
        {
            musicSource.clip = menu;
            musicSource.Play();
            musicPlaying = true;
        }

        //This plays audio for first scene/game
        if (currentScene.name == "Game" && musicPlaying)
        {
            Debug.Log("sceneChange");
            musicSource.Stop();
            musicSource.clip = game;
            musicSource.Play();
            musicPlaying = false;

        }
    }


    //This can be used to play the audio from outside the script
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void Playplay(AudioClip clip)
    {
        playSource.PlayOneShot(clip);
    }
}