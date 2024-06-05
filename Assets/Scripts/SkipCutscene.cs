using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    private float spacePressTime = 0f;
    private bool isSpacePressed = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true;
            spacePressTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isSpacePressed = false;
        }

        if (isSpacePressed && Time.time - spacePressTime >= 2f)
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
