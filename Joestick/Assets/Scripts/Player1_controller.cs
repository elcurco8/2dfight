﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_controller : MonoBehaviour
{

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGrounded;
    public float jumpForce;
    public float speed;
    public float tiempo_entre_saltos;
    Rigidbody2D rb;
    public float fuerzaBajada;
    public float saltoHorizontal;


    private _Joestick joestick;
    private bool piesTocandoPiso;
    private float ContadorDeSalto;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;


    }

    void Update()
    {
        ContadorDeSalto += Time.deltaTime;
        piesTocandoPiso = GetComponentInChildren<piesTocando>().piesTocandoPiso;

        if (isGrounded && joestick.b3)
        {
            if (ContadorDeSalto > 0)
            {
                if (piesTocandoPiso) // si esta tocando el piso, salta derecho.
                {
                    rb.velocity = new Vector2(0, jumpForce);
                    ContadorDeSalto = -tiempo_entre_saltos;
                }
                else
                {

                    if (GetComponentInChildren<chekeadorPiso>().ladoColision == "DERECHA")
                    {
                        rb.velocity = new Vector2(-jumpForce, jumpForce);
                    }
                    else
                    {
                        rb.velocity = new Vector2(jumpForce, jumpForce);
                    }
                    ContadorDeSalto = -tiempo_entre_saltos;
                }
            }

        }
    }

    void FixedUpdate()
    {
        isGrounded = GetComponentInChildren<chekeadorPiso>().tocandoPiso;

        // hace que se mueva para los costados
        float x = joestick.direccionJoestickIzquierdo.x;

        if (joestick.direccionJoestickIzquierdo.y < 0 && isGrounded == false)
        {
            Vector3 move = new Vector3(x * speed, -1 * fuerzaBajada, 0f);
            rb.velocity = move;
        }
        else
        {
            Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f);
            rb.velocity = move;
        }





    }



}