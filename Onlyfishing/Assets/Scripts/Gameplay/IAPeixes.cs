using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
//O peixe deve entrar em cena intangível até uma posição vertical aleatória dentro dos limites do aquário
//Ao chegar na posição, o seu comportamendo deve ser ativado e ele deixa de ser intangível
//Os peixes não podem sair dos limites do aquário (x = -7.3f ~ 4.3f / y = -2.6f ~ 0f)
//Os três peixes devem ter comportamentos diferentes e trocam de direção ao encostar na pata do gato
//O Peixe Normal apenas se movimenta para a esquerda e direita
//O Peixe Sol se movimenta mais rápido e em zig e zag na vertical
//O Peixe Lua se movimenta mais devagar, porém troca entre o estado de tangibilidade
public class IAPeixes : MonoBehaviour
{
    private float dirX, dirY, vel, velNormal, velAlterada, rPosY;
    private bool entrandoEmCena, comportamentoAtivado, ativouComportamento = false;
    private int modoDeComportamento;
    private SpriteRenderer sr;
    private Color cor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        IniciarPeixe();
    }

    void FixedUpdate()
    {
        if (entrandoEmCena)
            EntrarEmCena(dirY, vel);
        else
        {
            if (cor.a < 1f)
            {
                cor.a += 0.5f * Time.deltaTime;
                sr.color = cor;
            }

            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.3f, 4.3f), Mathf.Clamp(transform.position.y, -2.6f, 0f));
        }


        if (comportamentoAtivado)
        {
            switch (modoDeComportamento)
            {
                case 1:
                    Nadar(dirX, 0, vel);
                    break;
                case 2:
                    Nadar(dirX, dirY, vel);

                    if (!ativouComportamento)
                    {
                        StartCoroutine(FazerZigZag());
                        ativouComportamento = true;
                    }
                    break;
                case 3:
                    Nadar(dirX, 0, vel);

                    if (!ativouComportamento)
                    {
                        StartCoroutine(FicarIntangivel());
                        ativouComportamento = true;
                    }
                    break;
            }
        }

        if (transform.position.x <= -7.3f || transform.position.x >= 4.3f)
            InverterDirecao();
    }

    void IniciarPeixe()
    {
        entrandoEmCena = true;
        comportamentoAtivado = false;

        switch (gameObject.name)
        {
            case "PeixeNormal(Clone)":
                vel = 3f;
                modoDeComportamento = 1;
                break;
            case "PeixeSol(Clone)":
                vel = 4f;
                modoDeComportamento = 2;
                break;
            case "PeixeLua(Clone)":
                vel = 2f;
                modoDeComportamento = 3;
                break;
        }

        int rNum = Random.Range(0, 2);

        if (rNum == 0)
        {
            dirX = -1f;
            sr.flipX = false;
        }
        else
        {
            dirX = 1f;
            sr.flipX = true;
        }

        dirY = 1f;
        velAlterada = vel * 2;
        velNormal = vel;
        rPosY = Random.Range(-2.6f, 0f);
        cor = sr.color;
    }

    void EntrarEmCena(float dirY, float vel)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        transform.Translate(new Vector2(transform.position.x, dirY) * vel * Time.deltaTime);

        if (transform.position.y >= rPosY)
        {
            entrandoEmCena = false;
            comportamentoAtivado = true;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void Nadar(float dirX, float dirY, float vel)
    {
        transform.Translate(new Vector2(dirX, dirY) * vel * Time.deltaTime);
    }

    void InverterDirecao()
    {
        dirX *= -1;
        sr.flipX = !sr.flipX;
    }

    IEnumerator Fugir()
    {
        InverterDirecao();
        vel = velAlterada;
        yield return new WaitForSeconds(2f);
        vel = velNormal;
    }

    IEnumerator FazerZigZag()
    {
        while (true)
        {
            dirY *= -1;
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator FicarIntangivel()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            GetComponent<CircleCollider2D>().enabled = false;

            while (cor.a > 0.1f)
            {
                cor.a -= 1f * Time.deltaTime;
                sr.color = cor;
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1f, 6f));
            GetComponent<CircleCollider2D>().enabled = true;

            while (cor.a < 1f)
            {
                cor.a += 1f * Time.deltaTime;
                sr.color = cor;
                yield return null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            int rNum = Random.Range(0, 2);

            if (rNum == 1)
                StartCoroutine(Fugir());
        }
    }
}
