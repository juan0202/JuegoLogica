using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeText : MonoBehaviour {

    public string changeText = "";
    public GameObject obj;
    Text textUI;
	// Use this for initialization
	void Start () {
        textUI = obj.GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	
    public void Change(string Text)
    {
        textUI.text = Text;
       
    }


}
