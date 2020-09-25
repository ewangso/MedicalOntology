using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    private Vector2 direction;
     public Rigidbody rbody;
    public float speed = 800f;
    private float vertical, horizontal;
    public Vector3 movement;
    bool isflying = false;
    private float MoveScale = 1.0f;
    private float SimulationRate = 60f;
    private Vector3 MoveThrottle = Vector3.zero;
    public float BackAndSideDampen = 10f;
    private float MoveScaleMultiplier = 1.0f;
    public float Acceleration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rbody.drag = 1.5f;
        rbody.useGravity = true;
        rbody.mass = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        MoveScale = 3.0f;
        bool moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool moveUp = Input.GetKey(KeyCode.M);
        bool moveDown = Input.GetKey(KeyCode.Tab);

        if ((moveForward && moveLeft) || (moveForward && moveRight) ||
                (moveBack && moveLeft) || (moveBack && moveRight))
            MoveScale = 0.70710678f;

        MoveScale *= SimulationRate * Time.deltaTime;

        float moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;

        Quaternion ort = transform.rotation;
        Vector3 ortEuler = ort.eulerAngles;
        ortEuler.z = ortEuler.x = 0f;
        ort = Quaternion.Euler(ortEuler);

        if (moveForward)
            MoveThrottle += ort * (transform.lossyScale.z * moveInfluence * Vector3.up);
        if (moveBack)
            MoveThrottle += ort * (transform.lossyScale.z * moveInfluence * BackAndSideDampen * Vector3.back);
        if (moveLeft)
            MoveThrottle += ort * (transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.left);
        if (moveRight)
            MoveThrottle += ort * (transform.lossyScale.x * moveInfluence * BackAndSideDampen * Vector3.right);
        if (moveUp)
           MoveThrottle += ort * (transform.lossyScale.y * moveInfluence * Vector3.up);
        if (moveDown)
            MoveThrottle += ort * (transform.lossyScale.y * moveInfluence * Vector3.down);


        moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;

        //Player object moves based on user input
        /*
        Quaternion rotate = transform.rotation;
        direction = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (direction.y > 0.0f)
        {
            movement += rotate * (direction.y * Vector3.forward * transform.lossyScale.x);
        }
        if (direction.y < 0.0f)
        {
            movement += rotate * (Mathf.Abs(direction.y) * transform.lossyScale.x * Vector3.back);
        }
        if (direction.x > 0.0f)
        {
            movement += rotate * (direction.x * transform.lossyScale.z * Vector3.right);
        }
        if (direction.x < 0.0f)
        {
            movement += rotate * (Mathf.Abs(direction.x) * transform.lossyScale.z * Vector3.left);
        }
        */

        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");
        //direction = new Vector3(horizontal, 0.0f, vertical);
        //transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, 0, Camera.main.transform.eulerAngles.z);
        //transform.rotation = Quaternion.LookRotation(direction);
        transform.position += new Vector3(direction.x, direction.y, 0.0f) * speed;
        rbody.AddForce(movement * speed);
        /*
        bool firstPress = Input.GetKeyDown(KeyCode.C);
        float firstPressTime = Time.time;
        bool secondPress = Input.GetKeyDown(KeyCode.C);
        float secondPressTime = Time.time;
        if (firstPress && secondPress && secondPressTime - firstPressTime < 0.5)
        {
            isflying = true;
            PlayerFly();
             }
             */
        if (Input.GetKeyDown(KeyCode.C))
            {
            isflying = true;
            PlayerFly();
        }

        }
        void PlayerFly()
        {
            //if (!isflying)
            //{
            Debug.Log("PlayerFly is called");
            if (Input.GetKey(KeyCode.C))
            {
                 rbody.useGravity = false;
            Vector3 up = new Vector3(0, 2033333f, 0);
                 rbody.AddForce(up * speed);
                Debug.Log("The fly command");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
            rbody.useGravity = true;
            }
            //isflying = false;
            //}
            //else
            //{
            // rbody.useGravity = true;
            //}
        }

    
}