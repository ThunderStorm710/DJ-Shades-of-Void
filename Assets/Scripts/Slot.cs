using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update
    public void removerFilhos(){
        foreach(Transform child in transform){
            GameObject.Destroy(child.gameObject);
            Debug.Log("Removi filhos");
        }
    }
}
