using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Louise : MonoBehaviour
{
    [SerializeField] private Transform pata;

    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(pata.position.x, -6.41f, 0.96f), Mathf.Clamp(pata.position.y, 3.08f, 3.08f));
    }
}
