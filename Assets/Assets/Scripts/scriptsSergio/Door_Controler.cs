using UnityEngine;
using System.Collections;

public class Door_Controler : MonoBehaviour {

    public Transform Door;
    public Transform TargetPos;
    public pipeScript objective;

    public bool Zone_Complete;
    public float speed;

     
    private bool Open = false;
    private Vector3 InitialPos;

    void Start()
    {
        InitialPos = Door.transform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        if ((other.transform.tag == "Player") && Zone_Complete)
        {
            Open = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.transform.tag == "Player") && Zone_Complete)
        {
            Open = false;
        }
    }


    void Update()
    {

        if (objective.havePower)
        {
            Zone_Complete = true;
        }

        if (Open)
        {
           OpenDoor();
        }
        else if (!Open)
        {
           
           CloseDoor();
        }
    }



    public void OpenDoor()
    {
        Door.position = Vector3.Lerp(Door.position,TargetPos.position, Time.deltaTime * speed);
    }

    public void CloseDoor()
    {
        Door.position = Vector3.Lerp(Door.position, InitialPos, Time.deltaTime * speed);
    }

}
