using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabEliseo : MonoBehaviour
{

    GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {

            if (cube == null)
            {
                SelectCube();
            }

            else if(cube != null)
            {
                MoveCube();
            }
            else
            {
                RealeaseCube();
            }     
        }
    }

    void MoveCube()
    {
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitInfo;
        cube.SetActive(false);
        if (Physics.Raycast(rayo, out hitInfo) == true)
        {
            cube.transform.position = hitInfo.point + Vector3.up * cube.transform.localScale.y / 2;
        }
        cube.SetActive(true);
    }

    void RealeaseCube()
    {
        Debug.Log("Realease");
        cube = null;
    }

    void SelectCube()
    {
        Debug.Log("Select");
        Vector3 pos = Input.mousePosition;
        if(Application.platform == RuntimePlatform.Android)
        {
            pos = Input.GetTouch(0).position;
        }

        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayo, out hitInfo) == true)
        {
            if (hitInfo.collider.tag.Equals("Cube"))
            {
                cube = hitInfo.collider.gameObject;
                LeanTween.scale(cube, cube.transform.localScale * 1.2f, 0.75f).setEaseInBounce().setLoopPingPong();
            }
        }
    }
}
