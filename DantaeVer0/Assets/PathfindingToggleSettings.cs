using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingToggleSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AstarPath.active.logPathResults = Pathfinding.PathLog.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
