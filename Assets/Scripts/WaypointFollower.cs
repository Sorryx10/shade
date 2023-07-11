using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWatpointIndex = 0;

    [SerializeField] private float speed = 2f;
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWatpointIndex].transform.position, transform.position) < .1f)
        {
            currentWatpointIndex++;
            if (currentWatpointIndex >= waypoints.Length)
            {
                currentWatpointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWatpointIndex].transform.position, Time.deltaTime * speed);
    }
}
