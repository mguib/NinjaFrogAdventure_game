using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rig;
    Animator anim;

    public float velocity;
    public float jumpForce;
    

    bool isJumping;
    bool doubleJumping;
    bool isBlowing;

    private GameController gc;
    private bool recovery;
    private Healt healthSystem;
    private PlayerAudio playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthSystem = GetComponent<Healt>();
        playerAudio = GetComponent<PlayerAudio>();
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
        if (Input.GetKeyDown(KeyCode.Space) && !isBlowing)
        {
            if (!isJumping)
            {
                playerAudio.PlaySFX(playerAudio.jump);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // == Vector2(0,1)
                anim.SetInteger("transition", 2);
                doubleJumping = true;

            }
            else
            {
                if (doubleJumping)
                {
                    playerAudio.PlaySFX(playerAudio.jump);
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
            OnHit();
        }

        if (collision.gameObject.tag == "Saw")
        {
            //GameController.instance.ShowGameOver();
            OnHit();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            playerAudio.PlaySFX(playerAudio.caixa);
            OnHit();
        }

        if (collision.gameObject.layer == 8)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            playerAudio.PlaySFX(playerAudio.coinSound);
        }

        if (collision.gameObject.CompareTag("Caixa"))
        {
            playerAudio.PlaySFX(playerAudio.caixa);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlaySFX(playerAudio.caixa);
        }
    }

    //float recoveryCount;
    public void OnHit()
    {
        ////recoveryCount += Time.deltaTime;
        //if(recoveryCount >= 2f)
        //{
        //    anim.SetTrigger("hit");
        //    healthSystem.health--;

        //    recoveryCount = 0f;
        //}

        anim.SetTrigger("hit");
        healthSystem.health--;

        if (healthSystem.health <= 0) //&& !recovery) //Game Over
        {
            //Debug.Log("Morreu");
            recovery = true;
            //Game over aqui
            GameController.instance.ShowGameOver();
        }
        
    }

}
