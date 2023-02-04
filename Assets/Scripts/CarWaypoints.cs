using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class CarWaypoints : MonoBehaviour
{
    List<Transform> wayPoints = new();
    [SerializeField] Color debugColor = Color.green;
    [SerializeField] float sphereRadius = 0.5f;
    
    void Awake()
    {
        FillWaypoints();
    }
    
    void Update()
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
    
    void FillWaypoints()
    {
        wayPoints.Clear();
        foreach (Transform child in transform)
        {
            wayPoints.Add(child);
            child.name = "Waypoint " + wayPoints.IndexOf(child);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        if (wayPoints.Count > 0)
            wayPoints.ForEach(o => Gizmos.DrawSphere(o.position, sphereRadius));
    }
}