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
    // Start is called before the first frame update
    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseBoredom", delay, interval);
    }

    public void IncreaseBoredom()
    {
        slider.value++;
        boredomValue = (int)slider.value;
    }
    public void DecreaseBoredom(int decreasedAmount)
    {
        slider.value = slider.value - decreasedAmount;
    }

   
}
