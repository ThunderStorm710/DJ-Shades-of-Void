using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucialWall : MonoBehaviour
{
    public void BreakWall()
    {
        Debug.Log("A PAREDE FOI PARTIDA");

        // Adicione l�gica aqui para quebrar a parede
        GameManager.instance.BreakCrucialWall();
        Destroy(gameObject); // Ou qualquer outra l�gica para remover a parede
    }
}
