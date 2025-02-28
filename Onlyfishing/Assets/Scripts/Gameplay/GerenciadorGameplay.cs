using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GerenciadorGameplay : MonoBehaviour
{
    public TMP_Text txtPontos, txtMaiorPontuacao;
    public static int pontuacao, maiorPontuacao, qntdPeixes;
    public static Animator pontosAnim;
    public GameObject transicao;
    public static GerenciadorCenas gc;
    public GameObject[] peixes;

    void Start()
    {
        gc = transicao.GetComponent<GerenciadorCenas>();
        pontosAnim = txtPontos.GetComponent<Animator>();
        pontuacao = 0;
        qntdPeixes = 0;
        StartCoroutine(GerarPeixes());
    }

    void Update()
    {
        txtPontos.text = "Pontos: " + pontuacao;
        txtMaiorPontuacao.text = "Maior Pontuação: " + maiorPontuacao;
    }

    public static void Pontuar()
    {
        if (!Mauro.acordado)
        {
            pontuacao += 10;
            qntdPeixes--;
            pontosAnim.SetTrigger("Pontuar");
        }
        else
        {
            CalcularPontuacao();
            gc.IniciarCena("GameOver");
        }
    }

    public static void CalcularPontuacao()
    {
        if (pontuacao > maiorPontuacao)
        {
            maiorPontuacao = pontuacao;
        }
    }

    IEnumerator GerarPeixes()
    {
        while (true)
        {
            if (qntdPeixes < 5)
            {
                int rNum = Random.Range(0, 3);
                Instantiate(peixes[rNum]);
                qntdPeixes++;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
