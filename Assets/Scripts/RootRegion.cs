using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class RootRegion : MonoBehaviour
{
    [SerializeField] private Transform endRegion;


    [Header("Inputs")]
    [SerializeField] private KeyCode RootButton;
    [SerializeField] private KeyCode SecondaryRootButton;

    [Header("Thresholds")]
    [SerializeField] private float OkThreshold = 6f;
    [SerializeField] private float GoodThreshold = 4f;
    [SerializeField] private float PerfectThreshold = 2f;
    [SerializeField] private float holdTimeThreshold = 0.5f;

    [Header("Speed Boosts")]

    public UnityEvent<QualityTiming> OnRootCompletion;
    public UnityEvent OnRootPress;
    //public UnityEvent OnRootRelease;

    private Transform Car;

    private bool isRooting = false;
    private bool canRoot = false;

    private bool hasRooted = false;

    private float heldTime = 0f;
    private float lastHeldTime = 0f;


    public enum QualityTiming
    {
        Bad,
        Ok,
        Good,
        Perfect
    }

    // Start is called before the first frame update
    void Start()
    {
        Car = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (!canRoot) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (!isRooting)
                    {
                        isRooting = true;
                        hasRooted = true;
                        OnRootPress.Invoke();
                    }
                    lastHeldTime = Time.fixedTime;
                break;

                case TouchPhase.Ended:
                    if (isRooting && hasRooted)
                    {
                        heldTime = Time.fixedTime - lastHeldTime;

                        if (heldTime < holdTimeThreshold)
                        {
                            OnRootCompletion.Invoke(QualityTiming.Bad);
                            canRoot = false;
                            hasRooted = true;
                            return;
                        }

                        QualityTiming timing = evaluateGoodnessOfTiming(Car);
                        OnRootCompletion.Invoke(timing);

                        canRoot = false;
                    }
                    break;


            }
        }
        if (Input.GetKeyUp(RootButton) || Input.GetKeyUp(SecondaryRootButton))
        {
            if (isRooting && hasRooted)
            {
                heldTime = Time.fixedTime - lastHeldTime;

                if (heldTime < holdTimeThreshold)
                {
                    OnRootCompletion.Invoke(QualityTiming.Bad);
                    canRoot = false;
                    hasRooted = true;
                    return;
                }

                QualityTiming timing = evaluateGoodnessOfTiming(Car);
                OnRootCompletion.Invoke(timing);

                canRoot = false;
            }

            return;
        }


        if (Input.GetKeyDown(RootButton) || Input.GetKeyDown(SecondaryRootButton))
        {
            if (!isRooting)
            {
                isRooting = true;
                hasRooted = true;
                OnRootPress.Invoke();
            }
            lastHeldTime = Time.fixedTime;
        }
    }

    QualityTiming evaluateGoodnessOfTiming(Transform carPosition)
    {
        float distance = (endRegion.position - carPosition.position).magnitude;

        if (distance > OkThreshold) return QualityTiming.Bad;
        if (distance > GoodThreshold) return QualityTiming.Ok;
        if (distance > PerfectThreshold) return QualityTiming.Good;

        return QualityTiming.Perfect;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Car")) return;
        canRoot = true;
        hasRooted = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Car")) return;
        if (!hasRooted && canRoot || isRooting && !hasRooted)
        {
            OnRootCompletion.Invoke(QualityTiming.Bad);
            canRoot = false;
        }
        canRoot = false;
        hasRooted = false;
        isRooting = false;
    }
}
