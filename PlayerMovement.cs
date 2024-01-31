using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float doubleJumpForce = 4f; 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float horizontalMovement;
    private bool isJumping = false;
    private int numberOfJumps = 0;
    

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI RedcoinText;
    public TextMeshProUGUI livestext;
   

    public GameObject RestartPanel;
    public GameObject ShopPanel;
    public AudioClip coinsound;
    public AudioClip redcoinsound;
    public AudioClip livesound;
    public AudioClip jumpbuttonsound;
    public AudioClip purchase;
    private AudioSource audioSource;
    private GameManager gameManager;
    private Vector3 respawnPoint; //r
    private int remainingrespawns;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        respawnPoint = transform.position;

        gameManager = GameManager.Instance;

        remainingrespawns = 3;
        

        

        if (gameManager.IsSpeedUnlocked())
        {
            moveSpeed = 8f;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        Updatelives();
        UpdateCoins();
        AnimationHandle();
        Flip();
    }

    private void AnimationHandle() 
    {
        horizontalMovement = Input.GetAxis("Horizontal");
    
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping && gameManager.IsDoubleJumpUnlocked() && numberOfJumps < 2) // Allow double jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                numberOfJumps++;
                audioSource.Play();
            }
            else if (!isJumping) // Allow regular jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                animator.SetBool("isFlying", true);
                numberOfJumps = 1;
                
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                    audioSource = gameObject.AddComponent<AudioSource>();

                audioSource.clip = jumpbuttonsound;
                audioSource.Play();
            }
        }
        if (Mathf.Abs(horizontalMovement) > 0f && !isJumping)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false); 
        }
    }



    private void Flip()
    {
        if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("isFlying", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            CollectCoin();
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "RedCoin")
        {
            CollectRedCoin();
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "ShopAlien")
        {
            Time.timeScale = 0f;
            ShopPanel.SetActive(true);
        }
        else if (collision.transform.tag == "Live")
        {
            CollectLive();
            Destroy(collision.gameObject);
            
        }


            //checkpoints and death
        else if (collision.tag == "DeathZone")
        {
            if ( remainingrespawns > 0 ) 
            {
                
                transform.position = respawnPoint;
                remainingrespawns --;
               
            }
            else if ( remainingrespawns <= 0)
            {
                Death();
            }
                   
        }
        else if ( collision.tag == "Checkpoint1")
        {
            if ( remainingrespawns > 0 ) 
            {
                respawnPoint = transform.position;
            }
        }


        else if (collision.CompareTag("Poseidon"))
        {
            if (gameManager.GetCollectedCoins() >= 20)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex + 1);
            }
        }
    }

    
        
    private void CollectLive()
    {
        remainingrespawns++;
        audioSource.PlayOneShot(livesound);

    }

    private void Updatelives()
    {
        livestext.text = remainingrespawns.ToString();
    }

    private void UpdateCoins()
    {
        coinText.text = gameManager.GetCollectedCoins().ToString();
        RedcoinText.text = gameManager.GetCollectedRedCoins().ToString();
    }

    private void CollectCoin()
    {
        audioSource.PlayOneShot(coinsound);
        gameManager.AddCoins(1);
    }

    private void CollectRedCoin()
    {
        audioSource.PlayOneShot(redcoinsound);
        gameManager.AddRedCoins(1);
    }

    public void speedupgrade()
    {
        if (gameManager.GetCollectedRedCoins() >= 1 && !gameManager.IsSpeedUnlocked())
        {
            moveSpeed = 8f;
            gameManager.SetSpeedUnlocked(true);
            gameManager.AddRedCoins(-1);
            audioSource.PlayOneShot(purchase);
        }
    }

    public void jumpUpgrade()
    {
        if (gameManager.GetCollectedRedCoins() >= 2 && !gameManager.IsDoubleJumpUnlocked())
        {
            gameManager.SetDoubleJumpUnlocked(true);
            gameManager.AddRedCoins(-2);
            audioSource.PlayOneShot(purchase);
        }
    }

    private void Death()
    {
        Time.timeScale = 0f;
        RestartPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        RestartPanel.SetActive(false);

        string currentSceneName = SceneManager.GetActiveScene().name;
        gameManager.ResetCoinsForScene(currentSceneName);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void ShopReset()
    {
        Time.timeScale = 1f;
        ShopPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
