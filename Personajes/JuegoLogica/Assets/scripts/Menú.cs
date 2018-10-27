using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class Menú : MonoBehaviour {

	
	public void Iniciar() {



        SceneManager.LoadScene("JuegoPrincipal");
       
		
	}
	
	
	public void Salir () {
        SceneManager.LoadScene("Menú");
	}
}
