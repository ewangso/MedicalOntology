using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

/*
 * This CSV reader is specifically to populate the node information
 */
public class NewCSVReader : MonoBehaviour
{
    //this variable is supposed to take the input file - CVDO
    //public string CDVO;


    // List for holding data from CSV reader
    //private List<Dictionary<string, object>> fileContent;

    List<Info> inform = new List<Info>();
    Text instruction;

    void Start()
    {
        instruction = GetComponent<Text>();

        TextAsset CVDO = Resources.Load<TextAsset>("CVDO");

        string[] rows = CVDO.text.Split(new char[] { '\n' });

        //Debug.Log(lines.Length);

        for (int i = 1; i < rows.Length - 1; i++)
        {
            string[] column = rows[i].Split(new char[] { ',' });
           
            if(column[1] != "")
            {
                Info information = new Info();
                if (column.Length >= 38)
                {
                    information.classID = column[0];
                    information.description = column[1];
                    information.parents = column[7];

                    inform.Add(information);
                }
            }
        }
        

        foreach (Info i in inform)
        {

            Debug.Log(i.classID  + ", " + i.description + ", " + i.parents);
        }
        //fileContent = CSVReader.Read("CDVO"); // set the contents of the file to the list using the read function
        //Debug.Log(fileContent); //print out the content to the console
    }

    void Update()
    {
        
    }
}
