using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int Laps = 4;

    public UnityEvent PlayerEnd;
    public UnityEvent RivalEnd;

    private int playerLap = 0;
    private int rivalLap = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rivalLap == Laps)
        {
            RivalEnd.Invoke();
        }
        else if (playerLap == Laps)
        {
            PlayerEnd.Invoke();
        }
    }

    public void IncreaseLap(int which)
    {
        if (which == 0)
        {
            playerLap++;
            return;
        }
        rivalLap++;
    }
}
