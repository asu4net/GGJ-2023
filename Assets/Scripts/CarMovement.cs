using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private CarWaypoints waypointsSlot;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float distanceToChangeWaypoint = 0.1f;
    [SerializeField][Range(0, 1)] private float lookAtSmoothValue = 0.5f;

    private int currWaypoint = 0;
    private float defaultSpeed = 0;

    public void OnRootCompletion(RootRegion.QualityTiming qualityTiming)
    {

        switch (qualityTiming)
        {
            case RootRegion.QualityTiming.Bad:
                StartCoroutine(AsyncSpeedBoost(6f, 2f));
                break;

            case RootRegion.QualityTiming.Ok:
                StartCoroutine(AsyncSpeedBoost(12f, 2f));
                break;

            case RootRegion.QualityTiming.Good:
                StartCoroutine(AsyncSpeedBoost(16f, 2f));
                break;

            case RootRegion.QualityTiming.Perfect:
                StartCoroutine(AsyncSpeedBoost(22f, 2f));
                break;

            default:
                break;
        }
    }

    private IEnumerator AsyncSpeedBoost(float newSpeed, float time)
    {
        speed = newSpeed;
        yield return new WaitForSeconds(time);
        speed = defaultSpeed;
    }

    private void Awake()
    {
        defaultSpeed = speed;
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

    private void Update()
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