using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInputController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.5f;
    public Vector3 thumbstick;
    public Rigidbody rbody;
    public float flyspeed = 200f;
    private float horizontal, vertical;
    private Vector3 direction;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rbody.drag = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(-horizontal, 0.0f, -vertical);
        rbody.AddForce(direction * speed);
        Vector2 rawinput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) *speed;
        thumbstick = new Vector3(rawinput.x, 0f, rawinput.y);
        transform.position -= thumbstick;
        fly();

    }

    // Does not work yet
    void fly()
    {
            //if (!isflying)
            //{
            if (Input.GetKey(KeyCode.C))
            {
                rbody.useGravity = false;
                Vector3 up = new Vector3(0, 20f, 0);
                rbody.AddForce(up * speed);
                Debug.Log("The fly command");
            }
            // else if (Input.GetKeyDown(KeyCode.G))
            // {
            //rbody.useGravity = true;
            //}
            //isflying = false;
            //}
            //else
            //{
            // rbody.useGravity = true;
            //}
        
    }
}
