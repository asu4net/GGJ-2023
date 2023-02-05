using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource loopSource;
    void Start()
    {
        float length = introSource.clip.length;
        Invoke(nameof(StartLoop), length);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void StartLoop()
    {
        loopSource.Play();
    }
}
