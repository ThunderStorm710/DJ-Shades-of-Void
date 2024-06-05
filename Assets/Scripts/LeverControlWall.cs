using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControlWall : MonoBehaviour
{
    public Wall wall; // Referência à parede
    public Spikes spikes; // Referência aos espinhos
    public GameObject plataforma;

    private bool isActivated = false;
    private bool playerIsNearby = false; // Controla se o jogador está na proximidade
    private bool isPlayingSound = false; // Controla se o som está tocando

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        spikes.Raise();
        wall.Appear();
        plataforma.SetActive(false);
        isActivated = false;
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
                // Desaparece a parede quando a alavanca é ativada
                transform.position += Vector3.right * 0.2f;

                wall.Appear();
                spikes.Lower();
                plataforma.SetActive(true);

                Debug.Log("Alavanca ativada! A parede desapareceu. Spikes ativados.");
            }
            else
            {
                transform.position += Vector3.left * 0.2f;


                wall.Disappear();
                spikes.Raise();
                plataforma.SetActive(false);

                Debug.Log("Alavanca ativada! A parede apareceu. Spikes desativados.");
            }
            StartCoroutine(ToggleLever());
        }
    }

    private IEnumerator ToggleLever()
    {
        isPlayingSound = true;

        // Espera até que o som termine de tocar
        yield return new WaitForSeconds(1f);
        isPlayingSound = false;

        // Inverte a escala no eixo x para fazer uma reflexão axial

    }

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
