using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control2 : MonoBehaviour
{
    Vector3 mv = Vector3.zero;
    float fr;
    float lr;
    float L;
    float speed = 100;
    float rotspeed = 20;
    float minY = 8f;
    float maxY = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fr = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        lr = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y;
        L = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).x;
        mv.x = fr;
        mv.z = lr;
        transform.Translate(mv * speed * Time.deltaTime);
        if(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            transform.position=transform.position + new Vector3(0,-speed*Time.deltaTime,0);
        }
        if (Mathf.Abs(L) > 0.1f)
        {
            float ro = L * rotspeed * Time.deltaTime;
            transform.Rotate(0, ro, 0);
        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
    }
}
