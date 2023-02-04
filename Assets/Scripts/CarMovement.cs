using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private CarWaypoints waypointsSlot;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float distanceToChangeWaypoint = 0.1f;
    [SerializeField] [Range(0, 1)] private float lookAtSmoothValue = 0.5f;
    
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
        
        if (Vector3.Distance(transform.position, destination) <= distanceToChangeWaypoint)
            currWaypoint++;
        
        if (waypointsSlot.Count() == currWaypoint)
            currWaypoint = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(currentDir, Vector3.up), lookAtSmoothValue);
    }
}