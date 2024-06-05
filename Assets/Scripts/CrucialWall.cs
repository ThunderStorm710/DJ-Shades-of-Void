using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucialWall : MonoBehaviour
{
    public void BreakWall()
    {
        Debug.Log("A PAREDE FOI PARTIDA");

        // Adicione lógica aqui para quebrar a parede
        GameManager.instance.BreakCrucialWall();
        Destroy(gameObject); // Ou qualquer outra lógica para remover a parede
    }
}
