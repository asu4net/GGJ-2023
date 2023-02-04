using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private CarWaypoints waypointsSlot;
    [SerializeField] private float speed = 10.0f;

    private int currWaypoint = 0;
    
    void Awake()
    {
        if (!waypointsSlot)
            waypointsSlot = FindObjectOfType<CarWaypoints>();
        if (!waypointsSlot)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Waypoints missing");
            #else
            Application.Quit();
            #endif
        }
    }
    
    void Update()
    {
        Vector3 destination = waypointsSlot.GetWaypointPos(currWaypoint);
        Vector3 currentDir = (destination - transform.position).normalized;
        transform.position += currentDir * (speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, destination) <= 0.1f)
            currWaypoint++;
        
        if (waypointsSlot.Count() == currWaypoint)
            currWaypoint = 0;
    }
}
