using System.Collections;
using UnityEngine;

public class DynamicPortal : MonoBehaviour
{
    public Transform teleportDestination1; // Ponto para onde o jogador será teleportado
    public Transform teleportDestination2; // Ponto para onde o jogador será teleportado
    public bool teleport = false;
    public WorldSwitcher worldSwitcher;   // Referência ao script WorldSwitcher
    public int destinationWorldIndex;     // Índice do mundo de destino
    public SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer do portal
    public Sprite newSprite; // Sprite do portal quando ativo
    public Sprite oldSprite; // Sprite do portal quando inativo

    AudioManager audioManager;

    public float cooldown = 2.0f; // Cooldown em segundos para o som do portal
    private float lastTeleportTime = -10.0f; // Tempo desde o último teleport

    private bool playerIsOverlapping = false; // Controla se o jogador está sobre o teletransporte

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (playerIsOverlapping && Input.GetKeyDown(KeyCode.E) && Time.time - lastTeleportTime >= cooldown)
        {
            TeleportPlayer();
            lastTeleportTime = Time.time; // Atualiza o tempo do último teleport
        }
    }

    private void TeleportPlayer()
    {
        audioManager.PlaySFX(audioManager.portalIn); // Toca o som do portal
        worldSwitcher.SwitchToWorld(destinationWorldIndex);

        // Teletransporta o jogador para a localização de destino
        if (teleport)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = teleportDestination2.position;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = teleportDestination1.position;
        }
    }

    // Método para alterar o destino do portal e atualizar o sprite
    public void SetTeleportDestination(int newWorldIndex)
    {
        teleport = !teleport;

        if (teleport)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            spriteRenderer.sprite = oldSprite;
        }
        destinationWorldIndex = newWorldIndex;
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
