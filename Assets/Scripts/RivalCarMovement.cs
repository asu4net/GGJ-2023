using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RivalCarMovement : MonoBehaviour
{
    [SerializeField] private RivalCarWaypoints waypointsSlot;
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float badDecrease = 1.0f;
    [SerializeField] private float OkIncrease = 8.0f;
    [SerializeField] private float GoodIncrease = 9.0f;
    [SerializeField] private float PerfectIncrease = 10f;
    [SerializeField] private float distanceToChangeWaypoint = 0.1f;

    [SerializeField] private float speedLoss = 0.001f;
    [SerializeField] private float minimumSpeed = 4.0f;
    [SerializeField] private float maximumSpeed = 15f;

    [SerializeField][Range(0, 1)] private float lookAtSmoothValue = 0.5f;

    private int currWaypoint = 0;
    private float defaultSpeed = 0;

    public void OnRootCompletion(RootRegion.QualityTiming qualityTiming)
    {

        switch (qualityTiming)
        {
            case RootRegion.QualityTiming.Bad:
                //StartCoroutine(AsyncSpeedBoost(badSpeed, 1.5f));
                speed -= badDecrease;
                break;

            case RootRegion.QualityTiming.Ok:
                //StartCoroutine(AsyncSpeedBoost(OkSpeed, 1.5f));
                speed += OkIncrease;
                break;

            case RootRegion.QualityTiming.Good:
                //StartCoroutine(AsyncSpeedBoost(GoodSpeed, 1.5f));
                speed += GoodIncrease;
                break;

            case RootRegion.QualityTiming.Perfect:
                //StartCoroutine(AsyncSpeedBoost(PerfectSpeed, 1.5f));
                speed += PerfectIncrease;
                break;

            default:
                break;
        }

        if (speed > maximumSpeed) speed = maximumSpeed;
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
            waypointsSlot = FindObjectOfType<RivalCarWaypoints>();
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


    private void FixedUpdate()
    {
        speed -= speedLoss;

        if (speed < minimumSpeed) speed = minimumSpeed;
    }
}