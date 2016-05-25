using UnityEngine;
using System.Collections;

public class switchScript : MonoBehaviour {

    public bool activated;
    bool isIn;
    public PlayerControler playerRef;

    void Update ()
    {
        if (isIn && playerRef.isInteracting)
        {
            activated = true; 
        }
        else
        {
            activated = false; 
        }
    }

	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isIn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isIn = false;
        }
    }
}
