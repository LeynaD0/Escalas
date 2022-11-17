using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabPrueba : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textoBoton;
    public enum EstadoSelector
    {
        EnEspera,
        SeleccionObjetoMover,
        SeleccionObjetoRotar,
        Mover,
        Escalar,
        Rotar,
        Soltar
    }

    [SerializeField]
    EstadoSelector estadoActual = EstadoSelector.EnEspera;
    public GameObject cubo;
    void Start()
    {
        textoBoton.text = "En espera";
    }

    // Update is called once per frame
    void Update()
    {
        switch (estadoActual)
        {
            case EstadoSelector.EnEspera:
                estadoActual = EstadoSelector.SeleccionObjetoMover;
                break;
            case EstadoSelector.SeleccionObjetoMover:
                BotonMover();
                break;
            case EstadoSelector.Mover:
                BotonMover();
                break;
            case EstadoSelector.Soltar:
                Realese();
                break;
        }

        /*if (cubo == null)
        {
            Select();
        }

        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Realese();
            }
            else
            {
                Move();
            }
        }*/
    }

    void Move()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        cubo.SetActive(false);
        if(Physics.Raycast(rayo, out hit))
        {
            cubo.transform.position = hit.point + Vector3.up * cubo.transform.localScale.y / 2;
        }
        cubo.SetActive(true);

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadoSelector.Soltar;
        }
    }

    void Select()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(rayo, out hit))
            {
                if (hit.collider.tag.Equals("Mover"))
                {
                    cubo = hit.collider.gameObject;
                    if(estadoActual == EstadoSelector.SeleccionObjetoMover)
                    {
                        estadoActual = EstadoSelector.Mover;
                    }
                }
            }
        }
    }

    void Realese()
    {
        cubo = null;
        estadoActual = EstadoSelector.EnEspera;
    }

    public void BotonMover()
    {
       if(estadoActual == EstadoSelector.SeleccionObjetoMover)
        {
            textoBoton.text = "Selección objeto mover";
            Select();
        }
    }
}
