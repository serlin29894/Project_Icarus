using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class logScript : MonoBehaviour {

    Text text;
    public string title;
    public string textInLog;


    public logScript  (string title1)
    {
        title = title1;
    }

	// Use this for initialization
	void Start () 
    {
        text = GetComponent<Text>();	
	}	
	
}
