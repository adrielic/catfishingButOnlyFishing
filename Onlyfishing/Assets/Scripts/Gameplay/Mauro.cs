using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//O Mauro deve iniciar dormindo e acordar periodicamente
//Se o jogador pegar um peixe enquanto o Mauro está acordado, causa Game Over
//Os tempos de soneca e acordado devem ser aleatórios

public class Mauro : MonoBehaviour
{
    public static bool acordado;

    void Start()
    {
        acordado = false;
        StartCoroutine(Soneca());
    }

    IEnumerator Soneca()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 14));
            GetComponent<Animator>().SetTrigger("TrocarEstado");
            Debug.Log("tocando animação");
            yield return new WaitForSeconds(1f);
            acordado = true;
            GetComponent<Animator>().SetTrigger("TrocarEstado");
            Debug.Log("acordado = " + acordado);
            yield return new WaitForSeconds(Random.Range(2, 6));
            GetComponent<Animator>().SetTrigger("TrocarEstado");
            yield return new WaitForSeconds(0.5f);
            acordado = false;
            GetComponent<Animator>().SetTrigger("TrocarEstado");
            Debug.Log("acordado = " + acordado);
        }
    }
}
