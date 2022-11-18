using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabPrueba : MonoBehaviour
{ 
    public enum EstadosSelector
    {
        EnEspera,
        SeleccionObjetoMover,
        SeleccionObjetoRotar,
        SeleccionObjetoEscalar,
        SeleccionObjetoCrear,
        Mover,
        Escalar,
        Rotar,
        Soltar,
        EsperaTrasCrear
    }

    [SerializeField]
    EstadosSelector estadoActual = EstadosSelector.EnEspera;
    public GameObject objetoSeleccionado;
    Vector2 mousePos;
    [SerializeField]
    GameObject buttomsCreateMode, buttoms;

    private void Start()
    {
        buttomsCreateMode.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        switch (estadoActual)
        {
            case EstadosSelector.EnEspera:
                estadoActual = EstadosSelector.EnEspera;
                break;
            case EstadosSelector.SeleccionObjetoMover:
                Select();
                break;
            case EstadosSelector.SeleccionObjetoRotar:
                Select();
                break;
            case EstadosSelector.SeleccionObjetoEscalar:
                Select();
                break;
            case EstadosSelector.Mover:
                Move();
                break;
            case EstadosSelector.Soltar:
                Soltar();
                break;
            case EstadosSelector.Rotar:
                Rotar();
                break;
            case EstadosSelector.Escalar:
                Escalar();
                break;
            case EstadosSelector.EsperaTrasCrear:
                estadoActual = EstadosSelector.Mover;
                break;
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
                    GameObject objectHit = hit.transform.gameObject;
                if (objectHit.CompareTag("Mover"))
                {
                    objetoSeleccionado = objectHit;
                    ObjetoSeleccionadoCambiarEstado();
                }
            }
        }
    }
    void ObjetoSeleccionadoCambiarEstado()
    {
        switch (estadoActual)
        {
            case EstadosSelector.SeleccionObjetoMover:
                estadoActual = EstadosSelector.Mover;
                break;
            case EstadosSelector.SeleccionObjetoRotar:
                objetoSeleccionado.GetComponent<Rigidbody>().isKinematic = true;
                mousePos = Input.mousePosition;
                estadoActual = EstadosSelector.Rotar;
                break;
            case EstadosSelector.SeleccionObjetoEscalar:
                estadoActual = EstadosSelector.Escalar;
                break;
            case EstadosSelector.SeleccionObjetoCrear:
                estadoActual = EstadosSelector.EsperaTrasCrear;
                break;
        }
    }

    public void CrearObjecto(GameObject objectoACrear)
    {
        objetoSeleccionado = Instantiate(objectoACrear, Vector3.zero, Quaternion.identity);
        objetoSeleccionado.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        estadoActual = EstadosSelector.EsperaTrasCrear;
    }

    void Move()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        objetoSeleccionado.SetActive(false);
        if (Physics.Raycast(rayo, out hit))
        {
            objetoSeleccionado.transform.position = hit.point + ((Vector3.up * objetoSeleccionado.transform.localScale.y) / 2);
        }
        objetoSeleccionado.SetActive(true);

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.Soltar;
        }
    }

    void Rotar()
    {
        Vector2 mouseDelta = mousePos - (Vector2)Input.mousePosition;
        objetoSeleccionado.transform.Rotate(mouseDelta.y, mouseDelta.x, 0f);

        mousePos = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            objetoSeleccionado.GetComponent<Rigidbody>().isKinematic = false;
            estadoActual = EstadosSelector.EnEspera;
        }
    }

    void Escalar()
    {
        objetoSeleccionado.transform.localScale += Vector3.one * Input.mouseScrollDelta.y;
        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.EnEspera;
        }
    }
    void Soltar()
    {
        objetoSeleccionado = null;
        estadoActual = EstadosSelector.EnEspera;
    }
    public void EnterSelectMove()
    {
        estadoActual = EstadosSelector.SeleccionObjetoMover;
    }
    public void EnterSelectRotate()
    {
        estadoActual = EstadosSelector.SeleccionObjetoRotar;
    }
    public void EnterSelectEscalar()
    {
        estadoActual = EstadosSelector.SeleccionObjetoEscalar;
    }

    public void EnterSelectCreate()
    {
        estadoActual = EstadosSelector.SeleccionObjetoCrear;
        buttomsCreateMode.SetActive(true);
        buttoms.SetActive(false);
    }

    public void OutSelectCreate()
    {
        buttomsCreateMode.SetActive(false);
        buttoms.SetActive(true);
    }

}
