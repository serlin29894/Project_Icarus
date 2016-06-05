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
    public bool isTePipe;
    public float offSetX;
    public float offSetY;

    public triggerScript colliderLeft;
    public triggerScript colliderRight;


    public AudioClip DetachSound;
    public AudioClip AtachSound;
    public AudioClip ImpactSound;

    int counter;
    Vector3 vector;

    void Start ()
    {
        offSetX = 0.4f;
        offSetY = 0.4f;
    }

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

    //check if its TePipe and if it both of sides havve energy
    void LateUpdate ()
    {
        if (isTePipe)
        {
            if (colliderRight.otherPipe != null && colliderLeft.otherPipe != null)
            {
                if (colliderLeft.otherPipe.havePower && colliderRight.otherPipe.havePower)
                {
                    havePower = true; 
                }
                else
                {
                    havePower = false;
                }
            }
            else
            {
                havePower = false;
            }
        }
    }

    //FUNCTIONS
    void setLocationOfTriggers()
    {
        colliderLeft.setLocationOfThisTrigger();
        colliderRight.setLocationOfThisTrigger();
        //colliderExtraque tendra la de tres picos;
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
                    if (this.gameObject.tag == "PipeLineCurve")
                    {  
                        vector.x += offSetX;                        
                    }
                    if (colliderLeft.collidingPipeLeft.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x += offSetX;
                    }
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    leftAttached = true;
                    colliderLeft.collidingPipeLeft.rightAttached = true;



                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();

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
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x += offSetX;                        
                    }
                    if (colliderRight.collidingPipeLeft.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x += offSetX;
                    }
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    leftAttached = true;
                    colliderRight.collidingPipeLeft.rightAttached = true;


                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();

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
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x -= offSetX;                        
                    }
                    if (colliderRight.collidingPipeRight.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x -= offSetX;
                    }
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    rightAttached = true;
                    colliderRight.collidingPipeRight.leftAttached = true;


                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();

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
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x -= offSetX;                        
                    }
                    if (colliderLeft.collidingPipeRight.gameObject.tag == "PipeLineCurve")
                    {
                        vector.x -= offSetX;
                    }
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    rightAttached = true;
                    colliderLeft.collidingPipeRight.leftAttached = true;


                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();

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
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y -= offSetY;                        
                    }
                    if (colliderLeft.collidingPipeUp.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y -= offSetY;
                    }
                    vector.z = colliderLeft.collidingPipeUp.transform.position.z;
                    vector.x = colliderLeft.collidingPipeUp.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    upAttached = true;
                    colliderLeft.collidingPipeUp.downAttached = true;


                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();

                }
            }
            else if (isInContactUp && colliderRight.isTriggerUp)
            {
                if (colliderRight.collidingPipeUp.isAttachedToAnotherPipe && !colliderRight.collidingPipeUp.downAttached)
                {
                    //change the variable of the pipe
                    vector.y = colliderRight.collidingPipeUp.transform.position.y - (this.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2 + colliderRight.collidingPipeUp.GetComponent<MeshRenderer>().bounds.size.y / 2);
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y -= offSetY;                        
                    }
                    if (colliderRight.collidingPipeUp.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y -= offSetY;
                    }
                    vector.z = colliderRight.collidingPipeUp.transform.position.z;
                    vector.x = colliderRight.collidingPipeUp.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    upAttached = true;
                    colliderRight.collidingPipeUp.downAttached = true;


                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();
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
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y += offSetY;                        
                    }
                    if (colliderLeft.collidingPipeDown.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y += offSetY;
                    }
                    vector.z = colliderLeft.collidingPipeDown.transform.position.z;
                    vector.x = colliderLeft.collidingPipeDown.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    downAttached = true;
                    colliderLeft.collidingPipeDown.upAttached = true;



                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();

                }
            }
            else if (isInContactDown && colliderRight.isTriggerDown)
            {
                if (colliderRight.collidingPipeDown.isAttachedToAnotherPipe && !colliderRight.collidingPipeDown.upAttached)
                {
                    //change the variables of the pipe
                    vector.y = colliderRight.collidingPipeDown.transform.position.y + (this.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2 + colliderRight.collidingPipeDown.GetComponent<MeshRenderer>().bounds.size.y / 2);
                    if (this.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y += offSetY;                       
                    }
                    if (colliderRight.collidingPipeDown.gameObject.tag == "PipeLineCurve")
                    {
                        vector.y += offSetY;
                    }
                    vector.z = colliderRight.collidingPipeDown.transform.position.z;
                    vector.x = colliderRight.collidingPipeDown.transform.position.x;
                    this.gameObject.transform.position = vector;

                    this.GetComponent<Rigidbody>().isKinematic = true;
                    isAttachedToAnotherPipe = true;
                    downAttached = true;
                    colliderRight.collidingPipeDown.upAttached = true;


                    this.GetComponent<AudioSource>().clip = AtachSound;
                    this.GetComponent<AudioSource>().volume = 0.4f;
                    this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                    this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                    this.GetComponent<AudioSource>().Play();
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
            if (colliderRight.otherPipe != null)
            {
                if (colliderRight.otherPipe.havePower == true)
                {
                    havePower = true;
                }
            }

            if (colliderLeft.otherPipe != null)
            {
                if (colliderLeft.otherPipe.havePower == true)
                {
                    havePower = true;
                }
            }

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
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();

                colliderLeft.collidingPipeLeft.rightAttached = false;
            }
            else if (leftAttached && colliderRight.isTriggerLeft)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
                colliderRight.collidingPipeLeft.rightAttached = false;
            }

            if (rightAttached && colliderRight.isTriggerRight)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
                colliderRight.collidingPipeRight.leftAttached = false;
            }
            else if (rightAttached && colliderLeft.isTriggerRight)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
                colliderLeft.collidingPipeRight.leftAttached = false;
            }
            #endregion

            #region vertical
            if (upAttached && colliderLeft.isTriggerUp)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
                colliderLeft.collidingPipeUp.downAttached = false;
            }
            else if (upAttached && colliderRight.isTriggerUp)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
                colliderRight.collidingPipeUp.downAttached = false;
            }

            if (downAttached && colliderRight.isTriggerDown)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
                colliderRight.collidingPipeDown.upAttached = false;
            }
            else if (downAttached && colliderLeft.isTriggerDown)
            {
                this.GetComponent<AudioSource>().clip = DetachSound;
                this.GetComponent<AudioSource>().volume = 0.4f;
                this.GetComponent<AudioSource>().spatialBlend = 0.5f;
                this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
                this.GetComponent<AudioSource>().Play();
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

    void OnCollisionEnter()
    {
       /* this.GetComponent<AudioSource>().clip = ImpactSound;
        this.GetComponent<AudioSource>().volume = 0.4f;
        this.GetComponent<AudioSource>().spatialBlend = 0.5f;
        this.GetComponent<AudioSource>().reverbZoneMix = 0.6f;
        this.GetComponent<AudioSource>().Play();
        */
    }

    //FALTA AÑADIR EL SISTEMA DE PARTICULAS: IF HAVEPOWER && ISATTACHEDTOANOTHERPIPE && !LEFTATTACHED || !RIGHTATTACHED -> EJECUTA SISTEMA DE PARTICULAS EN EL LEFTATTACHED O RIGHTATTACHED QUE TENGA VALOR FALSE
}
