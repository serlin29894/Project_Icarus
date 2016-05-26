using UnityEngine;
using System.Collections;

public class pipeScript : MonoBehaviour
{
    //SET UP DOWN LOGIC 

    public bool isAttachedToAnotherPipe;
    public bool leftAttached;
    public bool rightAttached;
    public bool upAttached;
    public bool downAttached;
    public bool isBeingClicked;
    public bool isInContactRight;
    public bool isInContactLeft;
    public bool isInContactUp;
    public bool isInContactDown;
    public bool isThisPipeStatic;
    public bool havePower;

    public triggerScript colliderLeft;
    public triggerScript colliderRight;

    Vector3 vector;


    // Update is called once per frame
    void Update()
    {
        //get current rotation and set right or left colliders
        setLocationOfTriggers();

        //SI NO ESTÁ ATACHEADA A OTRA PIPE
        notAttachedLogic();

        //SI ESTÁ ATACHEADA A OTRA PIPE
        attachedLogic();

        //SI ESTA PIPE ESTA ENGANCHADA SOLO POR UN LADO Y CLICAN EN ELLA, DESENGANCHARLA
        ifItsAttachedAndClicked();
    }


    //FUNCTIONS
    void setLocationOfTriggers ()
    {
        if (colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).x > this.gameObject.transform.position.x && colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).y == colliderRight.transform.TransformPoint(colliderRight.transform.localPosition).y)
        {
            colliderLeft.isTriggerLeft = false;
            colliderLeft.isTriggerRight = true;

            colliderRight.isTriggerLeft = true;
            colliderRight.isTriggerRight = false;
        }
        else if (colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).x < this.gameObject.transform.position.x && colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).y == colliderRight.transform.TransformPoint(colliderRight.transform.localPosition).y)
        {
            colliderLeft.isTriggerLeft = true;
            colliderLeft.isTriggerRight = false;

            colliderRight.isTriggerLeft = false;
            colliderRight.isTriggerRight = true;
        }
        else
        {
            colliderLeft.isTriggerLeft = false;
            colliderLeft.isTriggerRight = false;

            colliderRight.isTriggerLeft = false;
            colliderRight.isTriggerRight = false;
        }

        if (colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).y > this.gameObject.transform.position.y && colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).x == colliderRight.transform.TransformPoint(colliderRight.transform.localPosition).x)
        {
            colliderLeft.isTriggerUp = true;
            colliderLeft.isTriggerDown = false;

            colliderRight.isTriggerUp = false;
            colliderRight.isTriggerDown = true; ;
        }
        else if (colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).y < this.gameObject.transform.position.y && colliderLeft.transform.TransformPoint(colliderLeft.transform.localPosition).x == colliderRight.transform.TransformPoint(colliderRight.transform.localPosition).x)
        {
            colliderLeft.isTriggerUp = false;
            colliderLeft.isTriggerDown = true;

            colliderRight.isTriggerUp = true;
            colliderRight.isTriggerDown = false;
        }
        else
        {
            colliderLeft.isTriggerUp = false;
            colliderLeft.isTriggerDown = false;

            colliderRight.isTriggerUp = false;
            colliderRight.isTriggerDown = false;
        }
    }

    void notAttachedLogic()
    {
        if (!isAttachedToAnotherPipe && !isThisPipeStatic && !isBeingClicked)
        {
            havePower = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            if (isInContactLeft)
            {
                if (colliderLeft.collidingPipeLeft.isAttachedToAnotherPipe && !colliderLeft.collidingPipeLeft.rightAttached)
                {
                    //change the variables of the pipe

                    vector.y = colliderLeft.collidingPipeLeft.transform.position.y;
                    vector.z = colliderLeft.collidingPipeLeft.transform.position.z;
                    vector.x = colliderLeft.collidingPipeLeft.transform.position.x + (this.gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2 + colliderLeft.collidingPipeLeft.GetComponent<MeshRenderer>().bounds.size.x / 2);
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    leftAttached = true;
                    colliderLeft.collidingPipeLeft.rightAttached = true;
                }
            }

            if (isInContactRight)
            {
                if (colliderRight.collidingPipeRight.isAttachedToAnotherPipe && !colliderRight.collidingPipeRight.leftAttached)
                {
                    //change the variables of the pipe
                    //this.gameObject.transform.position = colliderRight.collidingPipeRight.spawnLeft.transform.position;
                    vector.y = colliderRight.collidingPipeRight.transform.position.y;
                    vector.z = colliderRight.collidingPipeRight.transform.position.z;
                    vector.x = colliderRight.collidingPipeRight.transform.position.x - (this.gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2 + colliderRight.collidingPipeRight.GetComponent<MeshRenderer>().bounds.size.x / 2);
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    rightAttached = true;
                    colliderRight.collidingPipeRight.leftAttached = true;
                }
            }
        }
    }

    void attachedLogic ()
    {
        if (isAttachedToAnotherPipe && !isBeingClicked)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //TIENE ENERGIA LA PIPE?

            if (colliderLeft.collidingPipeLeft != null)
            {
                if (colliderLeft.collidingPipeLeft.havePower)
                {
                    havePower = true;
                }
            }

            if (colliderRight.collidingPipeRight != null)
            {
                if (colliderRight.collidingPipeRight.havePower)
                {
                    havePower = true;
                }
            }

        }
    }

    void ifItsAttachedAndClicked ()
    {
        if (isBeingClicked && !isThisPipeStatic)
        {
            if (rightAttached || leftAttached)
            {
                isAttachedToAnotherPipe = false;
                havePower = false;

                //MAKE COLLIDING PIPES VARIABLES FALSE
                if (leftAttached)
                    colliderLeft.collidingPipeLeft.rightAttached = false;

                if (rightAttached)
                    colliderRight.collidingPipeRight.leftAttached = false;
                //---

                leftAttached = false;
                rightAttached = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }





    //FUNCTIONS UNITY
    void OnMouseDown()
    {
        if (!rightAttached || !leftAttached)
            isBeingClicked = true;
    }

    void OnMouseUp()
    {
        if (!rightAttached || !leftAttached)
            isBeingClicked = false;
    }





    //FALTA AÑADIR EL SISTEMA DE PARTICULAS: IF HAVEPOWER && ISATTACHEDTOANOTHERPIPE && !LEFTATTACHED || !RIGHTATTACHED -> EJECUTA SISTEMA DE PARTICULAS EN EL LEFTATTACHED O RIGHTATTACHED QUE TENGA VALOR FALSE
}
