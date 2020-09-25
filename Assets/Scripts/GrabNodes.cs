using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class GrabNodes : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public LineRenderer line;
    public GameObject parent;
    public bool objectClicked = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (objectClicked)
        {
            GameObject controller = GameObject.FindGameObjectWithTag("LController");
            LineRenderer line = controller.GetComponent<LineRenderer>();
            Vector3 cursorPoint = new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
            transform.position = cursorPosition;
        }
        */
        //MoveEdgesWithObject();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        objectClicked = true;
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
        objectClicked = false;
    }
    /*
    public void MoveEdgesWithObject()
    {
        if (objectClicked) {
            DataContainer data = GetComponent<DataContainer>();
            line = GetComponent<LineRenderer>();
             MedicalPlotter plot = new MedicalPlotter();
                if (data.parent != null && data.children.Count == 0)
                {
                    parent = plot.findNode(data.parent);
                    if (parent != null)
                    { 
                        line = parent.GetComponent<LineRenderer>();
                        //line.SetPosition(GetComponent<DataContainer>().linknum, transform.position);
                    }
                }
                else if (data.parent != null && data.children.Count > 0)
                {
                    parent = plot.findNode(data.parent);
                    if (parent != null)
                    {
                        line = parent.GetComponent<LineRenderer>();
                        //line.SetPosition(GetComponent<DataContainer>().linknum, transform.position);
                    }
                    line = GetComponent<LineRenderer>();
                    Vector3[] positions = new Vector3[data.children.Count * 2];
                    line.GetPositions(positions);
                    for (int i = 0; i < positions.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            //line.SetPosition(i, transform.position);
                        }
                    }


                }
                else if (data.parent == null && data.children.Count > 0)
                {
                    line = GetComponent<LineRenderer>();
                    Vector3[] positions = new Vector3[data.children.Count * 2];
                    line.GetPositions(positions);
                    for (int i = 0; i < positions.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            line.SetPosition(i, transform.position);
                        }
                    }
                }
        }   

    } 
    //Does not work yet
    public void GoToNode()
    {

        string nodeName = GameObject.Find("Canvas").GetComponentInChildren<InputField>().text;
        GameObject node = GameObject.Find(nodeName);
        Vector3 offset = new Vector3(1.0f, 0, -1.0f);
        if (node != null)
        {
            player.transform.position = node.transform.position + offset;
        }
        else
        {
            Debug.Log("Object could not be found");
        }
    }
    */
}
