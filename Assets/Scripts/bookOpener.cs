using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static OVRInput;

public class bookOpener : MonoBehaviour
{
    //this is to attach the image we want to display
    //public GameObject Panel;
    public GameObject Prefab;
    public Canvas NodeDisplay;


    /*private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }*/

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100.0f))
            {
                if (hitInfo.collider.gameObject.tag == "NodeTag")
                {
                    //NodeDisplay.gameObject.SetActive(true);
                    Debug.Log("tag");
                }
            }
        }
    }

    //this fucitons can allow us to open and close the image
    /*public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }

    }*/

}
