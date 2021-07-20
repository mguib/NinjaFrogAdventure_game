using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mollow : MonoBehaviour
{
    public float speed;
    public float moveTime;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    public Transform headPoint;

    private Rigidbody2D rig;
    private Animator anim;

    private bool dirRight;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            //Se verdadeiro vai para direita
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            //Se falso vai para esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            dirRight = !dirRight;
            timer = 0f;
        }

    }

    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            float height = other.contacts[0].point.y - headPoint.position.y;

            if (height > 0 && !playerDestroyed)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("hit");
                //boxCollider2D.enabled = false;
                //circleCollider2D.enabled = false;
                //rig.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.40f);
            }
            else
            {
                playerDestroyed = true;
                //Tira vida do player
                GameController.instance.ShowGameOver();
                Destroy(other.gameObject);
            }
            
        }
    }

}
