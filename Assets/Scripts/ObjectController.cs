using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private Animator playerAnimator; 

    private GameObject player;
    public GameObject itemButton;

    private Inventory inventory;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null){
            playerAnimator = player.GetComponent<Animator>();
            inventory = player.GetComponent<Inventory>();
        }



    }


    void Update()
    {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {

            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController.hasPickaxe && gameObject.tag == "Wall")
            {


                Debug.Log("Parede a desaparecer." + gameObject.name);

                if (gameObject.name == "ParedeCrucial")
                {
                    GameManager.instance.BreakCrucialWall();
                }
                StartCoroutine(playerController.AnimationPickAxe(() =>
                {
                    // Desativa o gameObject da parede após a animação
                    gameObject.SetActive(false);
                    inventory.RemoveItem("Pickaxe");
                    if (player.GetComponent<Inventory>().VerifyItem("Pickaxe"))
                    {
                        playerController.hasPickaxe = true;
                    }
                    else
                    {
                        playerController.hasPickaxe = false;
                    }
                    GameManager.instance.UsePickaxe();

                    
                }));
            }

            else if (playerController.hasAxe && gameObject.tag == "Wood Wall") {
                StartCoroutine(playerController.AnimationAxe(() =>
                {
                    Debug.Log("Parede de madeira a desaparecer.");

                    gameObject.SetActive(false);

                    // Atualiza o estado da picareta e remove do inventário
                    inventory.VerifyItem("Axe");
                    inventory.RemoveItem("Axe");
                }));


            }
            else if (gameObject.tag == "Wall") {
                Debug.Log("Falta a picareta.");

            } else if (gameObject.tag == "Wood Wall") {
                Debug.Log("Falta a machado.");
            }

            if (gameObject.tag != "Wall" && gameObject.tag != "Wood Wall") {
                audioManager.PlaySFX(audioManager.itemEquipment);
                inventory.AddItem(itemButton, gameObject.name);
                player.GetComponent<PlayerController>().ObtainObject(gameObject);
                //Destroy(gameObject);
                PlayerPickUp();
            }
            
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
        //playerAnimator.SetBool("isInteracting", true);
        Invoke("ResetPickUpAnimation", 1.0f);
        //playerAnimator.SetTrigger("isInteracting", false);
        Destroy(gameObject);
    }

    void ResetPickUpAnimation()
    {
        playerAnimator.SetBool("IsPickingUp", false);
    }
}

