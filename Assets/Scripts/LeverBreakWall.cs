using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBreakWall : MonoBehaviour
{
    public Wall wall; // Referência à parede que será modificada

    public bool isActivated = false;
    private bool playerIsNearby = false; // Controla se o jogador está na proximidade
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (playerIsNearby && Input.GetKeyDown(KeyCode.E))
        {
            isActivated = !isActivated;
            audioManager.PlaySFX(audioManager.leverSound);

            // Inverte a escala no eixo x para fazer uma reflexão axial
            Vector3 escala = gameObject.transform.localScale;
            escala.y *= -1;
            gameObject.transform.localScale = escala; // Aplica a nova escala

            if (isActivated)
            {
                // Desaparece a parede quando a alavanca é ativada
                //transform.position += Vector3.right * 0.2f;

                wall.Disappear();
                isActivated = true;

                // Opcional: adicionar uma animação ou som para indicar que a alavanca foi ativada
                Debug.Log("Alavanca ativada! A parede desapareceu.");
            } else {
                //transform.position += Vector3.left * 0.2f;

                wall.Appear();
                Debug.Log("Alavanca ativada! A parede apareceu.");

            }
    }
    }

    // Detecta se o jogador entrou na proximidade
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = true;
        }
    }

    // Detecta se o jogador saiu da proximidade
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;
        }
    }

}
