using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAvoidance : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private RaycastHit2D[] collisionResults;
    private GameObject enemy;
    Vector3 debugLocation;
    float distance = 2.0f;
    int[] multiAngles = {0,15,-15,30,-30,45,-45,60,-60,75,-75,90,-90};
    Vector3[] vectorAngle = new Vector3[13];
    ContactFilter2D filter;

    Vector3 lastPos;
    Vector3 debugMoveVec;
    float debugAngle;
    //Vector3 movementDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = this.gameObject.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemy = this.gameObject;
        lastPos = gameObject.transform.position;

        for (int i=0; i<multiAngles.Length; i++){
            float radAngle = multiAngles[i] * Mathf.Deg2Rad;
            vectorAngle[i] = new Vector3(Mathf.Sin(radAngle), Mathf.Cos(radAngle),0);
            //Debug.Log("Vectors " + i + " " + vectorAngle[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //CheckCollisionAhead(coll, Vector2.up, distance);

        
        Vector3 movementDirection;
        float movementAngle;
        CalculateMovementDirection(out movementDirection, out movementAngle);
        Debug.Log("Movement Vector: " + movementDirection);
        Debug.Log("Movement Vector Angle: " + movementAngle);
        debugMoveVec = movementDirection;
        //CheckCollisionAhead(coll, movementDirection, distance);
        CheckMultipleCollisionsAhead(movementDirection);
    }

    private bool Boxcasting(){
       
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, distance, Vector2.up, LayerMask.GetMask("Unwalkable", "Enemy"));
            Color rayColor;
            if(raycastHit.collider != null){
                rayColor = Color.white;
            }else{
                rayColor = Color.red;
            }
            Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0), Vector3.down * (coll.bounds.extents.y), rayColor);
            Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector3.down * (coll.bounds.extents.y), rayColor);
            Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0), Vector3.up * (coll.bounds.extents.y), rayColor);
            Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector3.up * (coll.bounds.extents.y), rayColor);
            Debug.DrawRay(coll.bounds.center + new Vector3(-coll.bounds.extents.x, coll.bounds.extents.y), Vector3.right * (coll.bounds.extents.x), rayColor);
            Debug.DrawRay(coll.bounds.center + new Vector3(-coll.bounds.extents.x, -coll.bounds.extents.y), Vector3.right * (coll.bounds.extents.x), rayColor);
            Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, coll.bounds.extents.y), Vector3.left * (coll.bounds.extents.x), rayColor);
            Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, -coll.bounds.extents.y), Vector3.left * (coll.bounds.extents.x), rayColor);
            Debug.Log("Ray collided: " + raycastHit.collider.name);
            return raycastHit.collider != null;
        
    }

    private void CalculateMovementDirection(out Vector3 movementDirection, out float movementAngle){
        movementDirection = (gameObject.transform.position-lastPos).normalized; 
        Debug.Log("GameObject Vector: " + gameObject.transform.position);
        Debug.Log("Last Pos Vector: " + lastPos);
        movementAngle = Vector2.Angle(Vector2.up, movementDirection);
        Vector3 cross = Vector3.Cross(Vector2.up,movementDirection);
        //Debug.Log(cross);
        if (cross.z < 0) movementAngle = -movementAngle;
        

        lastPos = gameObject.transform.position;

        //return (moveVec, angle);
    }

    private bool CheckMultipleCollisionsAhead(Vector3 movementVector){
        for (int i=0; i<multiAngles.Length; i++){
            //Need to have movement vector and angle change calculated instead of gameobject object oriented with Vector.up
            //float radAngle = multiAngles[i] * Mathf.Deg2Rad;
             //Vector3 direction = new Vector3(movementVector.x+Mathf.Sin(radAngle), movementVector.y+Mathf.Cos(radAngle),movementVector.z);
            
            var rotatedVector = Quaternion.AngleAxis(multiAngles[i],Vector3.forward)*movementVector
            ;
            if (CheckCollisionAhead(coll, rotatedVector, distance)==false)
            {
                Debug.Log("No hits for angle: " + multiAngles[i]);
                Debug.DrawLine(gameObject.transform.position, debugLocation, Color.green);
                    //float rad = Mathf.Deg2Rad*multiAngles[i];
                    //location = new Vector3(gameObject.transform.position.x+distance*Mathf.Sin(rad), gameObject.transform.position.y+distance*Mathf.Cos(rad),gameObject.transform.position.z);

                    //Steer towards no collision angle
                    //Check if movement vector going towards collision and change if so
                return false;
            }
            
        }
        //Reduce speed until opening occurs
        return true;
    }

    private bool CheckCollisionAhead(Collider2D movableCollider, Vector2 direction, float distance){
        if (movableCollider != null)
        {
            //Setup data structures within function
            RaycastHit2D[] hits = new RaycastHit2D[10];
            ContactFilter2D filter = new ContactFilter2D(){};
            filter.SetLayerMask(LayerMask.GetMask("Unwalkable", "Enemy"));
            filter.useLayerMask = true;
            
           //////////////////* Values for Visual Debugging *///////////////////////////
            float angle = Vector2.Angle(Vector2.up, direction);
            Vector3 cross = Vector3.Cross(Vector3.up,direction);
                //Debug.Log(cross);
            if (cross.z > 0) angle = -angle;
                //Debug.Log("Angle: " + angle);

            float rad = Mathf.Deg2Rad*angle;
            debugLocation = new Vector3(gameObject.transform.position.x+distance*Mathf.Sin(rad), gameObject.transform.position.y+distance*Mathf.Cos(rad),gameObject.transform.position.z);
            //*************************************************************************/


            int numHits = movableCollider.Cast(direction, filter, hits, distance);
            for (int i=0; i < numHits; i++)
            {
                if (!hits[i].collider.isTrigger){
                    Debug.Log("Collision Avoidance: True");
                    Debug.Log(multiAngles[i] + " " + hits[i].collider.name);
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos() {
      
        if (gameObject != null && coll != null)
        {
            //Draw multiple rays of angle check
        for (int i=0; i<multiAngles.Length; i++){
            /*
            float radAngle1 = multiAngles[i] * Mathf.Deg2Rad;
            float radAngle2 = Vector2.Angle(Vector2.up, debugMoveVec);

            Vector3 direction = new Vector3(debugMoveVec.x+Mathf.Sin(radAngle), debugMoveVec.y+Mathf.Cos(radAngle),debugMoveVec.z);
            float angle = Vector2.Angle(Vector2.up, direction);
            Vector3 cross = Vector3.Cross(Vector3.up,direction);
                //Debug.Log(cross);
            if (cross.z > 0) angle = -angle;
                //Debug.Log("Angle: " + angle);

            float rad = Mathf.Deg2Rad*angle;
            Vector3 debugDirection = new Vector3(gameObject.transform.position.x+distance*Mathf.Sin(rad), gameObject.transform.position.y+distance*Mathf.Cos(rad),gameObject.transform.position.z);
        */
        //Debug.DrawLine(gameObject.transform.position, , Color.white);
        }

        //Draw current collision check
        Gizmos.DrawWireCube(debugLocation, coll.bounds.size);
        }
    }
}


