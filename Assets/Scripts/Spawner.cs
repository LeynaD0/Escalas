using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject cylinderPrefab;

    [SerializeField]
    float timer;
    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = 5f;
            Instantiate(cylinderPrefab, transform.position, transform.rotation);
        }
    }
}
