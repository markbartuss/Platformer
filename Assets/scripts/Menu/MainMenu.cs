using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    public AudioClip Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void button()//plays a button sound effect 
    {
        audioManager.PlaySFX(Button);
    }
    public void Play()
    {
        //changes to the game scene when the method play gets called
        SceneChange();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }
    public void SceneChange()//goes to the next scene in the buildIndex
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void quit()
    {
        Application.Quit();
    }

}