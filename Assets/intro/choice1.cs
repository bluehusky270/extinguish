
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class choice1 : MonoBehaviour
{
    LineRenderer lr;
    GameObject rh;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        rh = GameObject.Find("RightHandAnchor");
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = rh.transform.position;
        ray.direction = rh.transform.forward;
        lr.SetPosition(0, ray.origin);
        lr.SetPosition(1, ray.origin + ray.direction * 1000);
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
                GameObject hitobject = hit.collider.gameObject;
                if (hitobject.CompareTag("UI"))
                {
                    ExecuteEvents.Execute<IPointerClickHandler>(
                        hitobject,
                        new PointerEventData(EventSystem.current),
                        ExecuteEvents.pointerClickHandler);
                }
            }
        }
    }
}
