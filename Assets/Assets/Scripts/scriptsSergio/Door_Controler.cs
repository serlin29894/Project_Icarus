using UnityEngine;
using System.Collections;

public class Door_Controler : MonoBehaviour {

    public Transform Door;
    public Transform TargetPos;
    public pipeScript objective;

    public bool Zone_Complete;
    public float speed;

    public AudioClip Open_sound;
    public AudioClip Close_sound;
     
    private bool Open = false;
    private Vector3 InitialPos;

    private bool played;

    void Start()
    {
        InitialPos = Door.transform.position;
    }


    void OnTriggerEnter(Collider other)
    {
        if ((other.transform.tag == "Player") && Zone_Complete)
        {
            Open = true;
            played = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.transform.tag == "Player") && Zone_Complete)
        {
            Open = false;
            played = false;
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
            if (Door.position != TargetPos.position)
            {
                OpenDoor();
            }
            
        }
        else if (!Open)
        {
           if (Door.position != InitialPos)
            {
              CloseDoor();
            }
        }
    }



    public void OpenDoor()
    {
        
            Door.position = Vector3.MoveTowards(Door.position, TargetPos.position, Time.deltaTime * speed);
        
        if (this.GetComponent<AudioSource>().isPlaying == false && !played)
        {
            this.GetComponent<AudioSource>().clip = Open_sound;
            this.GetComponent<AudioSource>().Play();
            played = true;
        }
        

    }

    public void CloseDoor()
    {
        Door.position = Vector3.MoveTowards(Door.position, InitialPos, Time.deltaTime * speed);

        if (this.GetComponent<AudioSource>().isPlaying == false && !played)
        {
            this.GetComponent<AudioSource>().clip = Close_sound;
            this.GetComponent<AudioSource>().Play();
            played = true;
        }

    }

}
