using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Método para erguer os espinhos
    public void Raise()
    {
        // Aqui você pode animar os espinhos subindo
        // Exemplo simples de ativar o GameObject
        gameObject.SetActive(true);
    }

    // Método para abaixar os espinhos
    public void Lower()
    {
        // Aqui você pode animar os espinhos descendo
        // Exemplo simples de desativar o GameObject
        gameObject.SetActive(false);
    }
}
