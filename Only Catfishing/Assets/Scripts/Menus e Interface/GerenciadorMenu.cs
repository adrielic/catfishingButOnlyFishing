using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorMenu : MonoBehaviour
{
    public GameObject transicao, imgCreditos, imgTutorial;
    private GerenciadorCenas gc;
    public static bool primeiraVez = true;

    void Start()
    {
        gc = transicao.GetComponent<GerenciadorCenas>();
    }

    public void Jogar()
    {
        Debug.Log("iniciando o jogo");

        if (primeiraVez)
        {
            gc.IniciarCena("Intro");
            primeiraVez = false;
        }
        else
            gc.IniciarCena("Pesca");
    }

    public void Créditos()
    {
        Debug.Log("exibindo créditos");
        imgCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        Debug.Log("fechando créditos");
        imgCreditos.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("fechando o jogo");
        Application.Quit();
    }

    public void Tutorial()
    {
        Debug.Log("exibindo tutorial");
        imgTutorial.SetActive(true);
    }

    public void FecharTutorial()
    {
        Debug.Log("fechando tutorial");
        imgTutorial.SetActive(false);
    }
}
