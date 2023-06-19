using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(240, 135, true, 60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
