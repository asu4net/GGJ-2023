using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
        if (rivalLap == Laps + 1)
        {
            RivalEnd.Invoke();
            SceneManager.LoadScene("Loss_scene", LoadSceneMode.Single);
        }
        else if (playerLap == Laps + 1)
        {
            PlayerEnd.Invoke();
            SceneManager.LoadScene("Victory_scene", LoadSceneMode.Single);
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

    public void ResetGame()
    {
        SceneManager.LoadScene("Race_concept", LoadSceneMode.Single);
    }
}
