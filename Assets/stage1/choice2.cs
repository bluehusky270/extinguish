using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class choice2 : MonoBehaviour
{
    LineRenderer lr;
    GameObject rh;
    Ray ray;
    RaycastHit hit;
    public GraphicRaycaster ur;
    public EventSystem es;
    // Start is called before the first frame update
    void Start()
    {
        rh = GameObject.Find("RightHandAnchor");
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.sortingLayerName = "UI";
        lr.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = rh.transform.position;
        ray.direction = rh.transform.forward;
        lr.SetPosition(0, ray.origin);
        lr.SetPosition(1, ray.origin + ray.direction * 15);
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (checkuiraycast())
            {
                return;
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
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
    private bool checkuiraycast()
    {
        PointerEventData pointdata = new PointerEventData(es)
        {
            position = new Vector2(Screen.width / 2, Screen.height/2)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        ur.Raycast(pointdata, results);
        if (results.Count > 0)
        {
            foreach (var result in results)
            {
                if(result.gameObject.activeInHierarchy)
                {
                    ExecuteEvents.Execute<IPointerClickHandler>(result.gameObject, pointdata, ExecuteEvents.pointerClickHandler);
                }
            }
            return true;
        }
        return false;
    }
}
