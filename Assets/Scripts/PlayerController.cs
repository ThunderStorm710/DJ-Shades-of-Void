using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;

    private Animator animate;
    
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private float health;


    private bool isCrouching = false;
    private bool isInteracting = false;
    private bool isWalking = false;
    private string isCollectible = "isCollectible";

    private string isInteractingParam = "isInteracting";
    private string isCrouchingParam = "isCrouching";

    private bool facingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        animate = gameObject.GetComponent<Animator>();

        moveSpeed = 1f;
        jumpForce = 30f;
        isJumping = false;



    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));


        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        } else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        ControlJumpAnimation();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Ativa o par�metro IsCrouching no Animator
            animate.SetBool(isCrouchingParam, true);
            if (moveHorizontal > 0.3f || moveHorizontal < -0.3f){
                animate.SetBool("isMoving", true);

            } else {
                animate.SetBool("isMoving", false);
            }
        }
        else
        {
            // Desativa o par�metro IsCrouching no Animator
            animate.SetBool(isCrouchingParam, false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)){ //ISTO SERVE SÓ DE DEBUG
            animate.SetBool("isWalking", true);
            if (moveHorizontal < 0.3f && moveHorizontal > -0.3f){
                animate.SetBool("isMoving", true);

            } else {
                animate.SetBool("isMoving", false);
            }
        } else {
            animate.SetBool("isWalking", false); 
        }        

        if (Input.GetKeyDown(KeyCode.G)){ //ISTO SERVE SÓ DE DEBUG
            Debug.Log("COLLECTIBLE...");
            animate.SetTrigger(isCollectible);
        }

        if (Input.GetKeyDown(KeyCode.E)){
            animate.SetBool(isInteractingParam, true);
        } else {
             animate.SetBool(isInteractingParam, false);
        }

        



    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            animate.SetBool("isMoving", true);
        } else {
            animate.SetBool("isMoving", false);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    private void ControlJumpAnimation()
    {
        if (rb2D.velocity.y > 0.1)
        {
            animate.SetBool("isJumping", true);
            animate.SetBool("isFalling", false);
        }
        else if (rb2D.velocity.y < -0.1)
        {
            animate.SetBool("isJumping", false);
            animate.SetBool("isFalling", true);
        }
        else
        {
            animate.SetBool("isJumping", false);
            animate.SetBool("isFalling", false);
        }
    }

}
