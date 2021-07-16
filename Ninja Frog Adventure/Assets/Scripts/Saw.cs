using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    public float speed;
    public float moveTime;

    private bool dirRight;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(timer >= moveTime)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            dirRight = !dirRight;
            timer = 0f;
        }

    }
}
