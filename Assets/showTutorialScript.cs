using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class showTutorialScript : MonoBehaviour {


    public GameObject reference;
    Text text;
    Color color;
    bool setActive;
	// Use this for initialization
	void Start () {
        text = reference.GetComponent<Text>();
        color = new Color(225,225,225, 0);	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (setActive)
        {
            color.a = Mathf.Lerp (color.a, 255, 5.0f* Time.deltaTime);
            text.color = color;
        }
        if (!setActive)
        {
            color.a = Mathf.Lerp(color.a, 0, 5.0f * Time.deltaTime);
            text.color = color;
        }

	}

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag =="Player")
        {
            setActive = true; 
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag  == "Player")
        {
            setActive = false;
        }
    }
}
