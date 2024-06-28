using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class camcontrol : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField]float velocity;
    [SerializeField] float velocityRotacao;
    Rigidbody rb;
    [SerializeField] private InputAction.CallbackContext iput;
    Vector2 velocityPos = Vector2.zero;
    void Start()
    {
        transform.position = new Vector3 (10, 50, 10);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 move = velocityPos;

        rb.velocity = transform.TransformDirection(new Vector3(move.x * (cam.m_Lens.FieldOfView / 10) * velocity, 0, move.y * (cam.m_Lens.FieldOfView / 10) * velocity * 5));
    }

    public void move(InputAction.CallbackContext context)
    {
        velocityPos = context.ReadValue<Vector2>();

    }

    void movedirection(Vector2 MOVE)
    {

    }
    public void zom(InputAction.CallbackContext context)
    {
        cam.m_Lens.FieldOfView += (context.ReadValue<float>()/100)*-1;
        cam.m_Lens.FieldOfView = Mathf.Clamp(cam.m_Lens.FieldOfView,5,60);
    }

    public void rotecion(InputAction.CallbackContext context)
    {
        transform.eulerAngles += new Vector3(0, (context.ReadValue<Vector2>().y * velocityRotacao)*-1, 0);

    }

}
