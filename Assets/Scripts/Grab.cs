using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    GameObject selectedObject;
    bool grab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Input.mousePosition;

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;

            if(Physics.Raycast(rayo, out hitInfo))
            {
                if(hitInfo.collider.gameObject.tag == "Cubo")
                {
                    Debug.Log(hitInfo.collider.gameObject.name);
                    selectedObject = hitInfo.collider.gameObject;
                    grab = true;
                }
            }

            if (grab)
            {
                selectedObject.transform.position = pos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                grab = false;
            }
        }

        Vector3 mousePos()
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }
    }
}
