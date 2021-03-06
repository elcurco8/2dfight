﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selecter_script : MonoBehaviour
{
    public float tiempoEntreSeleccion;

    private _Joestick joestick;
    private GameObject player;
    private float timer; // se usa para el tiempo entre seleccion.
    public Color colorJugador;
    public bool habilitado;
    public bool locked;

    public float tiempoEntreBacks;
    public float timerBack;



    void Start()
    {
        timer = tiempoEntreSeleccion;
        habilitado = false;



        // determina que Joestick y que Players estan referenciados..
        string nroSelecter = this.gameObject.name[this.gameObject.name.Length - 1].ToString();
        player = GameObject.Find("Players/Player" + nroSelecter);
        if (nroSelecter == "1")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;
        }
        else if (nroSelecter == "2")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick2;
        }
        else if (nroSelecter == "3")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick3;
        }
        else if (nroSelecter == "4")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick4;
        }

        PintaSelecter();
    }

    void Update()
    {
        if (timerBack > 0)
            timerBack -= Time.deltaTime;

        if (locked == false)
        {
            if (timer <= 0 && habilitado)
            {
                if (joestick.fderecha || joestick.direccionJoestickIzquierdo.x > 0)
                {
                    player.GetComponent<Player_Skin_Controller>().SelectedSkin = player.GetComponent<Player_Skin_Controller>().SelectedSkin + 1;
                    timer = tiempoEntreSeleccion;
                    Invoke("PintaSelecter", 0.05f);
                }
                if (joestick.fizquierda || joestick.direccionJoestickIzquierdo.x < 0)
                {
                    player.GetComponent<Player_Skin_Controller>().SelectedSkin = player.GetComponent<Player_Skin_Controller>().SelectedSkin - 1;
                    timer = tiempoEntreSeleccion;
                    Invoke("PintaSelecter", 0.05f);
                }
            }
            else //mayor a 0
            {
                timer -= Time.deltaTime;
            }

            if (habilitado)
            {
                Invoke("PintaSelecter", 0.05f);
            }
            else
            {
                PintaSelecter(Color.black);
            }
            if (habilitado && joestick.b3)
            {
                locked = true;
                //reproduce animacion de entrada.
                this.gameObject.transform.Find("OK").GetComponent<Animator>().SetBool("OK", true);
                ReproduceSonido("entrada");
            }
            if (joestick.b2 && timerBack <= 0)
            {
                GameObject.Find("ControladorUI").GetComponent<UI_seleccionPersonajes>().PintarJugador(player, Color.black);
                GameObject.Find("ControladorUI").GetComponent<UI_seleccionPersonajes>().PintaSelecter(this.gameObject, Color.black);
                habilitado = false;
                timerBack = tiempoEntreBacks;

            }
        }
        else // (locked == true)
        {
            if (joestick.b2 && timerBack <= 0)
            {
                locked = false;
                habilitado = true;
                this.gameObject.transform.Find("OK").GetComponent<Animator>().SetBool("OK", false);
                ReproduceSonido("salida");
                timerBack = tiempoEntreBacks;

            }

        }





    }

    public void PintaSelecter()
    {
        // Metodo que pinta el selecter del color del jugador.
        Color color = SeleccionDeColor(player.GetComponent<Player_Skin_Controller>().SelectedSkin);

        this.colorJugador = color;
        this.gameObject.transform.Find("triangulo DER").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("triangulo IZQ").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("box").GetComponent<SpriteRenderer>().color = color;
    }
    public void PintaSelecter(Color color)
    {
        // Metodo que pinta el selecter del color del jugador.
        color.a = 1f;
        this.colorJugador = color;
        this.gameObject.transform.Find("triangulo DER").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("triangulo IZQ").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("box").GetComponent<SpriteRenderer>().color = color;
    }

    public Color SeleccionDeColor(int n)
    {
        //devuelve el color segun "n". El parametro indica que skin esta seleccionada.

        switch (n)
        {
            case 0:
                colorJugador.r = 0;
                colorJugador.g = 0.7696705f;
                colorJugador.b = 1;
                break;
            case 1:
                colorJugador.r = 0.7075472f;
                colorJugador.g = 0.01668743f;
                colorJugador.b = 0.09091208f;
                break;
            case 2:
                colorJugador.r = 0.9245283f;
                colorJugador.g = 0.2224101f;
                colorJugador.b = 0.8873301f;
                break;
            case 3:
                colorJugador.r = 0.01568627f;
                colorJugador.g = 1;
                colorJugador.b = 0;
                break;
            case 4:
                colorJugador.r = 0.6981f;
                colorJugador.g = 0.1855f;
                colorJugador.b = 0f;
                break;
            default:
                colorJugador = Color.black;
                break;
        }


        colorJugador.a = 1f;
        return colorJugador;
    }

    void ReproduceSonido(string s)
    {
        this.gameObject.transform.Find("OK").gameObject.GetComponent<AudioManagerScript>().Play(s);
    }


}
