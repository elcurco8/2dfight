﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chidori_sequirPlayer : MonoBehaviour
{
    public bool test; //variable que use para testearlo.

    private string playerName;
    public Color color;
    Vector3 posicionSeguir;
    public GameObject PrefabParticulas;
    public Sound[] sounds;

    private GameObject particulas;


    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
        }

    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        s.source.PlayOneShot(s.clip, 1);
    }


    void Start()
    {
        //cambia el color del chidori.

        color.a = 0.85f; //cambio el alfa del color
        this.transform.Find("rayos").gameObject.GetComponent<SpriteRenderer>().color = color;
        this.transform.Find("chidori").gameObject.GetComponent<SpriteRenderer>().color = color;

        if (!test)
            playerName = this.name.Substring(0, 7); //veo quien es el player con el nombre del chidori.

        particulas = Instantiate(PrefabParticulas, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        particulas.GetComponent<chidori_particulas_controller>().master = this.gameObject;
        particulas.GetComponent<chidori_particulas_controller>().color = this.color;
        PlaySound("chidori");
    }

    void Update()
    {
        if (!test)
        {
            posicionSeguir = GameObject.Find(playerName).GetComponent<Transform>().Find("cuerpo").GetComponent<Transform>().Find("mano IZQ").transform.position;
            posicionSeguir.z = posicionSeguir.z - 2;
            this.transform.position = posicionSeguir;

        }
    }
}
