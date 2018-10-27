using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour {

    GameObject target;
    new GameObject camera;
    string togo;
    bool go = false;

     void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        Console.WriteLine("Hola funciono ewe");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Console.WriteLine(other.tag);
        if(other.tag == "Player")
        {
            target = other.gameObject;

        }
        Console.WriteLine(this.gameObject.name);
        switch (this.gameObject.name)
        {
            case "warppradera":
                togo = "interior";
                go = true;
                break;

            case "warpregreso":
                togo = "JuegoPrincipal";
                
                go = true;
                break;
            case "warp2":
                togo = "North";
                go = true;
                break;
            case "warp3":
                togo = "Preplaya2";
                go = true;
                break;
            case "warppueblor":
                togo = "JuegoPrincipal";
                go = true;
                break;
            case "warpcascada":
                togo = "JuegoPrincipal";
                go = true;
                break;
            case "warpsouth":
                togo = "Preplaya2";
                go = true;
                break;
        }
        
    }
    private void Update()
    {
        if(go)
        {
            warpTo(togo);
            go = false;
        }

    }
    void warpTo(string scene)
    {
        Destroy(camera);
        if (camera.activeSelf) {
            SceneManager.LoadScene(scene);
        }

    }
}
