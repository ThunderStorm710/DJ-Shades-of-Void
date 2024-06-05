using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject[] worlds;
    public Vector3[] initialSpawnPoints; // Posi��es iniciais para cada mundo
    private Dictionary<int, Vector3> savedPositions = new Dictionary<int, Vector3>();
    private int currentWorld = 0;

    //public Animator animator; // Referência para o Animator
    private Animation animation;

    private void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        animation = gameObject.GetComponent<Animation>();

        // Inicializa com a posi��o de spawn inicial para cada mundo
        for (int i = 0; i < 1; i++)
        {
            savedPositions[i] = initialSpawnPoints[i];
        }

        // Certifica-se de que apenas o mundo inicial est� ativo
        foreach (var world in worlds)
        {
            world.SetActive(false);
        }
        worlds[currentWorld].SetActive(true);
        //MovePlayerToSavedPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Tentativa de mudança de mundo.");
            StartCoroutine(SwitchWorldCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Tentativa de mudança de mundo.");
            StartCoroutine(SwitchWorldBackwardCoroutine());
        }
    }

IEnumerator SwitchWorldCoroutine()
{
    int oldWorld = currentWorld;
    currentWorld = (currentWorld + 1) % worlds.Length;

    Debug.Log($"Iniciando a transição do mundo {oldWorld} para {currentWorld}.");

    //animator.SetInteger("Perspective", currentWorld);
    //animator.SetBool("IsAnimating", true);

    yield return new WaitForSeconds(0.5f); // Espera a animação completar
    //animator.SetBool("IsAnimating", false);
    if (oldWorld == 0){
    animation.Play("Trans1");
    } else if (oldWorld == 1) {
    animation.Play("Trans2");
    } else if (oldWorld == 2){
    animation.Play("Trans3");
    }

    Debug.Log("Animação completada.");

    worlds[oldWorld].SetActive(false);
    worlds[currentWorld].SetActive(true);

    Debug.Log($"Mundo {currentWorld} agora está ativo.");
}

IEnumerator SwitchWorldBackwardCoroutine()
{
    int oldWorld = currentWorld;
    if (currentWorld == 0){
        currentWorld = 2;
    } else if (currentWorld == 1){
        currentWorld = 0;
    } else if (currentWorld == 2){
        currentWorld = 1;
    } 

    Debug.Log($"Iniciando a transição do mundo {oldWorld} para {currentWorld}.");

    //animator.SetInteger("Perspective", currentWorld);
    //animator.SetBool("IsAnimating", true);

    yield return new WaitForSeconds(0.5f); // Espera a animação completar
    //animator.SetBool("IsAnimating", false);
    if (oldWorld == 0){
    animation.Play("Trans1");
    } else if (oldWorld == 1) {
    animation.Play("Trans2");
    } else if (oldWorld == 2){
    animation.Play("Trans3");
    }

    Debug.Log("Animação completada.");

    worlds[oldWorld].SetActive(false);
    worlds[currentWorld].SetActive(true);

    Debug.Log($"Mundo {currentWorld} agora está ativo.");
}
    /*void SwitchWorld()
    {
        // Salva a posi��o atual do personagem antes de mudar
        SaveCurrentPosition();

        // Desativa o mundo atual
        worlds[currentWorld].SetActive(false);

        // Calcula o pr�ximo mundo
        currentWorld = (currentWorld + 1) % worlds.Length;

        // Ativa o pr�ximo mundo
        worlds[currentWorld].SetActive(true);

        // Move o personagem para a posi��o salva do novo mundo
        MovePlayerToSavedPosition();
    }*/

    public void SwitchToWorld(int worldIndex) //TELEPORT
    {
        // Desativa o mundo atual
        worlds[currentWorld].SetActive(false);

        // Ativa o mundo especificado
        currentWorld = worldIndex;
        worlds[currentWorld].SetActive(true);
    }

    


    void SaveCurrentPosition()
    {
        //Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //savedPositions[currentWorld] = playerTransform.position;
    }

    void MovePlayerToSavedPosition()
    {
        //Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //playerTransform.position = savedPositions[currentWorld];
    }
}
