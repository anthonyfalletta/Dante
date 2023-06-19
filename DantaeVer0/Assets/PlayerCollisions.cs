using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy"){
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("Enemy Collision!");    
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy"){
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("Enemy Collision!");    
        }
    }
    
}
