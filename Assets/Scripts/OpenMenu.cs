using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class OpenMenu : MonoBehaviour
{
    public static bool IsOpen;
    public Canvas GameMenu;
    public Button rotate;
    public Button move;
    public SliderJoint2D scale;
    
    // Start is called before the first frame update
    void Start()
    {
        GameMenu = GameMenu.GetComponent<Canvas>();
        
        GameMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameMenu.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameMenu.enabled = false;
        }

    }
}
