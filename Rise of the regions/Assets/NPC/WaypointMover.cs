using System.Collections;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform waypointParent;
    public float moveSpeed = 2f;
    public float waitTime = 2f;
    public bool loopWayPoints = true;

    private Transform[] wayPoints;
    private int currentWaypointIndex;
    private bool isWaiting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wayPoints = new Transform[waypointParent.childCount];

        for(int i =0; i <waypointParent.childCount; i++)
        {
            wayPoints[i] = waypointParent.GetChild(i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0 || isWaiting)
        {
            return;
        }

        MoveToWaypoint();
        
    }

    void MoveToWaypoint()
    {
        Transform target = wayPoints[currentWaypointIndex];

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSecondsRealtime(waitTime);

        //If looping is enable: increment currentWaypointIndex and wrap around if needed
        //If not looping: increment currentWaypoints but dont exceed last waypoint
        currentWaypointIndex = loopWayPoints ? (currentWaypointIndex + 1) % wayPoints.Length : Mathf.Min(currentWaypointIndex + 1, wayPoints.Length -1);

        isWaiting = false;

    }

}
