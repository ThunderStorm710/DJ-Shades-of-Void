using System.Collections;
using UnityEngine;

public class KeyPortal : MonoBehaviour
{
    public Transform teleportDestination; // Ponto para onde o jogador será teleportado
    public WorldSwitcher worldSwitcher;   // Referência ao script WorldSwitcher
    public int destinationWorldIndex;     // Índice do mundo de destino

    private bool playerIsOverlapping = false; // Controla se o jogador está sobre o teletransporte
    private Inventory playerInventory;  // Referência ao inventário do jogador

    public string type;

    AudioManager audioManager;

    public float cooldown = 2.0f; // Cooldown em segundos para o som do portal
    private float lastTeleportTime = -10.0f; // Tempo desde o último teleport

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        if (playerIsOverlapping && Input.GetKeyDown(KeyCode.E) && playerInventory != null && Time.time - lastTeleportTime >= cooldown)
        {
            if (playerInventory.VerifyItem(type + " Key"))
            {
                TeleportPlayer();
                lastTeleportTime = Time.time; // Atualiza o tempo do último teleport
            }
            else
            {
                Debug.Log("Você precisa de uma chave para usar este portal!");
            }
        }
    }

    private void TeleportPlayer()
    {
        if (worldSwitcher != null)
        {
            worldSwitcher.SwitchToWorld(destinationWorldIndex);
        }
        audioManager.PlaySFX(audioManager.portalIn); // Toca o som do portal

        // Teletransporta o jogador para a localização de destino
        GameObject.FindGameObjectWithTag("Player").transform.position = teleportDestination.position;
        Debug.Log("Teleportado para o destino!");
    }

    // Detecta se o jogador entrou no trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOverlapping = true;
            playerInventory = collision.GetComponent<Inventory>();
        }
    }

    // Detecta se o jogador saiu do trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsOverlapping = false;
            playerInventory = null;
        }
    }
}
