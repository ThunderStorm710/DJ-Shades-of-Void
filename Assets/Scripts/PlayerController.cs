using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;

    private Animator animate;
    
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private bool isGrounded;

    private float moveHorizontal;
    private float moveVertical;
    private float health;

    private bool canJump = true;
    private bool isDead = false;
    public float jumpCooldown = 1f; // Tempo de cooldown em segundos

    private float jumpCooldownTimer;

    private bool isPickaxeHit = false;
    private bool isAxeHit = false;

    private bool isCrouching = false;
    private bool isInteracting = false;
    private bool isWalking = false;
    private string isCollectible = "isCollectible";

    private string isInteractingParam = "isInteracting";
    private string isCrouchingParam = "isCrouching";

    private bool facingRight = true;

    public bool hasPotion = false;
    public bool hasCore = false;
    public bool hasPickaxe = false;
    public bool hasAxe = false;

    private AudioManager audioManager;
    private AudioSource runningSoundSource;

    private Vector3 lastPosition;

    public TextMeshProUGUI distanceText; // Texto para mostrar a distância percorrida com TextMeshPro
    public TextMeshProUGUI distanceTextEndGame; // Texto para mostrar a distância percorrida com TextMeshPro


    public float TotalDistance { get; private set; }

    private Briefing briefing;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        lastPosition = transform.position;

        animate = gameObject.GetComponent<Animator>();

        moveSpeed = 2f;
        jumpForce = 20f;
        isJumping = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        briefing = GameObject.FindGameObjectWithTag("Briefing").GetComponent<Briefing>();


        runningSoundSource = gameObject.AddComponent<AudioSource>();
        runningSoundSource.clip = audioManager.running;
        runningSoundSource.loop = true; // Faz o som repetir automaticamente
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) // Se o jogador está morto, não permite movimento
        {
            return;
        }
        if (!canJump)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
            {
                canJump = true;
            }
        }

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

        if (Mathf.Abs(moveHorizontal) > 0.1f && !runningSoundSource.isPlaying)
        {
            runningSoundSource.Play();
        }
        else if (Mathf.Abs(moveHorizontal) < 0.1f && runningSoundSource.isPlaying)
        {
            // Parar o som de corrida quando o personagem parar de se mover
            runningSoundSource.Stop();
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

        /*if (Input.GetKeyDown(KeyCode.G)){ //ISTO SERVE SÓ DE DEBUG
            Debug.Log("COLLECTIBLE...");
            animate.SetTrigger(isCollectible);
        }*/

        // Calcula a distância percorrida desde o último frame e a adiciona ao total.
        //float distance = Vector3.Distance(transform.position, lastPosition);
        //TotalDistance += distance;

        // Atualiza a última posição para a posição atual no final do frame.
        //lastPosition = transform.position;
        //distanceText.text = "Área Percorrida: " + TotalDistance.ToString("F2") + " unidades";
        //distanceTextEndGame.text = "Área Percorrida: " + TotalDistance.ToString("F2") + " unidades";

        // Opcional: exibe a distância total percorrida.
        //Debug.Log("Distância Total Percorrida: " + TotalDistance + " unidades.");

    }

    public void ObtainObject(GameObject obj)
    {
        // Verifica a tag do objeto para determinar o tipo.
        if (obj.tag == "Potion")
        {
            hasPotion = true;
            Debug.Log("Potion coletada!");
        }

        if (obj.tag == "Core")
        {

            briefing.CompleteObjective();
            hasCore = true;
            Debug.Log("Core coletado!");
        }

        if (obj.tag == "PickAxe")
        {
            hasPickaxe = true;
            Debug.Log("Picareta obtida!");
        }

        if (obj.tag == "Axe")
        {
            hasAxe = true;
            Debug.Log("Axe obtido!");
        }
    }

    void FixedUpdate(){
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            animate.SetBool("isMoving", true);
        } else {
            animate.SetBool("isMoving", false);
        }

        if (!isJumping && moveVertical > 0.1f && isGrounded && canJump)
        {
            isJumping = true;
            isGrounded = false;
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            audioManager.PlaySFX(audioManager.jumpSound); // Tocar som de pulo
            canJump = false;
            jumpCooldownTimer = jumpCooldown;
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
        
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;

            if (isJumping)
            {
                isJumping = false;
                audioManager.PlaySFX(audioManager.jumpLand);
            }

        /*} else if (collision.gameObject.tag == "Wall"){
            Debug.Log("PAREDE");
            if(hasPickaxe && Input.GetKeyDown(KeyCode.E)){
                isPickaxeHit = true;
                Debug.Log("isPickaxeHit" + isPickaxeHit + "ATIVEI");
                //AnimationPickAxe();
            } 
        } else if (collision.gameObject.tag == "WoodWall"){
            if(hasAxe && Input.GetKeyDown(KeyCode.E)){
                Debug.Log("ATIVEI WOOD");
                isAxeHit = true;
                AnimationAxe();
            } */
        } /*else if (collision.gameObject.tag == "PickAxe" || collision.gameObject.tag == "Axe" || collision.gameObject.tag == "Core"){
            //Debug.Log("OBJECTO");
            if (Input.GetKeyDown(KeyCode.E)){
                animate.SetBool(isInteractingParam, true);
            } else {
                animate.SetBool(isInteractingParam, false);
            }
        }*/
        if (collision.gameObject.tag == "Spikes")
        {
            isDead = true;
            Debug.Log("MORRI");
            isGrounded = true;
            if (isJumping)
            {
                isJumping = false;
                audioManager.PlaySFX(audioManager.jumpLand);
            }
            animate.SetBool("isDead", true);
            StartCoroutine(DieAndReloadScene());
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
            isGrounded = false;

        } else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("APANHEI PAREDE");
            
            if (isPickaxeHit){
                Debug.Log("PARTI PAREDE");
                if (gameObject.GetComponent<Inventory>().VerifyItem("Pickaxe")){
                    hasPickaxe = true;
                } else {
                    hasPickaxe = false;
                }
                
                isPickaxeHit = false;
                animate.SetBool("isPickaxeHit", false);

            }
        } else if (collision.gameObject.tag == "Wood Wall")
        {
            if (isAxeHit){
                if (gameObject.GetComponent<Inventory>().VerifyItem("Pickaxe")){
                    hasAxe = true;
                } else {
                    hasAxe = false;
                }
                
                isAxeHit = false; 
                animate.SetBool("isAxeHit", false);

            }
        }
    }

    private IEnumerator DieAndReloadScene()
    {
        // Espera 1 segundo antes de recarregar a cena
        yield return new WaitForSeconds(1f);

        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Reset a flag da animação (caso necessário)
        animate.SetBool("isDead", false);
        isDead = false;
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

    public IEnumerator AnimationPickAxe(Action onCompleted)
    {
        animate.SetBool("isPickaxeHit", true);
        audioManager.PlaySFX(audioManager.pickaxeSound);
        //Debug.Log("ANIMAÇÃO - INICIO");

        yield return new WaitForSeconds(1);  // Supõe que a animação dura 1 segundo

        animate.SetBool("isPickaxeHit", false);
        //Debug.Log("ANIMAÇÃO - FIM");

        onCompleted?.Invoke();
    }

        public IEnumerator AnimationAxe(Action onCompleted)
    {
        animate.SetBool("isAxeHit", true);
        //Debug.Log("ANIMAÇÃO - INICIO");
        audioManager.PlaySFX(audioManager.axeSound);

        yield return new WaitForSeconds(1);  // Supõe que a animação dura 1 segundo

        animate.SetBool("isAxeHit", false);
        //Debug.Log("ANIMAÇÃO - FIM");

        onCompleted?.Invoke();
    }

}
