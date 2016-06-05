using UnityEngine;
using System.Collections;

public class oscillationScript : MonoBehaviour
{

    public Vector3 from = new Vector3(0f, 135f, 0 );
    public Vector3 to = new Vector3(0f, 225f, 0);
    public float speed = 0.5f;

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed * 2.0f, 1.0f);
        transform.eulerAngles = Vector3.Lerp(from, to, t);
    }
}