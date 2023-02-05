using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapChanger : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> numbers;
    int counter = 4;

    bool eslaDePrueba = true;

    void Start()
    {
        counter = 4;
        sr = GetComponent<SpriteRenderer>();
        LowerLapVisuals();
    }

    public void LowerLapVisuals()
    {
        if (eslaDePrueba)
        {
            eslaDePrueba = false;
            return;
        }
        if (counter < 0) return;
        sr.sprite = numbers[counter];
        counter--;
    }
}
