using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Camera main;
    public Camera FPS;
    public Camera sub;


    // Start is called before the first frame update
    void Start()
    {
        main.enabled = true;
        FPS.enabled = false;
        sub.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            main.enabled = false;
            FPS.enabled = false;
            sub.enabled = true;


        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            main.enabled = true;
            FPS.enabled = false;
            sub.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            main.enabled = false;
            FPS.enabled = true;
            sub.enabled = false;
        }
    }
}
