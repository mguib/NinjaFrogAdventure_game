using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool isFront;
    private Vector2 direction;
    private bool isDead;

    public float speed;
    public float maxVision;
    public Transform point;
    public Transform behind;
    public bool isRight;
    public float stopDistance;
    public int heath;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Verificação da posição do player ao iniciar
        if (isRight)//Vira pra direita
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;


        }
        else //Vira pra esquerda
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*Função que faz o inimigo se movimentar
     * Essa função depende do resultado de dois booleanos da função GetPlayer
     * sendo responsável por verificar se o inimigo estar morto e se ele esta na linha
     * de ataque     
    */
    void OnMove()
    {
        if (isFront && !isDead) //Se estar na visão e não morto
        {
            anim.SetInteger("transition", 1);
            if (isRight)//Vira pra direita
            {
                transform.eulerAngles = new Vector2(0, 0); //Rotação do personagem
                direction = Vector2.right;  //Modifica a direção
                rig.velocity = new Vector2(speed, rig.velocity.y);  //Adiciona velocidade

            }
            else //Vira pra esquerda (os comentários acima servem para esse trecho)
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
        }

    }

    private void FixedUpdate()
    {
        GetPlayer();    //Função de localizar o player e verificar se pode atacar      

        OnMove();   //Função que faz o inimigo se movimentar
    }

    /*Essa função é responsável por encontrar o player
     * Esse método é reposnsável por detectar objetos que estão no caminho, verificar
     * quais objetos são esses, dar uma distancia de ataque ao personagem, verificar 
     * se pode atacar na direita ou esquerda;
     */
    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision); //Detecta objetos a uma distancia determinada e uma direção a partir de um objeto

        if (hit.collider != null && !isDead)    //Verifica se esta colidindo em algo
        {
            if (hit.transform.CompareTag("Player")) //Verifica se colide com o player
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);  //Retorna a distancia entre dois pontos (o inimigo e em quem esta colidindo "player")

                if (distance <= stopDistance)//Distancia para atacar
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;    //Faz o inimigo parar

                    anim.SetInteger("transition", 2);

                    hit.transform.GetComponent<Player>().OnHit();   //Atribui um dano ao player
                }
            }
        }

        //Os comentários acima servem para esse trecho abaixo

        RaycastHit2D behindHit = Physics2D.Raycast(behind.position, -direction, maxVision);

        if (behindHit.collider != null && !isDead)
        {
            if (behindHit.transform.CompareTag("Player"))
            {
                //O player esta nas costas do inimigo
                //Debug.Log("Esta atras");
                isRight = !isRight;
                isFront = true;
            }
        }
    }
    /*Método que causa dano no personagem
     * Esse método retira 1 da vida do personagem (inimigo em questão) e verifica se ele esta morto.
     * Se sim, seta animação de morte. Se não, diminui 1 de vida e seta animação de dano
     */
    public void OnHit()
    {
        anim.SetTrigger("hit");
        heath--;

        if (heath <= 0)
        {
            isDead = true;
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }

    //Cria uma linha/gizmos para a direita da posição do personagem para uma direção e o tamanho do raio de alcance
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }

    //Cria uma linha/gizmos para a esquerda (-direction) da posição do personagem para uma direção e o tamanho do raio de alcance
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(behind.position, -direction * maxVision);
    }
}
