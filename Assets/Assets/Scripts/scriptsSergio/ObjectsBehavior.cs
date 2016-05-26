using UnityEngine;
using System.Collections;

public class ObjectsBehavior : MonoBehaviour {

    private Rigidbody MyRigidbody;
    public bool takeit = false;
    private Vector3 Pos;
    private bool TRue = false;

	// Use this for initialization
	void Start ()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        
	
	}
	
	
	void Update ()
    {
       if (takeit)
        {
            MyRigidbody.mass = 1;
        }
        else
        {
            MyRigidbody.mass = 99999;
            
        }
        
    }

    public void OnCollisionEnter(Collision col)

    {
        if (col.gameObject.layer == 8)
        {
            takeit = false;
        }


        if (takeit)
        {
            ContactPoint contact = col.contacts[0];
            //MyRigidbody.AddForce()
        }

    }


    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer ==  8)
        {
            takeit = true;

        }
    }

}
