using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerControler : MonoBehaviour {

    #region Public var
   
    public float Speed;
    public float JumpForce;
    public float SpeedCrounch;
    public float DelayFollowObject;
    public bool Grounded;
    public bool isOnStairs;
    //public SeguimientoCamara Camera_reference;

    #endregion

    public List<logScript> logList;
    public bool isOnMenu;
    public bool isInteracting;
    public GameObject arm_Move;
    public GameObject arm;
    public GameObject weapon;
   // public bool isOnStairs;

    #region Private var

    private Rigidbody MyRigidbody;
    private SpriteRenderer MySpriteRenderer;
    private Puppet2D_GlobalControl Puppet;
    private Animator myAnim;

    private CapsuleCollider MyCollider;
    private float ColliderInitialHeigt;
    private Vector3 ColliderInitialCenter;


    public float axisHorizontal;
    private float axisVertical;

    private bool Crounch = false;

    private bool GravitonWeaponEquip;
    private RaycastHit TargetWeapon;
    private GameObject CurrentObject;
    private bool HaveObject = false;
    private float PlayerObjecDistance;
    private Ray CameraMouse;
    private Vector3 MousePos;

    private Vector3 velc;

    private float Dot;

    private bool ColisionDetected;

    #endregion

    private bool pepe;


    // Use this for initialization
    void Start ()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        Puppet = GetComponent<Puppet2D_GlobalControl>();
        myAnim = GetComponent<Animator>();

        MyCollider = GetComponent<CapsuleCollider>();
        ColliderInitialHeigt = MyCollider.height;
        ColliderInitialCenter = MyCollider.center;

        MyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        
        
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //CheckCollisions();

        isInteracting = Input.GetButton("Interact");
        isOnMenu = Input.GetButton("menu");
    }

    void Update()
    {
        
        #region Movment control

        axisHorizontal = Input.GetAxis("Horizontal");

       if (!isOnStairs)
        {
            Movement(axisHorizontal);
        }
       

       
        Jump();

        #endregion

        //if (isOnStairs)
        //{
        //    axisHorizontal = 0;
        //}


     


        #region Crounch
        CrounchInput();
        CrounchState();
        #endregion

        #region Weapon mode
        if (Input.GetKeyDown(KeyCode.R))
        {

            GravitonWeaponEquip = !GravitonWeaponEquip;
        }

        if ( HaveObject && Input.GetKeyDown(KeyCode.W) )
        {
            TargetWeapon.transform.Rotate(new Vector3(0,90,0));
        }


        #endregion

        #region RetrunTargetWeapon


        if (GravitonWeaponEquip)
        {
            Cursor.visible = true;

            if (Input.GetMouseButtonDown(0))
            {
                CameraMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                MousePos = CameraMouse.origin /*+ CameraMouse.direction*/ * (transform.position - Camera.main.transform.position).magnitude;

                if (Physics.Raycast(CameraMouse, out TargetWeapon, Mathf.Infinity, 1 << 10)); 
                {
                   PlayerObjecDistance = (TargetWeapon.transform.position - transform.position).magnitude;

                   if (PlayerObjecDistance < 10 && !TargetWeapon.rigidbody.isKinematic)
                   {
                      HaveObject = true;
                      CurrentObject = TargetWeapon.transform.gameObject;
                      CurrentObject.GetComponent<Rigidbody>().useGravity = false;
                      CurrentObject.GetComponent<ObjectsBehavior>().takeit = true;
                      velc = CurrentObject.GetComponent<Rigidbody>().velocity;
                   }
                }
            }

          if (CurrentObject != null && Input.GetMouseButtonUp(0))

            {
                HaveObject = false;
                CurrentObject.GetComponent<Rigidbody>().useGravity = true;
                CurrentObject.GetComponent<Rigidbody>().AddForce(velc, ForceMode.Impulse);


                CurrentObject = null;
            }


         if (CurrentObject != null && HaveObject )

            {
                CameraMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                MousePos = CameraMouse.origin + CameraMouse.direction * (transform.position - Camera.main.transform.position).magnitude;

                Dot = Vector3.Dot(Vector3.Normalize(transform.position), Vector3.Normalize(MousePos - transform.position)) * -1;

               //if (Dot >= 0)
               //{
               //    Debug.Log("Right");
               //}
               //else if (Dot < 0)
               //{
               //    Debug.Log("Lefht");
               //}
               //
                PlayerObjecDistance = (TargetWeapon.transform.position - transform.position).magnitude;

                if (PlayerObjecDistance > 10)
                {
                    HaveObject = false;
                    CurrentObject.GetComponent<Rigidbody>().useGravity = true;
                    CurrentObject.GetComponent<Rigidbody>().AddForce(velc, ForceMode.Impulse);

                    CurrentObject = null;
                }

                CurrentObject.transform.position = Vector3.SmoothDamp(CurrentObject.transform.position, new Vector3(MousePos.x, MousePos.y, transform.position.z),ref  velc, DelayFollowObject);


            }



        }



        else
        {
            Cursor.visible = false;
        }
        #endregion

        if (GravitonWeaponEquip && HaveObject )
        {
            arm_Move.SetActive(true);
            arm.SetActive(false);
            weapon.SetActive(false);
            var direction = MousePos - arm_Move.transform.position;
            direction.Normalize();
            var rotAmaunt = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arm_Move.transform.rotation = Quaternion.Euler(0, 0, rotAmaunt + 90);
            
         }
        else
        {
            arm_Move.SetActive(false);
            arm.SetActive(true);
            weapon.SetActive(true);
        }



    }


    public void CrounchInput()
    {
        if (Input.GetButtonDown("Crounch"))
        {
            Crounch = true;
        }

        else if (Input.GetButtonUp("Crounch"))
        {
            Crounch = false;
        }
    }

    public void CrounchState()
    {
        if (Crounch == true)
        {
            MyCollider.height = 0.5F;
            MyCollider.center = new Vector3(0, -0.3f, 0);
        }
        else
        {
            MyCollider.height = ColliderInitialHeigt;
            MyCollider.center = ColliderInitialCenter;
        }

    }

    void Movement (float axisH)
    {
        if (ColisionDetected)
        {
            MyRigidbody.velocity = new Vector3(0, MyRigidbody.velocity.y, MyRigidbody.velocity.z);
            //MovementHorizontal = new Vector3(0, MyRigidbody.velocity.y, 0);
        }
        else
        {
            if (!Crounch)
            {
                MyRigidbody.velocity = new Vector3(axisH * Speed, MyRigidbody.velocity.y, MyRigidbody.velocity.z);
                //MovementHorizontal = new Vector3(axisH * Speed, MyRigidbody.velocity.y, 0);

            }
            else if (Crounch)
            {
                MyRigidbody.velocity = new Vector3(axisH * SpeedCrounch, MyRigidbody.velocity.y, MyRigidbody.velocity.z);
                //MovementHorizontal = new Vector3(axisH * SpeedCrounch, MyRigidbody.velocity.y, 0);
            }
        }

        myAnim.SetFloat("Speed", Mathf.Abs(axisHorizontal));


        ChechkFacing();

    }

    public void CheckCollisions()
    {
        ColisionDetected = false;

        Vector3 UpRightCheck = new Vector3(this.transform.position.x + MyCollider.radius * 2 - 0.1f, this.transform.position.y + MyCollider.height * 1/1.5f , this.transform.position.z);
        Vector3 MidRightCheck = new Vector3(this.transform.position.x + MyCollider.radius *2 - 0.1f , this.transform.position.y, this.transform.position.z);
        Vector3 DownRightCheck = new Vector3(this.transform.position.x + MyCollider.radius * 2 - 0.1f, this.transform.position.y - MyCollider.height * 1 / 1.5f, this.transform.position.z);

        Vector3 UpLefhtCheck = new Vector3(this.transform.position.x - MyCollider.radius * 2 + 0.1f, this.transform.position.y + MyCollider.height * 1 / 1.5f, this.transform.position.z);
        Vector3 MidLefthCheck = new Vector3(this.transform.position.x - MyCollider.radius * 2 + 0.1f, this.transform.position.y, this.transform.position.z);
        Vector3 DownLefhtCheck = new Vector3(this.transform.position.x - MyCollider.radius * 2 + 0.1f, this.transform.position.y - MyCollider.height * 1 / 1.5f, this.transform.position.z);

        Debug.DrawLine(UpRightCheck, new Vector3(this.transform.position.x + MyCollider.radius / 2 + 0.5f * 3, this.transform.position.y + MyCollider.height * 1 / 1.5f, this.transform.position.z), Color.black);
        Debug.DrawLine(DownRightCheck, new Vector3(this.transform.position.x + MyCollider.radius / 2 + 0.5f * 3, this.transform.position.y - MyCollider.height * 1 / 1.5f, this.transform.position.z), Color.green);
         Debug.DrawLine(MidRightCheck, new Vector3(this.transform.position.x + MyCollider.radius / 2 + 0.5f * 3, this.transform.position.y, this.transform.position.z), Color.red);
         Debug.DrawLine(MidLefthCheck, new Vector3(this.transform.position.x - MyCollider.radius / 2 - 0.5f * 3, this.transform.position.y, this.transform.position.z), Color.blue);

        Ray UpRayRight = new Ray(UpRightCheck, new Vector3(this.transform.position.x + MyCollider.radius / 2 + 0.5f * 3, this.transform.position.y + MyCollider.height * 1 / 1.5f, this.transform.position.z));
        Ray MidelRayRight = new Ray(MidRightCheck, new Vector3(this.transform.position.x + MyCollider.radius / 2 + 0.5f * 3, this.transform.position.y, this.transform.position.z));
        Ray DownRayRight = new Ray(DownRightCheck, new Vector3(this.transform.position.x + MyCollider.radius / 2 + 0.5f * 3, this.transform.position.y - MyCollider.height * 1 / 1.5f, this.transform.position.z));

        Ray UpRayLefth = new Ray(UpLefhtCheck, Vector3.left);
        Ray MidelRayLefth = new Ray(MidLefthCheck, Vector3.left);
        Ray DownRayLefth = new Ray(DownLefhtCheck, Vector3.left);

        RaycastHit rightHit;
        RaycastHit lefthHit;
       

        if( !Grounded && 
            (
                ( 
                    Physics.Raycast(UpRayRight, out rightHit, 0.2f) 
                    || Physics.Raycast(MidelRayRight, out rightHit, 0.2f) 
                    || Physics.Raycast(DownRayRight, out rightHit, 0.2f)
                ) 
                ||
                (
                    Physics.Raycast(UpRayLefth, out lefthHit, 0.2f) 
                    || Physics.Raycast(MidelRayLefth, out lefthHit, 0.2f) 
                    || Physics.Raycast(DownRayLefth, out lefthHit, 0.2f)
                )
            ) 
          )
        {
            //Debug.Log("pepe");
            ColisionDetected = true;
        }

       //if (Physics.Raycast(MidelRayRight, out rightHit, 0.1f, 1 << 11))
       //{
       //    Debug.Log("hitttMidel");
       //}
       //
       //if (Physics.Raycast(DownRayRight, out rightHit, 0.1f , 1 << 11))
       //{
       //    Debug.Log("hitttdown");
       //}

/*
        if ( (Physics.Raycast(MidelRayRight, out rightHit ,0.05f) || Physics.Raycast(MidelRayLefth, out lefthHit,0.05f)) && !Grounded)
        {
            MovementHorizontal = new Vector3(0, MyRigidbody.velocity.y, 0);
        }
        else
        {
            if (!Crounch)
            {
                MovementHorizontal = new Vector3(axisHorizontal * Speed, MyRigidbody.velocity.y, 0);

            }
            else if (Crounch)
            {
                MovementHorizontal = new Vector3(axisHorizontal * SpeedCrounch, MyRigidbody.velocity.y, 0);
            }
        }*/
    }

    public void Jump()
    {
        
        Grounded = false;
        Collider [] GroundColliders = Physics.OverlapSphere(this.transform.position - new Vector3(0, MyCollider.bounds.size.y, 0), 0.2f);
        //Debug.DrawLine(this.transform.position - new Vector3(0, MyCollider.bounds.size.y, 0), this.transform.position - new Vector3(0, MyCollider.bounds.size.y, 0) + new Vector3(10,10,0), Color.black);
        //Debug.Log(this.transform.position - new Vector3(0, MyCollider.bounds.size.y, 0));
        for (int i=0; i <GroundColliders.Length; i++)
        {
            if (GroundColliders[i].gameObject != gameObject && GroundColliders[i].gameObject.tag =="Ground")
            {
                Grounded = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) &&  Grounded)
        {
            Debug.Log("dasda");
            MyRigidbody.velocity = new Vector3(0, JumpForce, 0);
        }

        myAnim.SetBool("Ground", Grounded);
        myAnim.SetFloat("verSpeed", MyRigidbody.velocity.y);
    }

    public void ChechkFacing()
    {
        if (!HaveObject)
        {
          if (axisHorizontal > 0)
          {
              
              Puppet.flip = true;
          }
          else if (axisHorizontal < 0)
          {
              
              Puppet.flip = false;

          }
      }
      else
      {
        if (Dot >= 0)
        {
              if (Puppet.flip  )
              {
      
              }
              else
              {
                  Puppet.flip = true; 
              }
        }else if (Dot < 0)
        {
              if (!Puppet.flip)
              {
      
              }
              else
              {
                  Puppet.flip = false;
              }
          }
      }
    }

    public Rigidbody GetRigidbody()
    {
        return MyRigidbody;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9)
        {
           // Camera.main.GetComponent<SeguimientoCamara>().LimitsCamera = col.GetComponent<ZoneValues>();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == 9)
        {
            //Camera.main.GetComponent<SeguimientoCamara>().LimitsCamera = col.GetComponent<ZoneValues>();
        }
    }

   
 

}
