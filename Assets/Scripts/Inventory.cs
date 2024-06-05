using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    public Slot[] slotsReais;

    public GameObject[] slotsAux;
    void Start()
    {
        slotsAux = new GameObject[4];
        for (int i = 0; i < isFull.Length; i++)
        {
            isFull[i] = false;
            slotsAux[i] = slots[i];
            //slots[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item, string itemName)
    {
        // Encontre o primeiro slot vazio no invent�rio
        for (int i = 0; i < isFull.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true; // Marque o slot como cheio
                slots[i] = Instantiate(item, slots[i].transform, false); // Crie o �cone do item no slot
                slots[i].name = itemName; // Atualize o nome do slot para refletir o item
                slots[i].SetActive(true);
                Debug.Log($"Slot ({i + 1}) {itemName}"); // Log com o nome atualizado
                break;
            }
        }
    }


    public void RemoveItem(string itemName)
    {
        Debug.Log("A remover -> " + itemName);
        int indexToRemove = -1;
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(slots[i].name + " " +itemName);
            if (slots[i] != null && slots[i].name == itemName && isFull[i] == true)
            {
                isFull[i] = false;
                slotsReais[i].removerFilhos();
                //slots[i].GetComponent<Slot>().removerFilhos();
                slots[i] = slotsAux[i];
                indexToRemove = i;
                break;
            }
        }
       /* if (indexToRemove != -1)
        {
            UpdateUI(indexToRemove);
        }*/
    }

    public bool VerifyItem(string name){
       Debug.Log("POTETU");
       for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].name == name && isFull[i] == true)
            {
                return true;
            } 
        }
        return false;
    }

public void UpdateUI(int startIndex)
{
    // Deslocar todos os itens para a esquerda a partir do índice startIndex
    for (int i = startIndex; i < slots.Length - 1; i++)
    {
        if (isFull[i + 1] == false)
        {
            break; // Se o próximo slot não estiver cheio, pare o loop
        }
        else
        {
            // Mover o conteúdo para a esquerda
            slots[i] = slots[i + 1];
            isFull[i] = isFull[i + 1];

            // Limpar o slot onde o item foi movido
            slots[i + 1] = null;
            isFull[i + 1] = false;

            // Atualizar a posição na UI, se o slot não for nulo
            if (slots[i] != null)
            {
                slots[i].transform.position = 
                    slots[i].transform.parent.GetChild(i).position;
            }
        }
    }

    // Opcional: atualizar o último slot na UI
    if (slots[slots.Length - 1] != null)
    {
        slots[slots.Length - 1].transform.position = 
            slots[slots.Length - 1].transform.parent.GetChild(slots.Length - 1).position;
    }
}


}
