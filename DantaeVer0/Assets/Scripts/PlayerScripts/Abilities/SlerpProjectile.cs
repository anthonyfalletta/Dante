using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpProjectile : MonoBehaviour
{
    [SerializeField] GameObject demoObject;
    GameObject playerObject;
    GameObject enemyObject;
    Vector3 slerpPos;
    Vector3 centerPos;

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;
    // The time at which the animation started.
    private float startTime;

    
    // Start is called before the first frame update
    void Start()
    {
        //slerpObject = GameObject.Find("DemoObject");
        playerObject = GameObject.Find("Player");
        enemyObject = GameObject.Find("Enemy");

        startTime = Time.time;
        ShortestProjectilePathToEnemyCurved();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         // The center of the arc
        Vector3 center = (transform.position + enemyObject.transform.position) * 0.5F;

        // move the center a bit downwards to make the arc vertical
        center -= new Vector3(0, 1, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = (transform.position) - center;
        Vector3 setRelCenter = enemyObject.transform.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Lerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
        */

    }

private void ShortestProjectilePathToEnemyCurved()
{
    float xDistance = enemyObject.transform.position.x - playerObject.transform.position.x;
    float yDistance = enemyObject.transform.position.y - playerObject.transform.position.y;

    float pointStepDistanceX = 0.2f;
    float numPoints = xDistance/pointStepDistanceX;
    float pointStepDistanceY = yDistance/numPoints;

    //Create Points
    Vector3[] points = new Vector3[Mathf.CeilToInt(numPoints)];

    for (int i=0; i<numPoints; i++)
    {
        points[i] = new Vector3(playerObject.transform.position.x + pointStepDistanceX * i, playerObject.transform.position.y + pointStepDistanceY*i,0);
        Instantiate(demoObject, points[i], Quaternion.identity);
    }



}


     IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, float centerOffset)
    {
        var centerPivot = (start + end) * 0.5f;
        var startRelativeCenter = start - centerPivot;
        var endRelativeCenter = end - centerPivot;

        var f = 1f/10;
        for (var i=0f; i<1+f; i++)
        {
            yield return Vector3.Slerp(startRelativeCenter, endRelativeCenter, i) + centerPivot;
        }
    }
}
