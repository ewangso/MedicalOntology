using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlller : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float mousex, mousey;
    private Transform t;
    public float horspeed;
    public float virtspeed;
    public GameObject hitObject;
    public GameObject nodeMenu;
    // Start is called before the first frame update
    void Start()
    {
        nodeMenu = GameObject.Find("NodeMenu");
        //nodeMenu.SetActive(false);
        offset = new Vector3(0.0f, 0.5f, 0.5f);
        player = GameObject.FindGameObjectWithTag("Player");
        t = GetComponent<Transform>();
        horspeed = 2.0f;
        virtspeed = 2.0f;
        Debug.Log("Main Camera Position" + Camera.main.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position;
        mousex += horspeed * Input.GetAxis("Mouse X");
        mousey -= virtspeed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(mousey, mousex, 0.0f);
        
        RaycastHit hit_vr;
        /*MedicalPlotter plot = new MedicalPlotter();
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hitInfo.transform != null)
            {
                hitObject = plot.findNode(hitInfo.transform.gameObject.name);
                if (hit && hitObject != null)
                {
                    //Debug.Log("Hit " + hitObject.name);
                    //hitObject.GetComponent<GrabNodes>().objectClicked = true;
                    //hitObject.transform.GetChild(0).gameObject.SetActive(true); //why does this not recognize nodemenu and turn it on??
                    //nodeMenu.SetActive(true);
                }
                if (hitObject.tag == "NodeTag")
                {
                    //GameObject.Find("Floor").SetActive(false);
                    //Instantiate(nodeMenu, new Vector3(0,0,0), Quaternion.identity);

                }
            }
        }*/
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTrackedRemote);
        //transform.position = GameObject.FindGameObjectWithTag("LController").transform.position;
        if (Physics.Raycast(transform.position, transform.forward, out hit_vr))
        {
            if(hit_vr.collider != null  && hit_vr.collider.gameObject.layer == 9)
            {
                if (hitObject != hit_vr.collider.gameObject)
                {
                    hitObject = hit_vr.collider.gameObject;
                }
                Debug.Log("Hit" + hitObject.name);
                hitObject.GetComponent<GrabNodes>().objectClicked = true;
                LineRenderer line = GetComponent<LineRenderer>();
                if (line != null)
                {
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, hitObject.transform.position);
                }
            }
        }

    }
}
