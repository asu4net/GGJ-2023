using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{

    public UnityEvent GoalCrossed;
    public UnityEvent RivalGoalCrossed;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            GoalCrossed.Invoke();
            Debug.Log("Player");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("RivalCar"))
        {
            RivalGoalCrossed.Invoke();
            Debug.Log("Rival");
        }
    }
}
