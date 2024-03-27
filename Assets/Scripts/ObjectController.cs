using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPlayerInRange = false;
    private Animator playerAnimator; 

    private Inventory inventory;
    public GameObject itemButton;


    void Start()
    {

        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }


    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
            PlayerPickUp();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void PlayerPickUp()
    {
        playerAnimator.SetTrigger("isObject");

        Invoke("ResetPickUpAnimation", 1.0f);
        Destroy(gameObject);
    }

    void ResetPickUpAnimation()
    {
        playerAnimator.SetBool("IsPickingUp", false);
    }
}

