using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Defina a cor desejada no editor do Unity
    public Color[] colors = {Color.white, Color.red, Color.green};

    private WorldSwitcher worldSwitcher;
    private GameObject mundos;

    private SpriteRenderer objectRenderer;

    // Use Start para inicialização
    void Start()
    {
        // Pega o Renderer do objeto para que possamos mudar o material
        objectRenderer = GetComponent<SpriteRenderer>();

        mundos = GameObject.FindGameObjectWithTag("Worlds");
        if (mundos != null)
        {
            worldSwitcher = mundos.GetComponent<WorldSwitcher>();
        }
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        ChangeBackgroundColor();
    }

    public void ChangeBackgroundColor(){


        if (worldSwitcher.worlds[0].activeSelf){
            objectRenderer.material.color = colors[0];

        } else if (worldSwitcher.worlds[1].activeSelf){
            objectRenderer.material.color = colors[1];

        } else if (worldSwitcher.worlds[2].activeSelf){
            objectRenderer.material.color = colors[2];

        }

                //yield return new WaitForSeconds(1); // Espera a animação completar

    }
}
