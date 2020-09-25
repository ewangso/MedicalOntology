using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDescendants : MonoBehaviour
{
    DataContainer data;
    MedicalPlotter plot;
    List<string> children;
    // Start is called before the first frame update
    void Start()
    {
        plot = new MedicalPlotter();
        children = gameObject.GetComponent<DataContainer>().children;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
