using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorGameOver : MonoBehaviour
{
    public GameObject transicao;
    private GerenciadorCenas gc;

    void Awake()
    {
        gc = transicao.GetComponent<GerenciadorCenas>();
        StartCoroutine(RetornarMenu());
    }

    IEnumerator RetornarMenu()
    {
        yield return new WaitForSeconds(3f);
        gc.IniciarCena("Menu");
    }
}
