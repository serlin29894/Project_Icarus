using UnityEngine;
using System.Collections;

public class ZoneValues : MonoBehaviour {


    private CamaraControl CameraReference;

    

    public float Zone_DistanceToWall;
    public float Zone_DistanceToGround;
    public float Zone_DistanceToCeiling;
    //public float Zone_BoxCameraWith;
    //public float Zone_BoxCameraHeight;
    public float Zone_CameraZoom;
    public float Zone_OffsetX;
    public float Zone_OffsetY;
    public float Zone_PlaceCameraVelc;
    public float Zone_ZoomVelc;

    private bool IntheZone;
    private bool islerping;
    private bool Startlerping;
    private float _timeStartedLerping;
    void Start ()
    {
        CameraReference = Camera.main.GetComponent<CamaraControl>();
    }

    void Update()
    {
      if (IntheZone)
        {
            Startlerping = true;
            IntheZone = false;
        }
        else
        {
            Startlerping = false;
        }
        

      if (islerping)
        {

            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / Zone_ZoomVelc;
            
            //CameraReference.transform.position = Vector3.Lerp(CameraReference.transform.position, new Vector3(CameraReference.transform.position.x, CameraReference.transform.position.y, Zone_CameraZoom), percentageComplete);

            CameraReference.Zoom = Mathf.Lerp(CameraReference.Zoom, Zone_CameraZoom, Time.deltaTime * Zone_ZoomVelc);

            if (percentageComplete >= 1)
            {

                islerping = false;
            }

        }

      if (Startlerping)
        {

            islerping = true;
            _timeStartedLerping = Time.time;

            Startlerping = false;
        }
    }
	
   public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            ChangeCameraValues();
            IntheZone = true;
            //CameraReference.canMove = false;
        }
    }

    public void OnTrggerExit (Collider col)
    {
        if (col.transform.tag == "Player")
        {
            IntheZone = false;
        }

    }

    public void ChangeCameraValues()
    {
        CameraReference.DistanceToWalls = Zone_DistanceToWall;
        //CameraReference.DistanceToCeiling = Zone_DistanceToCeiling;
        CameraReference.DistanceToGround = Zone_DistanceToGround;
        //CameraReference.OffsetX = Zone_OffsetX;
        //CameraReference.OffsetY = Zone_OffsetY;
        CameraReference.newZoom = Zone_CameraZoom;
        //CameraReference.PlaceCameraVelc = Zone_PlaceCameraVelc;
        CameraReference.ZoomVelc = Zone_ZoomVelc;

    }



}
