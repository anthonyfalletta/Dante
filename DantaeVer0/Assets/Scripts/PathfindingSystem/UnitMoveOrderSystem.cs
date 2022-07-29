using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.InputSystem;
public class UnitMoveOrderSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame){
            Entities.ForEach((Entity entity, ref Translation translation) => {
                //Add Pathfinding Params 
                EntityManager.AddComponentData(entity, new PathfindingParams{
                    startPosition = new int2(0,0),
                    endPosition = new int2(4,0)
                });
            });
        }
    }
}
