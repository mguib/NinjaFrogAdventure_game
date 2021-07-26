using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPing : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool isFront;
    private Vector2 direction;

    public bool isRight;
    public float stopDistance;
    public float speed;
    public float maxVision;
    public Transform point;
    public Transform behaind;
    public Transform headPoint;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (isRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
            
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        GetPlayer();

        onMove();
    }

    void GetPlayer()
    {
        RaycastHit2D behaindHit = Physics2D.Raycast(behaind.position, -direction, maxVision);

        if(behaindHit.collider != null)
        {
            if(behaindHit.transform.CompareTag("Player"))
            {

                isFront = true;

                //Usar para atcar o play quando estiver perto
                float distance = Vector2.Distance(transform.position, behaindHit.transform.position);
                //Debug.Log(distance);
                if (distance >= stopDistance) //Distancia para atacar
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;
                }
                else
                {
                    distance = 0;
                }
            }
            
            
        }

        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Player"))
            {

                isFront = true;
                isRight = !isRight;

                //Usar para atcar o play quando estiver perto
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                //Debug.Log(distance);
                if (distance <= stopDistance) //Distancia para atacar
                {
                    isFront = true;
                    rig.velocity = Vector2.zero;
                }
                else
                {
                    distance = 0;
                }
            }
            
        }

    }

    void onMove()
    {
        anim.SetInteger("transition", 1);
        if (isFront)
        {
            anim.SetInteger("transition", 2);
            if (!isRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }
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
                //other.gameObject.GetComponent<Player>().OnHit();
                //playerDestroyed = true;
                //Tira vida do player
                //GameController.instance.ShowGameOver();
                //Destroy(other.gameObject);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }
}
