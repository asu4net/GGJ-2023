using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class FeedbackMessager : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject BadMessage;
    [SerializeField] private GameObject OkMessage;
    [SerializeField] private GameObject GoodMessage;
    [SerializeField] private GameObject PerfectMessage;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableMessage(RootRegion.QualityTiming qualityTiming)
    {
        BadMessage.SetActive(false);
        GoodMessage.SetActive(false);
        OkMessage.SetActive(false);
        PerfectMessage.SetActive(false);

        animator.SetTrigger("Root");

        switch (qualityTiming)
        {
            case RootRegion.QualityTiming.Bad:
                BadMessage.SetActive(true);
                break;

            case RootRegion.QualityTiming.Ok:
                OkMessage.SetActive(true);
                break;

            case RootRegion.QualityTiming.Good:
                GoodMessage.SetActive(true);
                break;

            case RootRegion.QualityTiming.Perfect:
                PerfectMessage.SetActive(true);
                break;

            default:
                break;
        }
    }
}
