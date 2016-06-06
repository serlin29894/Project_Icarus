using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.Collections;

public class interactableObject : MonoBehaviour {

    logScript logRef;
    public PlayerControler playerRef;
    public menuScript menuRef;
    bool isIn;
    bool haveTakenTheLog;
    public bool isDestroyable;


    void Start ()
    {
        logRef = gameObject.GetComponentInChildren<logScript>();
    }
    
    void Update ()
    {   
        if (isIn && playerRef.isInteracting && !haveTakenTheLog)
        {
            playerRef.logList.Add(logRef);
            haveTakenTheLog = true;
            playerRef.isOnMenu = true; // NO SE SETEA, SIN EMBARGO EL CODE PASA POR ENCIMA
            if (logRef.title == "weaponLog")
            {
                menuRef.weaponLog();
            }
            if (logRef.title == "log1")
            {
                menuRef.log1Mission();
            }
            if (logRef.title == "log2")
            {
                menuRef.log2Mission();
            }
            if (logRef.title == "log3")
            {
                menuRef.log3Mission();
            }
            if (logRef.title == "log4")
            {
                menuRef.log4Mission();
            }

            if (isDestroyable)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isIn = true;
            playerRef = col.gameObject.GetComponent<PlayerControler>();
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isIn = false; 
        }
    }
}
