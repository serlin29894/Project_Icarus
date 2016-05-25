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
            menuRef.log1Mission();
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
