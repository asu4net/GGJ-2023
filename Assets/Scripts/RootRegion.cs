using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RootRegion : MonoBehaviour
{
    [SerializeField] private Transform startRegion;
    [SerializeField] private Transform endRegion;

    [SerializeField] private KeyCode RootButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // PARA EL EVENTO:
        // Velocidad para ir
        // Tiempo de boost

        if (Input.GetKeyDown(RootButton))
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }
}
