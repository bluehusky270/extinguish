using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class gamemanage3 : MonoBehaviour
{
    public FirePointManager firePointManager;
    public Text uiText;
    public GameObject player;
    public GameObject waterbutton;
    public GameObject clearscreen;
    public GameObject ap;
    public GameObject op;
    public GameObject guide;
    public List<GameObject> firepoints;
    private Dictionary<GameObject, GameObject>arrows=new Dictionary<GameObject, GameObject>();
    private int clearedfires = 0;
    public Transform vrcamera;
    private Vector3 buttonoffset = new Vector3(0, -0.5f, 2);
    private Vector3 paneloffset = new Vector3(0, 0, 35f);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Helicopter");
        waterbutton.SetActive(false);
        clearscreen.SetActive(false);
        op.gameObject.SetActive(false);
        if (vrcamera==null)
        {
            vrcamera = Camera.main.transform;
        }
        foreach(GameObject firepoint in firepoints)
        {
            if(firepoint!=null)
            {
                GameObject arrow=Instantiate(ap, firepoint.transform.position+Vector3.up*2f, Quaternion.identity);
                arrow.transform.rotation = Quaternion.Euler(0,0,-90);
                arrow.SetActive(true);
                arrows.Add(firepoint, arrow);
            }
        }
        if (firePointManager != null)
        {
            firePointManager.UpdateFirePointText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            op.gameObject.SetActive(true);
        }
        checkfirepoints();
        if(waterbutton.activeSelf)
        {
            updateuiposition(waterbutton, buttonoffset);
        }
        if(clearscreen.activeSelf)
        {
            updateuiposition(clearscreen, paneloffset);
        }
    }
    void checkfirepoints()
    {
        foreach (GameObject firepoint in firepoints)
        {
            if(firepoint!=null&&Vector3.Distance(player.transform.position,firepoint.transform.position)<70f)
            {
                waterbutton.SetActive(true);
                waterbutton.GetComponent<Button>().onClick.RemoveAllListeners();
                waterbutton.GetComponent<Button>().onClick.AddListener(()=>extinguishfire(firepoint));
                if(isbuttononepressed())
                {
                    extinguishfire(firepoint);
                }
                return;
            }
        }
        waterbutton.SetActive(false );
    }
    void extinguishfire(GameObject firepoint)
    {
        if (firepoint != null)
        {
            Destroy(firepoint);
            clearedfires++;
            if(firePointManager!=null)
            {
                firePointManager.firePointRemovedCount++;
                firePointManager.UpdateFirePointText();
            }
            if(arrows.ContainsKey(firepoint))
            {
                Destroy(arrows[firepoint]);
                arrows.Remove(firepoint);
            }
            waterbutton.SetActive(false);
            if (clearedfires == firepoints.Count)
            {
                Destroy(guide);
                clearscreen.SetActive(true);
            }
        }
    }
    bool isbuttononepressed()
    {
        InputDevice righthand=InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if(righthand.TryGetFeatureValue(CommonUsages.primaryButton, out bool ispressed))
        {
            return ispressed;
        }
        return false;
    }
    void updateuiposition(GameObject uielement, Vector3 offset)
    {
        if(vrcamera!=null)
        {
            uielement.transform.position=vrcamera.position+vrcamera.forward*offset.z+vrcamera.up*offset.y+vrcamera.right*offset.x;
            uielement.transform.rotation = Quaternion.LookRotation(uielement.transform.position - vrcamera.position);
        }
    }
    public void UpdateFirePointText()
    {
        int totalfirepoints = firepoints.Count;
        string dynamicText = $"¹° »Ñ¸° °÷ ({clearedfires}/{totalfirepoints})";
        if (uiText != null)
        {
            uiText.text = dynamicText;
        }
    }
}
