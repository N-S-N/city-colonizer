using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bluldControler : MonoBehaviour
{
    [Header("Tipe")]
    [SerializeField]bool isObject;

    [Header("Controler")]   
    [SerializeField] List<GameObject> prefeb;
    Vector3 mausePossintion;
    [SerializeField]float maxdistancioOfCamera;
    [SerializeField] LayerMask layerOfGrand;
    int idex;

    [Header("Object")]
    [SerializeField] GameObject PrejebObj;
    Renderer Render;
    Collider Collider;
    bool Disponivel,Colidindo = true;

    #region obj

    private void Start()
    {
        if (!isObject) return;
        Render = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!isObject) return;

        if (Disponivel && Colidindo)
            podeconstruir();
        else
            naoPoderConstruir();
        
    }

    void podeconstruir()
    {
        Render.material.color = Color.white;
    } 
    void naoPoderConstruir()
    {
        Render.material.color = Color.red;
    }

    private void FixedUpdate()
    {
        if (!isObject) return;
        rey();
    }
    void rey()
    {
        if (Collider.bounds.min.y < 1)
        {
            Disponivel = false;
            return;
        }

        for (int i = 0; i < Collider.bounds.size.x; i++)
        {
            for (int j = 0; j < Collider.bounds.size.z; j++)
            {
                RaycastHit hit;
                Vector3 pont = new Vector3(Collider.bounds.min.x + i + 0.5f, Collider.bounds.min.y + 0.5f, Collider.bounds.min.z + j + 0.5f);
                Debug.DrawRay(pont, Vector3.down, Color.white, 0.6f);

                if (!Physics.Raycast(pont, Vector3.down, out hit, 0.6f, layerOfGrand))
                {
                    Disponivel = false;
                    return;
                }
            }
        }
        Disponivel = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isObject) return;
        Colidindo = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isObject) return;
        Colidindo = true;
    }

    #endregion

    #region UI
    public void openBluld(int i)
    {
        prefeb[i].SetActive(true);
        idex = i;
        InvokeRepeating("UpdateBluld",0,0.01f);
    }

    void UpdateBluld()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        if (Physics.Raycast(ray, out hit, maxdistancioOfCamera, layerOfGrand))
        {
            mausePossintion = hit.transform.position;
            mausePossintion.y = hit.transform.position.y + 1;
            prefeb[idex].transform.position = mausePossintion;
        }               
    }

    public void ClosedBluld()
    {
        prefeb[idex].SetActive(false);
        CancelInvoke();
    }

    #endregion
}
