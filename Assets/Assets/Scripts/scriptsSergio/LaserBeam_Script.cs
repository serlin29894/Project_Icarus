using UnityEngine;
using System.Collections;

public class LaserBeam_Script : MonoBehaviour {

    private float MinLenght;
    Vector3 NowPos = Vector3.zero;
    private LineRenderer LinerRenderer_reference;


    private Vector3[] F_Vec;
    private float BlockLen;
    private int LRSize;
    private float NowLength;
    private float MaxLenght;

    public PlayerControler Plc_Reference;
    public float Width;
    public Color raycolor;

    public float scale;
    public float AddLength = 0.1f;

    // Use this for initialization
    void Start ()
    {


        
        LRSize = 16;
        NowLength = 0.0f;
        LinerRenderer_reference = this.GetComponent<LineRenderer>();
       
        F_Vec = new Vector3[LRSize + 1];
        
        for (int i = 0; i < LRSize + 1; i++)
        {
            F_Vec[i] = transform.forward;
        }



        // LinerRenderer_reference = GetComponent<LineRenderer>();
        //
        // points = 16;
        //
        // F_Vec= new Vector3[points + 1];
        //
        // for (int i = 0; i < points + 1; i++)
        // {
        //     F_Vec[i] = transform.forward;
        // }
    }
	
	// Update is called once per frame
	void Update ()

    {
        if (Plc_Reference.ReturnHaveObject())
        {
            NowLength = Mathf.Min(1.0f, NowLength + AddLength);

            Vector3 NowPos = Vector3.zero;

            LinerRenderer_reference.SetWidth(Width * scale, Width * scale);
            LinerRenderer_reference.SetColors(raycolor, raycolor);
            
            for (int i = LRSize - 1; i > 0; i--)
            {
                F_Vec[i] = F_Vec[i - 1];
            }
            F_Vec[0] = transform.forward;
            F_Vec[LRSize] = F_Vec[LRSize - 1];
            float BlockLen = MaxLenght / LRSize;

            for (int i = 0; i < LRSize; i++)
            {
                NowPos = transform.position;
                for (int j = 0; j < i; j++)
                {
                    NowPos += F_Vec[j] * BlockLen;
                }
                LinerRenderer_reference.SetVertexCount(i+1);
                LinerRenderer_reference.SetPosition(i, NowPos);
            }






            /* NowPos = Plc_Reference.ObjectPos();

             LinerRenderer_reference.SetWidth(With, With/2);
             LinerRenderer_reference.SetColors(raycolor, raycolor);

             MaxLenght = (Plc_Reference.ObjectPos() - transform.position).magnitude;

             for (int i = points - 1; i > 0; i--)
             {
                 F_Vec[i] = F_Vec[i - 1];
             }
             F_Vec[0] = transform.forward;
             F_Vec[points] = F_Vec[points - 1];

             float BlockLen = MaxLenght / points;

             for (int i = 0; i < points; i++)
             {
                 NowPos = transform.position;
                 for (int j = 0; j < i; j++)
                 {
                     NowPos += F_Vec[j] * BlockLen;

                 }

                 LinerRenderer_reference.SetPosition(i, NowPos);
             }*/
        }
        
        

    }
}
