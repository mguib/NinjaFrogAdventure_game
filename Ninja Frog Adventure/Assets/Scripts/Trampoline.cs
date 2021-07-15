using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float jumpForce;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(0f, jumpForce),ForceMode2D.Impulse);


        }
    }
}
