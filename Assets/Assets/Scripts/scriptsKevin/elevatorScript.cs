using UnityEngine;
using System.Collections;

public class elevatorScript : MonoBehaviour {

    public Transform upLocation;
    public Transform downLocation;
    public float speed;
    Vector3 targetPos;
    public bool isMoving;
    public switchScript switchs;
    public PlayerControler playerRef;
    	
	// Update is called once per frame
	void Update () 
    {
        if (switchs.activated && !isMoving)
        {
            #region Setting target position depending where is the elevator
            if (this.transform.position == upLocation.transform.position)
            {
                targetPos = downLocation.position;
                isMoving = true;
                playerRef.transform.parent = this.gameObject.transform;
                playerRef.GetComponent<Rigidbody>().isKinematic = true;

                if (this.GetComponent<AudioSource>().isPlaying == false)
                {
                    this.GetComponent<AudioSource>().Play();

                }
            }

            if (this.transform.position == downLocation.transform.position)
            {    
                targetPos = upLocation.position;
                isMoving = true;
                playerRef.transform.parent = this.gameObject.transform;
                playerRef.GetComponent<Rigidbody>().isKinematic = true;

                if (this.GetComponent<AudioSource>().isPlaying == false)
                {
                    this.GetComponent<AudioSource>().Play();

                }
            }
            #endregion
        }

        if (isMoving && this.transform.position == targetPos)
        {

            isMoving = false;
            playerRef.transform.parent = null;
            playerRef.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (isMoving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
           
        }
        else
        {
            this.GetComponent<AudioSource>().Stop();
        }
	
	}
}
