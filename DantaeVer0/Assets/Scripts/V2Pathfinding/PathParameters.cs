using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

public struct PathParameters : IComponentData{
    public int2 startPosition;
    public int2 endPosition;
}
    
