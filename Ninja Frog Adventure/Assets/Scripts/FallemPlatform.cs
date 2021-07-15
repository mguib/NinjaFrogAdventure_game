using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallemPlatform : MonoBehaviour
{
    public float fallngTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            Invoke("Falling", fallngTime);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }
}
