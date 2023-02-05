using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Root(RootRegion.QualityTiming qualityTiming)
    {
        if (qualityTiming == RootRegion.QualityTiming.Bad)
        {
            animator.SetTrigger("Fail");
            return;
        }

        animator.SetTrigger("Good");
    }
}
