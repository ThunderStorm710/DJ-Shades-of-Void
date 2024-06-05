using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class VerticalLoop : MonoBehaviour
{
    private float height = 0.25f; // Altura máxima do movimento vertical
    private float speed = 2.0f; // Velocidade do movimento

    private Vector3 startPos;
    private float direction = 1.0f;

    void Start()
    {
        startPos = transform.position; // Armazena a posição inicial do objeto
    }

    void Update()
    {
        // Calcula a nova posição
        float movement = Mathf.Sin(Time.time * speed) * height;
        transform.position = startPos + new Vector3(0, movement, 0);
    }
}
