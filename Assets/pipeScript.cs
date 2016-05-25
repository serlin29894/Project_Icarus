using UnityEngine;
using System.Collections;

public class pipeScript : MonoBehaviour
{
    public bool isAttachedToAnotherPipe;
    public bool leftAttached;
    public bool rightAttached;
    public bool isBeingClicked;
    public bool isInContactRight;
    public bool isInContactLeft;
    public bool isThisPipeStatic;
    public bool havePower;

    public triggerScript colliderLeft;
    public triggerScript colliderRight;

    Vector3 vector;


    // Update is called once per frame
    void Update()
    {
        //SI NO ESTÁ ATACHEADA A OTRA PIPE
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
                    vector.x = colliderLeft.collidingPipeLeft.transform.position.x +(this.gameObject.GetComponent <MeshRenderer>().bounds.size.x/2 + colliderLeft.collidingPipeLeft.GetComponent <MeshRenderer>().bounds.size.x/2);
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
                    vector.x = colliderRight.collidingPipeRight.transform.position.x +(this.gameObject.GetComponent <MeshRenderer>().bounds.size.x/2 + colliderRight.collidingPipeRight.GetComponent <MeshRenderer>().bounds.size.x/2);
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    rightAttached = true;
                    colliderRight.collidingPipeRight.leftAttached = true;
                }
            }
        }



        //SI ESTÁ ATACHEADA A OTRA PIPE
        if (isAttachedToAnotherPipe && !isBeingClicked)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //TIENE ENERGIA LA PIPE?
            if (!isThisPipeStatic)
            {
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


        //SI ESTA PIPE ESTA ENGANCHADA SOLO POR UN LADO Y CLICAN EN ELLA, DESENGANCHARLA
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
