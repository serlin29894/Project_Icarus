using UnityEngine;
using System.Collections;

public class Nive3_Plataforma : MonoBehaviour {


    int boxes;

    public Transform Point1 , Point2, Point3;
  
    private Vector3 InitialPos;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        InitialPos = transform.position;
	
	}
	
	// Update is called once per frame
	void Update ()

    {


        if (boxes == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, InitialPos,Time.deltaTime * speed);
            
        }
        else if (boxes == 1)
        {
            if (transform.position != Point1.position)
            transform.position = Vector3.MoveTowards(transform.position, Point1.position, Time.deltaTime * speed);
            
        }
        else if (boxes == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point2.position, Time.deltaTime * speed);

        }
        else if (boxes == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point3.position, Time.deltaTime * speed);
        }


    }



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "caja")
        {
            Debug.Log("dasd");
            boxes += 1;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "caja")
        {
            boxes -= boxes;
        }
    }


}
