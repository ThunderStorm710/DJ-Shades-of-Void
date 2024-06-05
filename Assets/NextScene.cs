using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector director; // Arraste o componente PlayableDirector para este campo no Inspector

    void Start()
    {
        // Adiciona um evento para ser chamado quando a Timeline terminar
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            // Carrega a próxima cena quando a Timeline terminar
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnDestroy()
    {
        // Importante para evitar referências penduradas
        director.stopped -= OnPlayableDirectorStopped;
    }
}
