using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float startPos;
    private float length;

    private GameObject cam;
    [SerializeField] private float parallaxEffect;


    // Update is called once per frame
    void Start()
    {
        cam = GameObject.Find("Virtual Camera");
        startPos = transform.position.x;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length){
            startPos += length;
        } else if (temp < startPos - length) {
            startPos -= length;
        }
    }
}
