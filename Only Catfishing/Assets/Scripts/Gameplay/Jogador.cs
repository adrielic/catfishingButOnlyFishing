using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    private Rigidbody2D rb;
    public Sprite pataFechada, pataAberta;
    private Collider2D colVerificada;
    private float dirX, dirY;
    private bool naPata = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movimentacao();
        Pescar();
    }

    void Movimentacao()
    {
        dirX = Input.GetAxis("Mouse X");
        dirY = Input.GetAxis("Mouse Y");
        rb.velocity = new Vector2(dirX, dirY);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.24f, 4.02f), Mathf.Clamp(transform.position.y, -1.03f, 1.07f));
    }

    void Pescar()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<SpriteRenderer>().sprite = pataFechada;

            if (naPata && colVerificada != null)
            {
                GerenciadorGameplay.Pontuar();
                Destroy(colVerificada.gameObject);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            GetComponent<SpriteRenderer>().sprite = pataAberta;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Target"))
        {
            colVerificada = col;
            naPata = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Target"))
            naPata = false;
    }
}
