/*using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
/// <summary>
/// Detailed Description of this
/// </summary>
public class GetInfo : MonoBehaviour
{


    //This is for button press variables only
    float timeCurrent;
    float timeAtButtonDown;
    float timeAtButtonUp;
    float timeButtonHeld = 0;
    bool draggable = false;


    Text txt;
    string classID;
    string description;
    string parents;
    string classTitle;
    List<string> children; //this is the temperory string to take in the value of edited classID

    GameObject classti, classid, descrip, paren, childre; //classti is class title which needs to be displayed on top of the canvas, but it will extract the substring from classID
    GameObject hitObject;

    public GameObject nodeMenu;

    public void actionAfterHitOnNode()
    {
        MedicalPlotter plot = new MedicalPlotter();

        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hitInfo.transform != null)
        {
            hitObject = plot.findNode(hitInfo.transform.gameObject.name);

            if (hit && hitObject != null && hitObject.name != "Floor")
            {
                Debug.Log("Hit " + hitObject.name);
                hitObject.GetComponent<GrabNodes>().objectClicked = true;
                Instantiate(nodeMenu, new Vector3(0, 0, 0), transform.rotation); 
                hitObject.transform.SetParent(null);

                classID = hitObject.transform.gameObject.GetComponent<DataContainer>().id; //gets the public class id
                description = hitObject.transform.gameObject.GetComponent<DataContainer>().description; //gets the public description
                parents = hitObject.transform.gameObject.GetComponent<DataContainer>().parent; //gets the public parents
                children = hitObject.transform.gameObject.GetComponent<DataContainer>().children;

                Debug.Log(classID);
                Debug.Log(description);
                Debug.Log(parents);

                if (classID.StartsWith("http:"))
                {
                    //remove "http..." part of the classID string
                    classTitle = classID.Substring(31).TrimStart();
                }

                classti = GameObject.Find("PreferedName");
                classid = GameObject.Find("ClassID");
                descrip = GameObject.Find("Description");
                paren = GameObject.Find("Parents");
                childre = GameObject.Find("Children");
                //this bottom one if for the title

                txt = classti.GetComponent<Text>();
                txt.text = classTitle;

                txt = classid.GetComponent<Text>();
                txt.text = classID;

                txt = descrip.GetComponent<Text>();
                txt.text = description;

                txt = paren.GetComponent<Text>();
                txt.text = parents;

                txt = childre.GetComponent<Text>();

                if(children.Count > 0)
                {
                    //txt = children[0];
                    for (int i = 0; i < children.Count; i++)
                    {
                        txt.text += children[i];
                        txt.text += "\n";
                    }
                }                    
            }
        }

        //return classID;
    }

    void OnMouseDown()
    {
        timeAtButtonDown = timeCurrent;
        //Debug.Log("Time button pressed" + timeAtButtonDown);
    }

    void OnMouseUp()
    {
        timeAtButtonUp = timeCurrent;
        //Debug.Log("Time button released" + timeAtButtonUp);

        timeButtonHeld = (timeAtButtonUp - timeAtButtonDown);
        //Debug.Log("TimeButtonHeld = " + timeButtonHeld);

    }

    // Update is called once per frame
    void Update()
    {
        timeCurrent = Time.fixedTime;
        OnMouseDown();
        OnMouseUp();

        if (Input.GetMouseButtonUp(0) && timeButtonHeld < 2)
        {
            actionAfterHitOnNode();
        }
    }
}