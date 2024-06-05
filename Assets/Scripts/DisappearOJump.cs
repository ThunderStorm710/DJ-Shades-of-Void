using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnJump : MonoBehaviour
{
    public GameObject keyPrefab; // Referência ao prefab da chave

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Certifique-se de que o personagem tenha a tag "Player"
        {
            Debug.Log("CONTACTO");
            Vector3 spawnPosition = transform.position; // Posição para spawnar a chave

            Instantiate(keyPrefab, spawnPosition, Quaternion.identity); // Cria a chave
            Destroy(gameObject);
        }
    }
}
