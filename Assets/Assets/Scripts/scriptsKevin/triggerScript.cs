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

    public pipeScript otherPipe;

    void Start()
    {
        ownPipe = this.gameObject.GetComponentInParent<pipeScript>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PipeLine" && col.gameObject.GetComponent <pipeScript> () != null)
        {
            if (col.gameObject.GetComponent<pipeScript>().isAttachedToAnotherPipe)
            {
                
                //Debug.Log(isTriggerLeft);
                if (isTriggerLeft && !ownPipe.leftAttached && (col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerRight || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerRight))
                {
                    //Debug.Log(isTriggerLeft);
                    ownPipe.isInContactLeft = true;
                    collidingPipeLeft = col.gameObject.GetComponent<pipeScript>();
                    otherPipe = col.gameObject.GetComponent<pipeScript>();
                    Debug.Log(otherPipe.gameObject.name);
                }
                if (isTriggerRight && !ownPipe.rightAttached && (col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerLeft || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerLeft))
                {
                    ownPipe.isInContactRight = true;
                    collidingPipeRight = col.gameObject.GetComponent<pipeScript>();
                    otherPipe = col.gameObject.GetComponent<pipeScript>();
                    Debug.Log(otherPipe.gameObject.name);
                }
                if (isTriggerUp && !ownPipe.upAttached && (col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerDown || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerDown))
                {
                    ownPipe.isInContactUp = true;
                    collidingPipeUp = col.gameObject.GetComponent<pipeScript>();
                    otherPipe = col.gameObject.GetComponent<pipeScript>();
                    Debug.Log(otherPipe.gameObject.name);
                }
                if (isTriggerDown && !ownPipe.downAttached && (col.gameObject.GetComponent<pipeScript>().colliderLeft.isTriggerUp || col.gameObject.GetComponent<pipeScript>().colliderRight.isTriggerUp))
                {
                    ownPipe.isInContactDown = true;
                    collidingPipeDown = col.gameObject.GetComponent<pipeScript>();
                    otherPipe = col.gameObject.GetComponent<pipeScript>();
                    Debug.Log(otherPipe.gameObject.name);
                }
            }
        }
    }

    public void setLocationOfThisTrigger ()
    {
        //HORIZONTAL VALUES
        if (this.transform.TransformPoint (this.transform.localPosition).x > ownPipe.transform.position.x && Mathf.Abs (this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y) < Mathf.Abs (this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x))
        {
            isTriggerRight = true;
            collidingPipeLeft = null;
            collidingPipeUp = null;
            collidingPipeDown = null;            
            isTriggerLeft = false;
        }
        else if (this.transform.TransformPoint(this.transform.localPosition).x < ownPipe.transform.position.x && Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y) < Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x))
        {
            isTriggerRight = false;
            collidingPipeRight = null;
            collidingPipeUp = null;
            collidingPipeDown = null;
            isTriggerLeft = true;            
        }
        else
        {
            isTriggerRight = false;
            collidingPipeLeft = null;
            collidingPipeRight = null;
            isTriggerLeft = false;
        }

        //VERTICAL VALUES
        if (this.transform.TransformPoint(this.transform.localPosition).y > ownPipe.transform.position.y && Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x) < Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y))
        {
            isTriggerUp = true;
            collidingPipeLeft = null;
            collidingPipeRight = null;
            collidingPipeDown = null;
            isTriggerDown = false;
        }
        else if (this.transform.TransformPoint(this.transform.localPosition).y < ownPipe.transform.position.y && Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).x - ownPipe.transform.position.x) < Mathf.Abs(this.transform.TransformPoint(this.transform.localPosition).y - ownPipe.transform.position.y))
        {
            isTriggerUp = false;
            collidingPipeUp = null;
            collidingPipeRight = null;
            collidingPipeLeft = null;
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
        if (col.gameObject.tag == "PipeLine" && col.gameObject != this.gameObject.transform.parent.gameObject)
        {
            if (isTriggerLeft && col.gameObject.GetComponent<pipeScript>() == collidingPipeLeft)
            {
                otherPipe = null;
                ownPipe.isInContactLeft = false;
            }
            if (isTriggerRight && col.gameObject.GetComponent<pipeScript>() == collidingPipeRight)
            {
                otherPipe = null;
                ownPipe.isInContactRight = false;
            }
            if (isTriggerDown && col.gameObject.GetComponent<pipeScript>() == collidingPipeDown)
            {
                otherPipe = null;
                ownPipe.isInContactDown = false;
            }
            if (isTriggerUp && col.gameObject.GetComponent <pipeScript> () == collidingPipeUp)
            {
                otherPipe = null;
                ownPipe.isInContactUp = false;
            }
        }
    }
}
