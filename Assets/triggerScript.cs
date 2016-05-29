using UnityEngine;
using System.Collections;

public class triggerScript : MonoBehaviour
{
    public pipeScript collidingPipeLeft;
    public pipeScript collidingPipeRight;
    public pipeScript collidingPipeUp;
    public pipeScript collidingPipeDown;
    public pipeScript ownPipe;
    public bool isTriggerRight;
    public bool isTriggerDown;
    public bool isTriggerLeft;
    public bool isTriggerUp;
    

    void Start()
    {
        ownPipe = this.gameObject.GetComponentInParent<pipeScript>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PipeLine")
        {
            if (isTriggerLeft && !ownPipe.leftAttached && col.gameObject.GetComponent <pipeScript>().colliderLeft.isTriggerLeft || col.gameObject.GetComponent <pipeScript>().colliderRight.isTriggerLeft)
            {                
                ownPipe.isInContactLeft = true;
                collidingPipeLeft = col.gameObject.GetComponent<pipeScript>();
            }
            if (isTriggerRight && !ownPipe.rightAttached && col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerRight || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerRight)
            {
                ownPipe.isInContactRight = true;
                collidingPipeRight = col.gameObject.GetComponent<pipeScript>();
            }
            if(isTriggerUp && !ownPipe.upAttached && col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerDown || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerDown)
            {
                ownPipe.isInContactUp = true;
                collidingPipeUp = col.gameObject.GetComponent<pipeScript>();
            }
            if (isTriggerDown && !ownPipe.downAttached && col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerUp || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerUp)
            {
                ownPipe.isInContactDown = true;
                collidingPipeDown = col.gameObject.GetComponent<pipeScript>();
            }
        }
    }

    public void setLocationOfThisTrigger ()
    {
        //HORIZONTAL VALUES
        if (this.transform.TransformPoint (this.transform.localPosition).x > ownPipe.transform.position.x && Mathf.Abs (this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y) < Mathf.Abs (this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x))
        {
            isTriggerRight = true;
            isTriggerLeft = false;
        }
        else if (this.transform.TransformPoint(this.transform.localPosition).x < ownPipe.transform.position.x && Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y) < Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x))
        {
            isTriggerRight = false;
            isTriggerLeft = true;            
        }
        else
        {
            isTriggerRight = false;
            isTriggerLeft = false;
        }

        //VERTICAL VALUES
        if (this.transform.TransformPoint(this.transform.localPosition).y > ownPipe.transform.position.y && Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x) < Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y))
        {
            isTriggerUp = true;
            isTriggerDown = false;
        }
        else if (this.transform.TransformPoint(this.transform.localPosition).y < ownPipe.transform.position.y && Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x) < Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y))
        {
            isTriggerUp = false;
            isTriggerDown = true;
        }
        else
        {
            isTriggerUp = false;
            isTriggerDown = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PipeLine")
        {
            if (isTriggerLeft)
            {
                ownPipe.isInContactLeft = false;
            }
            if (isTriggerRight)
            {
                ownPipe.isInContactRight = false;
            }
            if (isTriggerDown)
            {
                ownPipe.isInContactDown = false;
            }
            if (isTriggerUp)
            {
                ownPipe.isInContactUp = false;
            }
        }
    }
}
