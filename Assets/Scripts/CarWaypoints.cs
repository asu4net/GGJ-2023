using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class CarWaypoints : MonoBehaviour
{
    private List<Transform> wayPoints = new();
    [SerializeField] private Color debugColor = Color.green;
    [SerializeField] private float sphereRadius = 0.5f;
    
    private void Awake()
    {
        FillWaypoints();
    }

    public Vector3 GetWaypointPos(int index)
    {
        return wayPoints[index].position;
    }

    public int Count()
    {
        return wayPoints.Count;
    }
    
    private void Update()
    {
        if (!Application.isPlaying)
            FillWaypoints();
        Transform prevWaypoint = null;
        wayPoints.ForEach(o =>
        {
            if (prevWaypoint)
                Debug.DrawLine(prevWaypoint.position, o.position, debugColor);
            prevWaypoint = o;
        });
    }
    
    private void FillWaypoints()
    {
        wayPoints.Clear();
        foreach (Transform child in transform)
        {
            wayPoints.Add(child);
            child.name = "Waypoint " + wayPoints.IndexOf(child);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        if (wayPoints.Count > 0)
            wayPoints.ForEach(o => Gizmos.DrawSphere(o.position, sphereRadius));
    }
}