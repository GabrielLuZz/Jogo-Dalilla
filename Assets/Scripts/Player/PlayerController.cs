using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;
    private CapsuleCollider2D colliderPlayer;

    [Header("Atributtes")]
    public float speed;
    public int addJumps;
    public int life;
    public float jumpForce;

    [Header("Bool")]
    public bool isGrounded;
    [HideInInspector] public bool isPause;

    [Header("UI")]
    public TextMeshProUGUI textLife;

    [Header("GameObjects")]
    public GameObject gameOver;
    public GameObject canvasPause;

    [Header("Level")]
    public string levelName;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("wasLoaded") == 1)
        {
            life = PlayerPrefs.GetInt("Life", 0);
            Debug.Log("Game loaded");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            addJumps = 1;

            if (Input.GetButtonDown("Jump") && addJumps > 0)
            {
                addJumps--;
                Jump();
            }
        }
        else

        {
            if (Input.GetButtonDown("Jump") && addJumps > 0)
            {
                addJumps--;
                Jump();
            }
        }

        Attack();

        textLife.text = life.ToString();

        if (life <= 0)
        {
            anim.Play("Die", -1);
            this.enabled = false;
            rb.velocity = Vector2.zero;
            colliderPlayer.enabled = false;
            rb.gravityScale = 0;
            gameOver.SetActive(true);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            PauseScreen();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            PlayerPrefs.SetInt("Life", life);
            Debug.Log("Game saved");
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    #region Move
    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("IsRun", true);
        }
        if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsRun", true);
        }

        if (moveX == 0)
        {
            anim.SetBool("IsRun", false);
        }
    }
    #endregion

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("Attack", -1);
        }
    }

    void PauseScreen()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvasPause.SetActive(false);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvasPause.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1;
        canvasPause.SetActive(false);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("IsJump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("IsJump", false);

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
