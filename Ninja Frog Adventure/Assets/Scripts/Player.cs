using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rig;
    Animator anim;

    public float velocity;
    public float jumpForce;
    public float vidas;

    bool isJumping;
    bool doubleJumping;

    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jump();
    }

    void Move()
    {
        //Retorna uma direção no eixo X com valor entre -1 e 1
        float direction = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(direction * velocity, rig.velocity.y);

        if (direction > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
            
        }

        if(direction != 0 && !isJumping)
        {
            anim.SetInteger("transition", 1);
        }
        
        if(direction < 0)
        {
            transform.eulerAngles = new Vector2(0,180);
            
        }

        if(direction == 0 && !isJumping)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // == Vector2(0,1)
                anim.SetInteger("transition", 2);
                doubleJumping = true;
            }
            else
            {
                if (doubleJumping)
                {
                    rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    doubleJumping = false;
                    anim.SetInteger("transition", 3);
                }
            }         
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = false;
        }

        if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = true;
        }
    }

}
