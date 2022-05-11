using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileBehavior : MonoBehaviour
{
    PlayerMovement playerMovement;
    public GameObject projectile;
    public GameObject projectileUse;
    PlayerStateManager playerStateManager;
    PlayerShooting playerShooting;
     GameObject enemyObject;
     Rigidbody2D rbProjectile;

     Vector3 targetPosition;
     Vector3 playerPosition;


    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerShooting = GetComponent<PlayerShooting>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GameObject.Find("Enemy");
        //projectile.transform.eulerAngles = new Vector3(0,0,270);
        //projectile.transform.position = transform.position + (new Vector3(0,1,0).normalized * 0.1f);
        targetPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
        if (playerShooting.GetProjectileState("MovingForward"))
        {
            ProjectilePathStart(targetPosition);
        }
        else if (playerShooting.GetProjectileState("MovingBack"))
        {
            ProjectilePathBack(playerPosition);
        }
        else if (playerShooting.GetProjectileState("Return"))
        {
            ProjectilePathReturn(playerPosition);
        }
    }

    public void SetTarget(Vector3 targetPos)
    {
        targetPosition = targetPos;
        Debug.Log("Target Pos" + targetPos);
    }
    public void SetPlayerTarget(Vector3 playerPos)
    {
        playerPosition = playerPos;
    }
    public void InstantiateProjectile()
    {
        projectileUse = Instantiate(projectile, transform.position, transform.rotation);
        rbProjectile = projectileUse.GetComponent<Rigidbody2D>();
    }

    public void RemoveProjectile()
    {
        if (projectileUse != null)
        {
            //DestroyImmediate(projectileUse, true);
            Destroy(projectileUse);
            projectileUse = null;
            playerShooting.SetProjectileState("Held");
        }
    }

    public void ProjectileSphereMovement()
    {
        if(playerMovement.GetMovementValue().normalized != Vector2.zero)
        {       
            //Debug.Log("Movement is zero");
            //Debug.Log("Vector 3 Normalized" + new Vector3(playerMovement.GetMovementValue().x,playerMovement.GetMovementValue().y,0).normalized);

            float angle = Mathf.Atan2(playerMovement.GetMovementValue().normalized.y,playerMovement.GetMovementValue().normalized.x) * Mathf.Rad2Deg;
            projectileUse.transform.eulerAngles = new Vector3(0,0,angle);
            //projectile.transform.position = transform.position + (new Vector3(playerMovement.GetMovementValue().x,playerMovement.GetMovementValue().y,0).normalized * 0.1f);
            projectileUse.transform.position = transform.position + (new Vector3(-playerMovement.GetMovementValue().x,-playerMovement.GetMovementValue().y,0).normalized * 0.1f);
        }   
    }

    public void ProjectilePathStart(Vector3 targetPosition)
    {
       var collisionObject = projectileUse.GetComponent<ProjectileCollision>().infoCollision;

       if (collisionObject == null)
       {
           
       
        Debug.Log("Firing Projectile");
        //rbProjectile.MovePosition(transform.position)
        //float speed = 0.25f;
        //StartCoroutine(ExampleCoroutine());
        //projectileUse.transform.position = enemyObject.transform.position;

        //Add if player to targetPos distance is bigger than X then, else Remove Projectile
        projectileUse.transform.position = Vector2.MoveTowards(projectileUse.transform.position, targetPosition, 2.0f*Time.deltaTime);
        Debug.Log("TargetPosition: " + targetPosition);
        Debug.Log("Magnitude: " + (projectile.transform.position + targetPosition).magnitude);

        //Not Finding Proper Distance Between May Be Due to Precision or Something Else
        if (Vector2.Distance(projectileUse.transform.position,targetPosition) == 0)
        {
            StartCoroutine(ChangeProjectileStateMoveBackDelay());
            
        }
       }  
       else
       {
           Debug.Log("Collision occur with Player Projectile");
           playerShooting.SetProjectileState("Free");
       }
           
    }

    IEnumerator ChangeProjectileStateMoveBackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Couroutine called...");
        //Should Definelty Change to just stop this specific one, probably through boolean
        StopAllCoroutines();
        playerShooting.SetProjectileState("MovingBack");
        StopCoroutine(ChangeProjectileStateMoveBackDelay());
    }

    public void ProjectilePathBack(Vector3 playerPosition)
    {
        //if project is not null then...
        if (projectileUse != null)
        {
            var collisionObject = projectileUse.GetComponent<ProjectileCollision>().infoCollision;

            if (collisionObject == null)
            {
                projectileUse.transform.position = Vector2.MoveTowards(projectileUse.transform.position, playerPosition, 1.5f*Time.deltaTime);
                if (Vector3.Distance(projectileUse.transform.position,playerPosition) == 0)
                {
                    playerShooting.SetProjectileState("Free");
                }
            }
            else
            {
                Debug.Log("Collision occur with Player Projectile");
                playerShooting.SetProjectileState("Free");
            }
        }      
    }
    
    public void ProjectilePathReturn(Vector3 playerPosition)
    {
        if (projectileUse != null)
        {
            var collisionObject = projectileUse.GetComponent<ProjectileCollision>().infoCollision;

            if (collisionObject == null)
            {
                projectileUse.transform.position = Vector2.MoveTowards(projectileUse.transform.position, playerPosition, 0.5f*Time.deltaTime);      
            }
            else
            {
                Debug.Log("Collision occur with Player Projectile");
                playerShooting.SetProjectileState("Free");
            }
        }      
    }
    /*

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        
        Debug.Log("Moving");
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(50);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    */
}
