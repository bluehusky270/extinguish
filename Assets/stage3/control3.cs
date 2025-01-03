using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control3 : MonoBehaviour
{
    Vector3 mv = Vector3.zero;
    float H;
    float V;
    float L;
    float speed = 100;
    float rotspeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        H = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        V = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y;
        L = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).x;
        mv.x = H;
        mv.z = V;
        transform.Translate(mv * speed * Time.deltaTime);
        if (Mathf.Abs(L) > 0.1f)
        {
            float ro = L * rotspeed * Time.deltaTime;
            transform.Rotate(0, ro, 0);
        }
    }
}
