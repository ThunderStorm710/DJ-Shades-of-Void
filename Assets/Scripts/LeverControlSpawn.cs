using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControlSpawn : MonoBehaviour
{
    public GameObject portalPrefab; // Prefab do portal
    public Transform spawnLocation1; // Localização de spawn do primeiro portal
    public Transform spawnLocation2; // Localização de spawn do segundo portal

    private bool isActivated = false;
    private bool isPlayingSound = false; // Controla se o som está tocando
    private GameObject spawnedPortal1; // Referência ao primeiro portal spawnado
    private GameObject spawnedPortal2; // Referência ao segundo portal spawnado
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayingSound)
        {
            if (!isActivated)
            {
                // Faz o spawn dos portais
                spawnedPortal1 = Instantiate(portalPrefab, spawnLocation1.position, spawnLocation1.rotation);
                spawnedPortal2 = Instantiate(portalPrefab, spawnLocation2.position, spawnLocation2.rotation);
                isActivated = true;

                // Opcional: adicionar uma animação ou som para indicar que a alavanca foi ativada
                Debug.Log("Alavanca ativada! Dois portais foram spawnados.");
            }
            else
            {
                // Des-spawna os portais
                Destroy(spawnedPortal1);
                Destroy(spawnedPortal2);
                isActivated = false;

                // Opcional: adicionar uma animação ou som para indicar que a alavanca foi desativada
                Debug.Log("Alavanca desativada! Dois portais foram despawnados.");
            }
            StartCoroutine(ToggleLever());
        }
    }

    private IEnumerator ToggleLever()
    {
        isPlayingSound = true;
        audioManager.PlaySFX(audioManager.leverSound);

        // Espera até que o som termine de tocar
        yield return new WaitForSeconds(audioManager.leverSound.length);
        isPlayingSound = false;


    }
  
}
