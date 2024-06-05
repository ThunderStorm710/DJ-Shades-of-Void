using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    public string requiredItemName = "Core";

    private GameObject player;
    public GameObject endGame;

    private PlayerController playerController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }

    }

    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the collider is the player.
        {

            if (playerController != null && playerController.hasCore )
            {
                // The player has the required item.
                EndLevelWithVictory();
            }
            else if (playerController != null && playerController.hasCore == false)
            {
                // The player does not have the required item.
                InformPlayerNeedsItem();
            }
        }
    }

    private void EndLevelWithVictory()
    {
        Debug.Log("Victory! Level completed.");
        Time.timeScale = 0f;
        endGame.SetActive(true);


        // Here, you can load a new scene or show a victory message.
        // Example: SceneManager.LoadScene("VictoryScene");
    }

    private void InformPlayerNeedsItem()
    {
        Debug.Log($"You need the {requiredItemName} to complete the level.");
        // Show a UI message to the player or play a sound to indicate they need the item.
    }
}

