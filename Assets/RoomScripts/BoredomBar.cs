using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoredomBar : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    private int boredomValue { get; set; }
    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseBoredom", delay, interval);
    }

    private void IncreaseBoredom()
    {
        slider.value++;
        boredomValue = (int)slider.value;
    }
}
