using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class logScript : MonoBehaviour {

    Text text;
    public string title;
    public string textInLog;
        

	// Use this for initialization
	void Start () 
    {
        if (this.title == "log2")
        {
            textInLog = "Spaceship Status Report:" + "\n" + "Day: 392" + "\n" + "Time: 16:37 – March 3rd – 2217 Earth Time" + "\n"  + "\n" + "\n" + "Collision with unknown mass detected. Left side damaged. Low pressure in cabin detected. Electrical systems damaged. Cryogene machines damaged. Earth communication system damaged. Initializing power saving mode.";
        }

        text = GetComponent<Text>();	
	}		
}
