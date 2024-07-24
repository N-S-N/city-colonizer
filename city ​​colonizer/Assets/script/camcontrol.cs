using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class camcontrol : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Randow_Map map;
    [SerializeField]float velocity;
    [SerializeField] float velocityRotacao;
    Rigidbody rb;
    [SerializeField] private InputAction.CallbackContext iput;
    Vector2 velocityPos = Vector2.zero;

    [SerializeField] LayerMask layerOfGrand;

    [SerializeField] float maxdistancioOfCamera;

    float rotgecion;
    void Start()
    {
        transform.position = new Vector3 (10, 50, 10);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotgecion = (int)transform.eulerAngles.y;

        Vector2 move = velocityPos;

        rb.velocity = transform.TransformDirection(new Vector3(move.x * (cam.m_Lens.FieldOfView / 10) * velocity, 0, move.y * (cam.m_Lens.FieldOfView / 10) * velocity * 5));
        confander();
      
    }

    void confander()
    {
        float zom = cam.m_Lens.FieldOfView;
        
        if (rotgecion == 0 || rotgecion == 180)//
        {
            if (rotgecion == 0)
            {
                transform.position = new Vector3(Math.Clamp(transform.position.x, 0 + zom - 1, map.MaxMap - zom+1), transform.position.y, Math.Clamp(transform.position.z, 0 + zom/2 - 23f, map.MaxMap - zom/1.7f - 18f));
            }
            else
            {
                transform.position = new Vector3(Math.Clamp(transform.position.x, 0 + zom - 1, map.MaxMap - zom + 1), transform.position.y, Math.Clamp(transform.position.z, 0 + zom/2f + 23f, map.MaxMap - zom/2.6f + 18.5f));
            }
        }
        else if(rotgecion == 90 || rotgecion == 270)
        {
            if (rotgecion == 90)
            {
                transform.position = new Vector3(Math.Clamp(transform.position.x, 0 + (zom/2.1f - 24), map.MaxMap - (zom/2.1f + 23)), transform.position.y, Math.Clamp(transform.position.z, 0 + zom/1.2f  + 1f, map.MaxMap - zom/1.2f  - 1f));
            }
            else
            {
                transform.position = new Vector3(Math.Clamp(transform.position.x, 0 + (zom / 2.1f + 24), map.MaxMap - (zom / 2.1f - 23)), transform.position.y, Math.Clamp(transform.position.z, 0 + zom / 1.2f - 1f, map.MaxMap - zom / 1.2f + 1f));
            }
        }
        //rotacao
        //zom
    }

    public void move(InputAction.CallbackContext context)
    {
        
        velocityPos = context.ReadValue<Vector2>();

    }
    public void zom(InputAction.CallbackContext context)
    {
        cam.m_Lens.FieldOfView += (context.ReadValue<float>()/100)*-1;
        cam.m_Lens.FieldOfView = Mathf.Clamp(cam.m_Lens.FieldOfView,5,60);
    }

    public void rotecion(InputAction.CallbackContext context)
    {
        if(context.started)
            confirmetrotecion(context.ReadValue<Vector2>());
    }
    Vector3 distance;

    void confirmetrotecion(Vector2 cpntedx)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));

        if (Physics.Raycast(ray, out hit, maxdistancioOfCamera, layerOfGrand))
        {
            distance = hit.collider.transform.position;

            transform.eulerAngles += new Vector3(0, (cpntedx.y * velocityRotacao), 0);
            InvokeRepeating("whilehit",0,0.001F);
        }
    }
    Vector3 hitcolader;
    void whilehit()
    {
        hitcolader = Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, maxdistancioOfCamera, layerOfGrand))
        {
            hitcolader = hit.collider.transform.position;
            hitcolader.y = distance.y;

            if (hitcolader != distance)
            {
                float x = 0;
                float z = 0;

                x = hitcolader.x - distance.x;
                z = hitcolader.z - distance.z;

                transform.position = new Vector3(transform.position.x - x, transform.position.y, transform.position.z-z);
                CancelInvoke();
            }
        }
    }
}
