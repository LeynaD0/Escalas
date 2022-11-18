using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject cubo, esfera, cilindro;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApagarBotones()
    {
        cubo.SetActive(false);
        esfera.SetActive(false);
        cilindro.SetActive(false);
    }
    public void PrenderBotones()
    {
        cubo.SetActive(true);
        esfera.SetActive(true);
        cilindro.SetActive(true);
    }
}
