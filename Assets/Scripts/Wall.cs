using UnityEngine;

public class Wall : MonoBehaviour
{
    // Método para desativar a parede
    public void Disappear()
    {
        gameObject.SetActive(false);
    }

    // Método para ativar a parede
    public void Appear()
    {
        gameObject.SetActive(true);
    }
}
