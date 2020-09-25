using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{

    public MedicalPlotter plotter;
    public DataContainer container;
    public GameObject thisNode;
    public GameObject firstChild;
    List<string> children;
    Rigidbody nodeObject;
    Vector3 vel;
    GameObject parentObj;

    void Start()
    {
        children = thisNode.GetComponent<DataContainer>().children;

        if(children.Count > 0)
        {
            firstChild = findNode(children[children.Count / 2]);
        }
        
        nodeObject = GetComponent<Rigidbody>();
        vel = nodeObject.velocity;
        parentObj = findNode(thisNode.GetComponent<DataContainer>().parent);
    }
    
    public void OnTriggerStay(Collider collision)
    {

        Vector3 direction = collision.transform.position - transform.position;

        if ((transform.position.x - collision.transform.position.x) < 0)
        {
            //print("hit left");
            transform.position = new Vector3(
                transform.position.x - 0.25f,
                transform.position.y,
                transform.position.z
            );
            return;
        }
        else if ((transform.position.x - collision.transform.position.x) > 0)
        {
            //print("hit right");
            transform.position = new Vector3(
                transform.position.x + 0.25f,
                transform.position.y,
                transform.position.z
            );
            return;
        }
        else if ((transform.position.x - collision.transform.position.x) == 0)
        {
            transform.position = new Vector3(
                transform.position.x + 0.25f,
                transform.position.y,
                transform.position.z
            );
            return;
        }
    }

    
    void FixedUpdate()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (vel.magnitude == 0)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100))
            {
                if(hit.transform.tag == "Node")
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y,
                        transform.position.z
                    );
                }
            }
        }
        
        if (firstChild != null)
        {
            transform.position = new Vector3(
                firstChild.transform.position.x,
                transform.position.y,
                firstChild.transform.position.z
            );
        }



    }

    public GameObject findNode(string nodeName)
    {
        GameObject obj = GameObject.Find(nodeName);
        if (obj != null)
            return obj;
        else
            return null;
    }


}
