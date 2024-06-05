using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{
    public DynamicPortal portal; // Referência ao portal que será modificado
    public int oldWorldIndex, newWorldIndex; // Novo índice do mundo de destino

    private bool isActivated = false;
    private bool playerIsNearby = false; // Controla se o jogador está na proximidade
    private bool isPlayingSound = false; // Controla se o som está tocando
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (playerIsNearby && Input.GetKeyDown(KeyCode.E) && !isPlayingSound)
        {
            isActivated = !isActivated;
            Vector3 escala = gameObject.transform.localScale;
            escala.x *= -1;
            gameObject.transform.localScale = escala; // Aplica a nova escala
            audioManager.PlaySFX(audioManager.leverSound);

            if (isActivated)
            {
                transform.position += Vector3.right * 0.2f;
                portal.SetTeleportDestination(newWorldIndex);
            }
            else
            {
                transform.position += Vector3.left * 0.2f;
                portal.SetTeleportDestination(oldWorldIndex);
            }
            StartCoroutine(ToggleLever());
        }
    }

    private IEnumerator ToggleLever()
    {
        isPlayingSound = true;

        // Espera até que o som termine de tocar
        yield return new WaitForSeconds(audioManager.leverSound.length);
        isPlayingSound = false;

        // Inverte a escala no eixo x para fazer uma reflexão axial
        Debug.Log("Alavanca ativada! Novo destino do portal definido.");
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
