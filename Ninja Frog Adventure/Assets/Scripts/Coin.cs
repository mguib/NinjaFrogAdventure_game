using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    public int score;
    public GameObject coletecd;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circle.enabled = false;
            coletecd.SetActive(true);
            //Quando o personagem coletar 5 itens, ele ganha um aumento de velocidade por 5s
            Destroy(gameObject, 0.25f);
            GameController.instance.AddCoin();
            GameController.instance.UpdateScoreText();
            GameController.instance.totalScore += score;
        }
    }
}
