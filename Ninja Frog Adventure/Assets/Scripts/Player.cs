using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rig;
    Animator anim;

    public float velocity;
    public float jumpForce;
    public float health;

    bool isJumping;
    bool doubleJumping;
    bool isBlowing;

    private GameController gc;
    private bool recovery;


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
        
        //Retorna uma dire��o no eixo X com valor entre -1 e 1
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
        if (Input.GetKeyDown(KeyCode.Space) && !isBlowing)
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
            //GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if(collision.gameObject.layer == 8)
        {            
            OnHit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            isBlowing = true;
            anim.SetInteger("transition", 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            isBlowing = false;
        }
    }

    float recoveryCount;
    public void OnHit()
    {
        Debug.Log("chamou");

        Debug.Log(recoveryCount);

        if (health <= 0) //Game Over
        {
            Debug.Log("Morreu");
            recovery = true;
            //Game over aqui
            GameController.instance.ShowGameOver();
        }
        
    }

}
