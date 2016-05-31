using UnityEngine;
using System.Collections;

public class pipeScript : MonoBehaviour
{
    //TO DO: 
    //PASS THE ENERGY THROUGH THE LAST PIPE
    //PUT PARTICLES
    //DETACH PIPES WHEN ALL CONNECTED

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

    int counter;
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

        //CONTROLA QUE NO ESTE EN CONTACTO CUAND NO LO ESTA
        //controlIsInContact();
    }



    //FUNCTIONS
    void setLocationOfTriggers()
    {
        colliderLeft.setLocationOfThisTrigger();
        colliderRight.setLocationOfThisTrigger();
        //colliderExtraque tendra la de tres picos;
    }

    void controlIsInContact()
    {
        if (!colliderLeft.isTriggerDown || !colliderRight.isTriggerDown)
        {
            isInContactDown = false;
        }
        if (!colliderRight.isTriggerLeft || !colliderLeft.isTriggerLeft)
        {
            isInContactLeft = false;
        }
        if (!colliderRight.isTriggerRight || !colliderLeft.isTriggerRight)
        {
            isInContactRight = false;
        }
        if (!colliderRight.isTriggerUp || !colliderLeft.isTriggerUp)
        {
            isInContactUp = false;
        } 
    }



    void notAttachedLogic()
    {
        if (!isAttachedToAnotherPipe && !isThisPipeStatic && !isBeingClicked)
        {
            havePower = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            #region horizontalLogic
            //LEFT COLLISION
            if (isInContactLeft && colliderLeft.isTriggerLeft)
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
            else if (isInContactLeft && colliderRight.isTriggerLeft)
            {
                if (colliderRight.collidingPipeLeft.isAttachedToAnotherPipe && !colliderRight.collidingPipeLeft.rightAttached)
                {
                    //change the variables of the pipe
                    vector.y = colliderRight.collidingPipeLeft.transform.position.y;
                    vector.z = colliderRight.collidingPipeLeft.transform.position.z;
                    vector.x = colliderRight.collidingPipeLeft.transform.position.x + (this.gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2 + colliderRight.collidingPipeLeft.GetComponent<MeshRenderer>().bounds.size.x / 2);
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    leftAttached = true;
                    colliderRight.collidingPipeLeft.rightAttached = true;
                }
            }
            //END LEFT COLLISION

            //RIGHT COLLISION
            if (isInContactRight && colliderRight.isTriggerRight)
            {
                if (colliderRight.collidingPipeRight.isAttachedToAnotherPipe && !colliderRight.collidingPipeRight.leftAttached)
                {
                    //change the variables of the pipe
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
            else if (isInContactRight && colliderLeft.isTriggerRight)
            {
                if (colliderLeft.collidingPipeRight.isAttachedToAnotherPipe && !colliderLeft.collidingPipeRight.leftAttached)
                {
                    //change the variables of the pipe
                    vector.y = colliderLeft.collidingPipeRight.transform.position.y;
                    vector.z = colliderLeft.collidingPipeRight.transform.position.z;
                    vector.x = colliderLeft.collidingPipeRight.transform.position.x - (this.gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2 + colliderLeft.collidingPipeRight.GetComponent<MeshRenderer>().bounds.size.x / 2);
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    rightAttached = true;
                    colliderLeft.collidingPipeRight.leftAttached = true;
                }
            }
            //END RIGHT COLLISION LOGIC
            #endregion

            #region vertical
            //UP COLLISION
            if (isInContactUp && colliderLeft.isTriggerUp)
            {
                if (colliderLeft.collidingPipeUp.isAttachedToAnotherPipe && !colliderLeft.collidingPipeUp.downAttached)
                {
                    //change the variables of the pipe
                    vector.y = colliderLeft.collidingPipeUp.transform.position.y - (this.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2 + colliderLeft.collidingPipeUp.GetComponent<MeshRenderer>().bounds.size.y / 2);
                    vector.z = colliderLeft.collidingPipeUp.transform.position.z;
                    vector.x = colliderLeft.collidingPipeUp.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    upAttached = true;
                    colliderLeft.collidingPipeUp.downAttached = true;
                }
            }
            else if (isInContactUp && colliderRight.isTriggerUp)
            {
                if (colliderRight.collidingPipeUp.isAttachedToAnotherPipe && !colliderRight.collidingPipeUp.downAttached)
                {
                    //change the variable of the pipe
                    vector.y = colliderRight.collidingPipeUp.transform.position.y - (this.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2 + colliderRight.collidingPipeUp.GetComponent<MeshRenderer>().bounds.size.y / 2);
                    vector.z = colliderRight.collidingPipeUp.transform.position.z;
                    vector.x = colliderRight.collidingPipeUp.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    upAttached = true;
                    colliderRight.collidingPipeUp.downAttached = true;
                }
            }
            //END UP COLLISION LOGIC

            //DOWN COLLISION
            if (isInContactDown && colliderLeft.isTriggerDown)
            {
                if (colliderLeft.collidingPipeDown.isAttachedToAnotherPipe && !colliderLeft.collidingPipeDown.upAttached)
                {
                    //change the variables of the pipe
                    vector.y = colliderLeft.collidingPipeDown.transform.position.y + (this.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2 + colliderLeft.collidingPipeDown.GetComponent<MeshRenderer>().bounds.size.y / 2);
                    vector.z = colliderLeft.collidingPipeDown.transform.position.z;
                    vector.x = colliderLeft.collidingPipeDown.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    downAttached = true;
                    colliderLeft.collidingPipeDown.upAttached = true;
                }
            }
            else if (isInContactDown && colliderRight.isTriggerDown)
            {
                if (colliderRight.collidingPipeDown.isAttachedToAnotherPipe && !colliderRight.collidingPipeDown.upAttached)
                {
                    //change the variables of the pipe
                    vector.y = colliderRight.collidingPipeDown.transform.position.y + (this.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2 + colliderRight.collidingPipeDown.GetComponent<MeshRenderer>().bounds.size.y / 2);
                    vector.z = colliderRight.collidingPipeDown.transform.position.z;
                    vector.x = colliderRight.collidingPipeDown.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    downAttached = true;
                    colliderRight.collidingPipeDown.upAttached = true;
                }
            }
            //END DOWN COLLISION

            #endregion
        }
    }



    void attachedLogic()
    {
        if (isAttachedToAnotherPipe && !isBeingClicked)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //DO THE PIPE HAS ENERGY?
            //HORIZONTAL
            if (colliderLeft.isTriggerLeft && colliderLeft.collidingPipeLeft != null && colliderLeft.collidingPipeLeft.havePower)
            {
                havePower = true;
            }
            else if (colliderLeft.isTriggerRight && colliderLeft.collidingPipeRight != null && colliderLeft.collidingPipeRight.havePower)
            {
                havePower = true;
            }
            else if (colliderLeft.isTriggerUp && colliderLeft.collidingPipeUp != null && colliderLeft.collidingPipeUp.havePower)
            {
                havePower = true;
            }
            else if (colliderLeft.isTriggerDown && colliderLeft.collidingPipeDown != null && colliderLeft.collidingPipeDown.havePower)
            {
                havePower = true;
            }

            //VERTICAL
            else if (colliderRight.isTriggerLeft && colliderRight.collidingPipeLeft != null && colliderRight.collidingPipeLeft.havePower)
            {
                havePower = true;
            }
            else if (colliderRight.isTriggerRight && colliderRight.collidingPipeRight != null && colliderRight.collidingPipeRight.havePower)
            {
                havePower = true;
            }
            else if (colliderRight.isTriggerUp && colliderRight.collidingPipeUp != null && colliderRight.collidingPipeUp.havePower)
            {
                havePower = true;
            }
            else if (colliderRight.isTriggerDown && colliderRight.collidingPipeDown != null && colliderRight.collidingPipeDown.havePower)
            {
                havePower = true;
            }
        }
    }



    void ifItsAttachedAndClicked()
    {
        if (isBeingClicked && !isThisPipeStatic)
        {
            isAttachedToAnotherPipe = false;
            havePower = false;
            //MAKE COLLIDING PIPES VARIABLES FALSE
            #region horizontal
            if (leftAttached && colliderLeft.isTriggerLeft)
            {
                colliderLeft.collidingPipeLeft.rightAttached = false;
            }
            else if (leftAttached && colliderRight.isTriggerLeft)
            {
                colliderRight.collidingPipeLeft.rightAttached = false;
            }

            if (rightAttached && colliderRight.isTriggerRight)
            {
                colliderRight.collidingPipeRight.leftAttached = false;
            }
            else if (rightAttached && colliderLeft.isTriggerRight)
            {
                colliderLeft.collidingPipeRight.leftAttached = false;
            }
            #endregion

            #region vertical
            if (upAttached && colliderLeft.isTriggerUp)
            {
                colliderLeft.collidingPipeUp.downAttached = false;
            }
            else if (upAttached && colliderRight.isTriggerUp)
            {
                colliderRight.collidingPipeUp.downAttached = false;
            }

            if (downAttached && colliderRight.isTriggerDown)
            {
                colliderRight.collidingPipeDown.upAttached = false;
            }
            else if (downAttached && colliderLeft.isTriggerDown)
            {
                colliderLeft.collidingPipeDown.upAttached = false;
            }
            #endregion
            //---

            leftAttached = false;
            rightAttached = false;
            upAttached = false;
            downAttached = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }




    //FUNCTIONS UNITY
    void OnMouseDown()
    {
        if (!((rightAttached && leftAttached) || (rightAttached && upAttached) || (rightAttached && downAttached) || (leftAttached && rightAttached) || (leftAttached && upAttached) || (leftAttached && downAttached) || (upAttached && rightAttached) || (upAttached && downAttached) || (upAttached && leftAttached) || (downAttached && upAttached) || (downAttached && leftAttached) || (downAttached && rightAttached)))
        {
            isBeingClicked = true;
        }
    }

    void OnMouseUp()
    {
        if (!((rightAttached && leftAttached) || (rightAttached && upAttached) || (rightAttached && downAttached) || (leftAttached && rightAttached) || (leftAttached && upAttached) || (leftAttached && downAttached) || (upAttached && rightAttached) || (upAttached && downAttached) || (upAttached && leftAttached) || (downAttached && upAttached) || (downAttached && leftAttached) || (downAttached && rightAttached)))
        {
            isBeingClicked = false;
        }
    }

    //FALTA AÑADIR EL SISTEMA DE PARTICULAS: IF HAVEPOWER && ISATTACHEDTOANOTHERPIPE && !LEFTATTACHED || !RIGHTATTACHED -> EJECUTA SISTEMA DE PARTICULAS EN EL LEFTATTACHED O RIGHTATTACHED QUE TENGA VALOR FALSE
}
