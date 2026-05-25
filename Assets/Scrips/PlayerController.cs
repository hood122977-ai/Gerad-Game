using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float axisH = 0.0f;
    public float speed = 3.0f;

    public float jumpForce = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";
    public int score;

    public static string gameState = "playing";
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();

        this.animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing")
        {
            return;
        }

        this.axisH = Input.GetAxisRaw("Horizontal");

        if (this.axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (this.axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }

        onGround = Physics2D.Linecast(transform.position,
            transform.position - (transform.up * 0.1f),
            groundLayer);
        if (onGround || axisH != 0)
        {
            this.rigid2D.velocity = new Vector2(axisH * speed, rigid2D.velocity.y);
        }

        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jumpForce);
            this.rigid2D.AddForce(jumpPw, ForceMode2D.Impulse);
            this.goJump = false;
        }
    }

    public void Jump()
    {
        this.goJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if ((collision.gameObject.tag == "Scoreltem"))
        {
            ItemData itme = collision.gameObject.GetComponent<ItemData>();

            score = itme.velue;

            Destroy(collision.gameObject);
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "gameclear";
        GameStop();
    }

    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameover";
        GameStop();

        GetComponent<PolygonCollider2D>().enabled = false;

        this.rigid2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    void GameStop()
    {
        this.rigid2D.velocity = new Vector2(0, 0);
    }
}
