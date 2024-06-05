using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination; // Ponto para onde o jogador ser� teleportado
    public WorldSwitcher worldSwitcher;   // Refer�ncia ao script WorldSwitcher
    public int destinationWorldIndex;     // �ndice do mundo de destino

    AudioManager audioManager;


    private bool playerIsOverlapping = false; // Controla se o jogador est� sobre o teletransporte

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Update()
    {
        // Verifica se o jogador est� sobre o teletransporte e se a tecla "E" foi pressionada
        if (playerIsOverlapping && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        // Certifica-se de que o mundo de destino est� ativo
        audioManager.PlaySFX(audioManager.portalIn);
        //yield return new WaitForSeconds(0.5f);
        worldSwitcher.SwitchToWorld(destinationWorldIndex);

        // Teletransporta o jogador para a localiza��o de destino
        GameObject.FindGameObjectWithTag("Player").transform.position = teleportDestination.position;
    }

    // Detecta se o jogador entrou no trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    // Detecta se o jogador saiu do trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}

