using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAvoidance : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject enemy;
    private Collider2D coll;

    private RaycastHit2D[] collisionResults;
    ContactFilter2D filter;
   
    int[] multiAngles = {0,15,-15,30,-30,45,-45,60,-60,75,-75,90,-90};
    
    Vector3 lastPos;
    float distance = 2.0f;


    Vector3 debugLocation;
    Vector3 debugMoveVec;


    
    // Start is called before the first frame update
    void Start()
    {
        coll = this.gameObject.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemy = this.gameObject;
        lastPos = gameObject.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
       //Declaring local variables for out variables from funciton
        Vector3 movementDirection;
        float movementAngle;
        CalculateMovementDirection(out movementDirection, out movementAngle);
            //Debug.Log("Movement Vector: " + movementDirection);
            //Debug.Log("Movement Vector Angle: " + movementAngle);
            //For debugging
            debugMoveVec = movementDirection;
        //CheckCollisionAhead(coll, movementDirection, distance);
        CheckMultipleCollisionsAhead(movementDirection);
    }

    void FixedUpdate(){
        //rb.MovePosition(rb.position + new Vector2(1.0f, 0.0f)* 3.0f * Time.fixedDeltaTime);
        //rb.velocity = new Vector2(3,3);
    }

    private void CalculateMovementDirection(out Vector3 movementDirection, out float movementAngle){
        movementDirection = (gameObject.transform.position-lastPos).normalized; 
        //Debug.Log("GameObject Vector: " + gameObject.transform.position);
        //Debug.Log("Last Pos Vector: " + lastPos);
        movementAngle = Vector2.Angle(Vector2.up, movementDirection);
        Vector3 cross = Vector3.Cross(Vector2.up,movementDirection);
        //Debug.Log(movementAngle);
        //Debug.Log(cross);
        if (cross.z < 0) movementAngle = -movementAngle;
        

        lastPos = gameObject.transform.position;
    }

    private bool CheckMultipleCollisionsAhead(Vector3 movementVector){
        for (int i=0; i<multiAngles.Length; i++){ 
            //Pass multiple direction vectors
            var rotatedVector = Quaternion.AngleAxis(multiAngles[i],Vector3.forward)*movementVector;

            //Check if movement vector going towards collision and change movement towards free angle if available
            if (CheckCollisionAhead(coll, rotatedVector, distance)==false)
            {
                Debug.Log("No hits for angle: " + multiAngles[i]);
                Debug.DrawLine(gameObject.transform.position, debugLocation, Color.green);
                    //Steer towards no collision angle
                    
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
        //Enable visual only during runtime
        if (gameObject != null && coll != null)
        {
        //Draw multiple rays of angle for direction
        for (int i=0; i<multiAngles.Length; i++){
            var rotatedVector = Quaternion.AngleAxis(multiAngles[i],Vector3.forward)*debugMoveVec;

            float angle = Vector2.Angle(Vector2.up, rotatedVector);
            Vector3 cross = Vector3.Cross(Vector3.up,rotatedVector);
                //Debug.Log(cross);
            if (cross.z > 0) angle = -angle;
                //Debug.Log("Angle: " + angle);

            float rad = Mathf.Deg2Rad*angle;
            Vector3 location = new Vector3(gameObject.transform.position.x+distance*Mathf.Sin(rad), gameObject.transform.position.y+distance*Mathf.Cos(rad),gameObject.transform.position.z);

        Debug.DrawLine(gameObject.transform.position, location, Color.white);
        }

        //Draw current collision box
        Gizmos.DrawWireCube(debugLocation, coll.bounds.size);
        }
    }
}


