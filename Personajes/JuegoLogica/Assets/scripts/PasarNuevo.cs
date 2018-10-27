using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNuevo : MonoBehaviour {

	// Use this for initialization
	public void Iniciar2 () {
        SceneManager.LoadScene("Interior");
		
	}

    
	
	// Update is called once per frame
	public void Salir () {
        SceneManager.LoadScene("Intro");
	}
}
