using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Material hitMaterial;
    public AudioClip shotSound;
    private AudioSource gunAudioSource;
    public TextMeshProUGUI puntuacion_text;
    public int puntuacion;
    private AudioSource lataAudioSource;
    public AudioClip latasound;

    void Awake()
    {
        gunAudioSource = GetComponent<AudioSource>();
        lataAudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

        
        if((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {
            gunAudioSource.PlayOneShot(shotSound);
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            { 
                pos = Input.GetTouch(0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayo, out hitInfo))
            {
                if (hitInfo.collider.tag.Equals("Lata"))
                {
                    lataAudioSource.PlayOneShot(latasound);
                    Rigidbody rigidbodyLata = hitInfo.collider.GetComponent<Rigidbody>();
                    rigidbodyLata.AddForce(rayo.direction * 50f, ForceMode.VelocityChange);
                    hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                    puntuacion += 10;
                }
            }

            else
            {
                puntuacion -= 5;
            }
        
        }

        if (puntuacion < 0)
        {
            puntuacion = 0;
        }

        puntuacion_text.text = "Puntuación: " + puntuacion.ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
