using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    // Nome do parâmetro da animação no Animator
    private string animationTriggerName = "attack02";

    private GameObject player;
    private PlayerController playerController;


    void Start()
    {
        // Obtém o componente Animator anexado ao inimigo
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }

    }

    // Método chamado quando outro objeto entra no trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou tem a tag específica, por exemplo, "Player"
        if (other.CompareTag("Player"))
        {
            if (playerController != null && !playerController.hasPotion)
        {
        
            Debug.Log("ATACAR");
            // Ativa o trigger no Animator para iniciar a animação
            animator.SetTrigger(animationTriggerName);
            } else {
                Debug.Log("TEM POÇÃO");
            }
        }
    }
}
