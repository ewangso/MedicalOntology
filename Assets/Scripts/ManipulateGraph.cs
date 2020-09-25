using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulateGraph : MonoBehaviour
{

    public Slider slider1;
    private float sliderLastX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        slider1 = GameObject.Find("RotateVSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion localRotation = Quaternion.Euler(slider1.value - sliderLastX, 0f, 0f);
        transform.rotation = transform.rotation * localRotation;
        sliderLastX = slider1.value;
    }
    public void slideScale(float t)
    {
        transform.localScale = new Vector3(t, t, t);
    }

    public void moveDown()
    {

        transform.localPosition += new Vector3(0, -1, 0);
    }

    public void moveUp()
    {

        transform.localPosition += new Vector3(0, 1, 0);
    }

    public void moveLeft()
    {

        transform.localPosition += new Vector3(-1, 0, 0);
    }

    public void moveRight()
    {

        transform.localPosition += new Vector3(1, 0, 0);
    }

    public void moveForward()
    {

        transform.localPosition += new Vector3(0, 0, -1);
    }

    public void moveBack()
    {

        transform.localPosition += new Vector3(0, 0, 1);
    }

    public void sliderRotate()
    {
        //transform.localRotation = Quaternion.Euler(0, 0, slider1.value);
        
    }
}
