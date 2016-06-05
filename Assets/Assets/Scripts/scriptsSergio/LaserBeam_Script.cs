using UnityEngine;
using System.Collections;

public class LaserBeam_Script : MonoBehaviour {

   
    private LineRenderer LinerRenderer_reference;


    private Vector3 StartPos;
    private Vector3 EndPos;

    public float speed;

    public PlayerControler Plc_Reference;
  


    

    // Use this for initialization
    void Start ()
    {
        LinerRenderer_reference = this.GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update ()

    {
        if (Plc_Reference.ReturnHaveObject())
        {
            StartPos = transform.position;
            EndPos = Plc_Reference.ObjectPos();

            LinerRenderer_reference.SetPosition(0, StartPos);
            LinerRenderer_reference.SetPosition(1, EndPos);

            LinerRenderer_reference.materials[0].SetFloat("_AddTex", Time.time * speed );
           
        }
        
        

    }
}
