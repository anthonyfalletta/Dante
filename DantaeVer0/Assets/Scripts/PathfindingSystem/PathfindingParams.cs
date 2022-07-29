using Unity.Mathematics;
using Unity.Entities;

public struct PathfindingParams : IComponentData
{
    public int2 startPosition;
    public int2 endPosition;
}
