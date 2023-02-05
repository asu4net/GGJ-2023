using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AutoPause : MonoBehaviour
{
    public UnityEvent autoPause;
    [SerializeField] private float seconds;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(AutoPauseF), seconds);
    }

    private void AutoPauseF()
    {
        //autoPause.Invoke();
        SceneManager.LoadScene("Main Menu");
    }
}
