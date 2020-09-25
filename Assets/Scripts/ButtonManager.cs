using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject sphere;
    public GameObject maincam;
    // Start is called before the first frame update
    void Start()
    {
        maincam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSphere()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 offset = new Vector3(0, 0, 1.5f);
        sphere.transform.position = maincam.transform.position + offset;
        //sphere.GetCompont
        //Instantiate(sphere, maincam.transform.position + offset, Quaternion.identity);
        sphere.AddComponent<MoveSpheres>();
        sphere.tag = "sphere";
        
        
    }
    public void LineSpheres()
    {
        float length = 20f;

        GameObject[] spheres = GameObject.FindGameObjectsWithTag("sphere");

        float spacing = length / spheres.Length;

        if (spheres != null && maincam != null)
        {
            Vector3 distance = maincam.transform.position;
            Vector3 newPos = new Vector3(distance.x - (0.5f * length), distance.y, distance.z + 14f);
            spheres[0].transform.position = newPos;
            Vector3 spherePositioning = spheres[0].transform.position;
            for (int i = 1; i < spheres.Length; i++)
            {
                spheres[i].transform.position = new Vector3(spherePositioning.x + spacing, spherePositioning.y, spherePositioning.z);
                spherePositioning = spheres[i].transform.position;
            }
        }

    }
    public void OrderSpheres()
    {
        float radius = 4.76f;
        GameObject[] spheres =  GameObject.FindGameObjectsWithTag("sphere");
        
        
        maincam = GameObject.Find("Main Camera");
        if (spheres != null && maincam != null)
        {
            Vector3 distance = maincam.transform.position;
            for (int i = 0; i < spheres.Length; i++)
            {
                float angle = i * Mathf.PI * 2f / spheres.Length;
                Vector3 newPos = new Vector3(distance.x + (Mathf.Cos(angle) * radius), distance.y, distance.z + (Mathf.Sin(angle) * radius));
                spheres[i].transform.position = newPos;
            }
        }
    }

}
