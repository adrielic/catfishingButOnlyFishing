using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorIntro : MonoBehaviour
{
    public GameObject transicao;
    private GerenciadorCenas gc;

    void Start()
    {
        gc = transicao.GetComponent<GerenciadorCenas>();
        StartCoroutine(IniciarJogo());
    }

    IEnumerator IniciarJogo()
    {
        yield return new WaitForSeconds(5f);
        gc.IniciarCena("Pesca");
    }
}
