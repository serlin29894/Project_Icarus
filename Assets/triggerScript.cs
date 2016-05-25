using UnityEngine;
using System.Collections;

public class triggerScript : MonoBehaviour
{
    public pipeScript collidingPipeLeft;
    public pipeScript collidingPipeRight;
    public pipeScript ownPipe;
    public bool isTriggerRight;
    public bool isTriggerLeft;

    void Start()
    {
        ownPipe = this.gameObject.GetComponentInParent<pipeScript>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PipeLine")
        {
            if (isTriggerLeft && !ownPipe.leftAttached)
            {
                ownPipe.isInContactLeft = true;
                collidingPipeLeft = col.gameObject.GetComponent<pipeScript>();
            }
            if (isTriggerRight && !ownPipe.rightAttached)
            {
                ownPipe.isInContactRight = true;
                collidingPipeRight = col.gameObject.GetComponent<pipeScript>();
            }
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
        }
    }
}
