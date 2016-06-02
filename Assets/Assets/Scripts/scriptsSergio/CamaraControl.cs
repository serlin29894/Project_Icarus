using UnityEngine;
using System.Collections;

public class CamaraControl : MonoBehaviour {

    #region Public
    public Transform target;

    public float BoxCameraWith;
    //public float BoxCameraHeight;
    public float Zoom;
    public float Camera_Speed;

    public float DistanceToWalls;
    public float DistanceToCeiling;
    public float DistanceToGround;

    public Transform LI_top;
    public Transform LI_right;
    public Transform LI_lefht;
    public Transform LI_down;

    public bool canMove = true;

    float time = 4f;
    

    #endregion

    #region Private

    private float MovmentX;
    private float MovmentY;

    private bool CanMoveRight = true;
    private bool CanMoveLefht = true;
    private bool CanMoveDown = true;
    private bool CanMoveTop = true;


    private Vector3 NewPosition;
    private Vector3 shadowPosition;

    RaycastHit CheackRight;
    RaycastHit CheackLefht;
    RaycastHit CheackTop;
    RaycastHit CheackDown;
    #endregion


    public float OffsetX, OffsetY, newZoom;
    public float PlaceCameraVelc, ZoomVelc;

    

    // Use this for initialization
    void Start()
    {
        

        CameraWithWindow(BoxCameraWith);
        //CameraHeightWindow(BoxCameraHeight);

        shadowPosition = transform.position;


    }

    public void CameraFollow()
    {


        if (CanMoveRight && ( target.position.x > LI_right.position.x))
        {
            //MovmentX = Mathf.Abs(LI_right.position.x - target.position.x);
            //shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(shadowPosition.x + MovmentX, shadowPosition.y, Zoom), Time.deltaTime * Camera_Speed);
            shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(target.position.x, shadowPosition.y, Zoom), Time.deltaTime * Camera_Speed);
            //shadowPosition = Vector3.MoveTowards(shadowPosition, new Vector3(target.position.x, shadowPosition.y, Zoom), Camera_Speed);
            NewPosition = shadowPosition;

        }

        if (CanMoveLefht && ( target.position.x < LI_lefht.position.x))
        {
            //MovmentX = Mathf.Abs(LI_lefht.position.x - target.position.x);
            //
            //shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(shadowPosition.x - MovmentX, shadowPosition.y, Zoom), Time.deltaTime * Camera_Speed);
            shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(target.position.x, shadowPosition.y, Zoom), Time.deltaTime * Camera_Speed);
            //shadowPosition = Vector3.MoveTowards(shadowPosition, new Vector3(target.position.x, shadowPosition.y, Zoom), Camera_Speed);
            NewPosition = shadowPosition;
        }

       /* if (CanMoveTop && (target.position.y > LI_top.position.y))
        {
            MovmentY = Mathf.Abs(LI_top.position.y - target.position.y);
        
            shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(shadowPosition.x, shadowPosition.y + MovmentY, Zoom), Time.deltaTime * Camera_Speed);
            NewPosition = shadowPosition;
        }

        if ( ( target.position.y < LI_down.position.y))
         {
          MovmentY = Mathf.Abs(LI_down.position.y - target.position.y);
          shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(shadowPosition.x, target.position.y - MovmentY , Zoom), Time.deltaTime * Camera_Speed);
          NewPosition = shadowPosition;
         }*/


        if (CanMoveTop)
        {
            shadowPosition = Vector3.Lerp(shadowPosition, new Vector3(shadowPosition.x, target.position.y + DistanceToGround, Zoom), Time.deltaTime * Camera_Speed);
            NewPosition = shadowPosition;
        
        }

    }

    // Update is called once per frame
    void Update()
    {

        CheackLimits();

        if (canMove)
        {
            CameraFollow();
            transform.position = NewPosition;
            //transform.position = new Vector3(target.position.x, target.position.y, Zoom);
        }
        else if (!canMove)
        {
            //PlaceCamera(OffsetX, OffsetY, PlaceCameraVelc);
            
        }

        //TargetPosition = Camera.main.WorldToScreenPoint(target.position);
        #region
        /*
        //right
        if ((TargetPosition.x > (Screen.width * 0.5f) + BoxCameraWith) && CanMoveRight)
        { 
            MovmentX = TargetPosition.x - ((Screen.width / 2) + BoxCameraWith);

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x + MovmentX, this.transform.position.y, this.transform.position.z), Time.deltaTime * X_velc);

        }


        //lefht
        if ((TargetPosition.x < (Screen.width * 0.5f) - BoxCameraWith) && CanMoveLefht)
        {
            MovmentX = TargetPosition.x - ((Screen.width / 2) - BoxCameraWith);

            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x + MovmentX, this.transform.position.y, this.transform.position.z), Time.deltaTime * X_velc);
        }

        //up
        if (TargetPosition.y > (Screen.height * 0.5f) + BoxCameraHeight)
        {
            MovmentY = TargetPosition.y + ((Screen.width / 2) - BoxCameraHeight);
        
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x , this.transform.position.y + MovmentY, this.transform.position.z), Time.deltaTime * Y_velc);
        }
        
        //down
       if (TargetPosition.y < (Screen.height * 0.5f) - BoxCameraHeight)
       {
          MovmentY = TargetPosition.y - ((Screen.width / 2) + BoxCameraHeight);
       
          this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + MovmentY, this.transform.position.z), Time.deltaTime * Y_velc);
       }
       */
        #endregion

    }

    




    public void CheackLimits()
    {
        #region CheckRight
        if (Physics.Raycast(target.position, Vector3.right, out CheackRight, Mathf.Infinity, 1 << 11, QueryTriggerInteraction.Collide))
        {
            
            if (CheackRight.transform.tag == "Limit")
            {
               if ((CheackRight.point - target.position).magnitude < DistanceToWalls)
               {
                   CanMoveRight = false;
               }
               else if ((CheackRight.point - target.position).magnitude > DistanceToWalls + MovmentX)
               {
                   CanMoveRight = true;
               }
            }
        }
        #endregion

        #region CheckLefht
        if (Physics.Raycast(target.position, Vector3.left, out CheackLefht, Mathf.Infinity, 1 << 11, QueryTriggerInteraction.Collide))
        {
            if (CheackLefht.transform.tag == "Limit")
            {
              if ((CheackLefht.point - target.position).magnitude < DistanceToWalls)
              {
                  CanMoveLefht = false;
              }
              else if ((CheackLefht.point - target.position).magnitude > DistanceToWalls + MovmentX)
              {
                  CanMoveLefht = true;
              }
            }
        }
        #endregion

        #region CheckTop
        if (Physics.Raycast(target.position, Vector3.up, out CheackTop, Mathf.Infinity, 1 << 11, QueryTriggerInteraction.Collide))
        {
            if (CheackTop.transform.tag == "Limit")
            {
                if ((CheackTop.point - target.position).magnitude < DistanceToCeiling )//BoundsCamera)
                {
                    CanMoveTop = false;
                }
                else if ((CheackTop.point - target.position).magnitude > DistanceToCeiling + 2 )//BoundsCamera )
                {
                    CanMoveTop = true;
                }
            }
        }
        #endregion

        #region CheackDown Not currently used
        if (Physics.Raycast(target.position, Vector3.down, out CheackDown, Mathf.Infinity, 1 << 11, QueryTriggerInteraction.Collide))
        {
            if (CheackDown.transform.tag == "Limit")
            {
                
                if ((CheackDown.point - target.position).magnitude < DistanceToGround)
                {
                    CanMoveDown = false;
                }
                else if ((CheackDown.point - target.position).magnitude < DistanceToWalls + MovmentY + DistanceToGround )
                {
                    CanMoveDown = true;
                }
            }
        }
        #endregion

    }

    

    /*public void PlaceCamera (float OffsetX,float OffsetY,float PlaceCameraVelc)
    {
        float timeStart = Time.deltaTime;
        float timeSinceStart = Time.deltaTime - timeStart;
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + OffsetX, target.position.y + OffsetY, transform.position.z), timeSinceStart / time);

        if ((timeSinceStart / time) >= 1)
        {
            canMove = true;
        }
        
    }*/

    public void CameraZoom(float newZoom, float VelcZoom)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, newZoom), VelcZoom);
    }

    public void CameraWithWindow( float X_Distance)
    {
        LI_right.position = new Vector3(LI_right.position.x + X_Distance, LI_right.position.y, LI_right.position.z);
        LI_lefht.position = new Vector3(LI_lefht.position.x - X_Distance, LI_lefht.position.y, LI_lefht.position.z);
    }

   /* public void CameraHeightWindow (float Y_Distance)
    {
        LI_top.position = new Vector3(LI_top.position.x , LI_top.position.y + Y_Distance, LI_top.position.z);
        LI_down.position = new Vector3(LI_down.position.x, LI_down.position.y - Y_Distance, LI_down.position.z);
    }
    */
   

}
