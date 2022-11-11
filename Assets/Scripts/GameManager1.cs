using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            {
                pos = Input.GetTouch(0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                if (hit.collider.gameObject.tag == "Cubo")
                {
                    if(hit.collider.GetComponent<ScaleBool>().estaGrande == false)        //Una condición que comprueba si esta es falsa, si es falsa. Coge el script del cubo y lo convierte en "true" y 
                    {
                        GameObject cubo = hit.collider.gameObject;
                        hit.collider.GetComponent<ScaleBool>().estaGrande = true;
                        LeanTween.scale(cubo, Vector3.one * 4f, 0.5f).setEaseInBounce();
                    }

                    else                                                                  //Sino, lo devuelve a su escala normal.
                    {
                        GameObject cubo = hit.collider.gameObject;
                        hit.collider.GetComponent<ScaleBool>().estaGrande = false;
                        LeanTween.scale(cubo, Vector3.one, 0.5f).setEaseInBounce();
                    }
                }
            }
        }
    }
}
