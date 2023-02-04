using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private LayerMask player;

    public UnityEvent GoalCrossed;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            GoalCrossed.Invoke();
        }
    }
}
