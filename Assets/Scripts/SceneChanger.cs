using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial_scene");
    }
    public void GoToRace()
    {
        SceneManager.LoadScene("Race_concept");
    }
}
