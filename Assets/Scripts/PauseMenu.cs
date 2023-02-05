using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent<bool> OnPause;

    private Animator animator;
    private bool gamePaused = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //SwitchPause();
        DontDestroyOnLoad(this);
    }

    public void OnButtonPlay()
    {
        SwitchPause();
    }

    public void OnButtonReset()
    {

    }

    public void OnButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.LogError("Waypoints missing");
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchPause();
    }

    void SwitchPause()
    {
        animator.SetTrigger("Switch");
        gamePaused = !gamePaused;
        OnPause.Invoke(gamePaused);
        Time.timeScale = gamePaused ? 0 : 1;
    }
}
