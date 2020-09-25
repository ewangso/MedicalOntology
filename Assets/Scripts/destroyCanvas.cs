using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCanvas : MonoBehaviour
{
    public GameObject nodeMenu;

    public void destory()
    {
        /*GetInfo info = new GetInfo(); 
        info.setBool(false);*/
        Destroy(nodeMenu);
    }
}
