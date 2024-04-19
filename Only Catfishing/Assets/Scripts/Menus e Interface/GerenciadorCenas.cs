using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorCenas : MonoBehaviour
{
    void Start()
    {
        Scene cenaAtual = SceneManager.GetActiveScene();
        

        if (cenaAtual.name == "Pesca")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void IniciarCena(string nomeCena) //Função que é chamada em outros scripts com o nome da cena desejada. 
    {
        StartCoroutine(CarregarCena(nomeCena)); //Inicia a Coroutine que carrega a cena desejada.
    }

    IEnumerator CarregarCena(string nomeCena) //Coroutine que toca a animação de transição e carrega a cena em seguida.
    {
        GetComponent<Animator>().SetTrigger("Iniciar"); //Parâmetro do animator. Faz a transição para a segunda animação (Saída).

        yield return new WaitForSecondsRealtime(1f); //Segura a coroutine por 1 seg.
        SceneManager.LoadScene(nomeCena); //Carrega a cena.
    }
}
