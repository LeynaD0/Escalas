using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    Transform selected;

    GameObject selectedActive;

    [SerializeField]
    Camera cam;

    Vector3 colliderOffset;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray;
            RaycastHit hitInfo;
            ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                if(hitInfo.collider.CompareTag ("Cubo"))
                {
                    GameObject cube = hitInfo.collider.gameObject;
                    CubePosition posicionCubo;
                    posicionCubo = hitInfo.collider.GetComponent<CubePosition>();
                    selected = hitInfo.collider.GetComponent<Transform>();
                    var cubeposition = selected.position;
                    //selectedActive = hitInfo.collider.gameObject;
                    cube.SetActive(false);
                    //selectedActive.SetActive(false);

                    if(selectedActive != null || cube != null)
                    {
                        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                        {
                            colliderOffset = new Vector3(0f, cube.GetComponent<Transform>().localScale.y / 2, 0f);
                            var temporalPos = colliderOffset;
                            cube.SetActive(true);
                            //selectedActive.SetActive(true);
                            cube.GetComponent<Transform>().position = hitInfo.point + temporalPos;
                        }
                        else
                        {
                            cube.SetActive(true);
                            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                            {
                                colliderOffset = new Vector3(0f, cube.GetComponent<Transform>().localScale.y / 2, 0f);
                                var temporalPos = colliderOffset;
                                cube.SetActive(true);
                                //selectedActive.SetActive(true);
                                cube.GetComponent<Transform>().position = posicionCubo.GetComponent<Transform>().position;
                            }
                        }
                    }
                }
            }
        }
    }
}
