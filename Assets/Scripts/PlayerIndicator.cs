using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerIndicator : MonoBehaviour
{
    [SerializeField] private Transform visuals;
    public Transform followTarget;
    private Transform cameraTarget;
    
    private void Awake()
    {
        if (Camera.main != null) 
            cameraTarget = Camera.main.transform;
        if (!followTarget)
            followTarget = FindObjectOfType<CarMovement>().transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!followTarget)
            return;
        transform.position = followTarget.position;
        Vector3 dirToCamera = (cameraTarget.position - transform.position).normalized;
        visuals.rotation = Quaternion.LookRotation(dirToCamera);
    }
}
