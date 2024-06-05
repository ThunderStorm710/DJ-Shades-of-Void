using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LevelEndMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;

    }
    public void MainMenu(){
        SceneManager.LoadScene(0);
    }
    public void PreviousLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RepeatLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena atual
        Time.timeScale = 1f; // Certifique-se de que o tempo está normal

    }
}
