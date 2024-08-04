using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bluldControler : MonoBehaviour
{
    #region variaves
    [Header("Tipe")]
    [SerializeField]bool isObject;

    [Header("Controler")]
    [SerializeField] float maxdistancioOfCamera;
    [SerializeField] LayerMask layerOfGrand;
    [SerializeField]List<GameObject> prefeb;
    Vector3 mausePossintion;
    int idex;

    [Header("Object")]
    [SerializeField] int PrejebObj;
    [SerializeField]Renderer Render;
    [SerializeField]Collider Collider;
    bool Disponivel = true;
    bool Colidindo = true;
    [SerializeField] florest florest;
    Vector3 positionBluld;
    Quaternion RotencionBluld;
    int whoIsColoder = 0;
    #endregion

    #region obj
    private void Update()
    {
        
        if (!isObject) return;
        if (Disponivel && Colidindo)
            podeconstruir();
        else
            naoPoderConstruir();
    }

    private void OnEnable()
    {
        Invoke("delay", 0f);
        Invoke("delay", 0.1f);
        Invoke("delay", 0.15f);
    }

    void delay()
    {
        Colidindo = true;
        whoIsColoder = 0;
        //Disponivel = true;
    }

    void podeconstruir()
    {
        Render.material.color = Color.white;

        if (Input.GetMouseButtonDown(0))
        {
            positionBluld = transform.position;
            RotencionBluld = transform.rotation;
            Invoke("buld", 0.1f);
        }
    } 

    void buld()
    {
        if(!gameObject.activeInHierarchy) return;
        GameObject spawm = Instantiate(florest.prevebMap[PrejebObj].obj, positionBluld, RotencionBluld, florest.pai.parent);
        florest.ObjOnMapList.Add(new ObjMap(spawm, positionBluld, RotencionBluld, Collider.bounds.size, PrejebObj));
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
        whoIsColoder++;
        Colidindo = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isObject) return;
        whoIsColoder--;
        if(whoIsColoder < 1)
            Colidindo = true;
    }

    #endregion

    #region UI

    Collider coll;
    public void openBluld(int i)
    {
        prefeb[i].SetActive(true);
        idex = i;
        coll = prefeb[idex].GetComponent<Collider>();
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
            // center
            if (coll.bounds.size.x % 2 == 0)
                mausePossintion.x += 0.5f;
            if (coll.bounds.size.z % 2 == 0)
                mausePossintion.z += 0.5f;

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
