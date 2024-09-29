using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    [SerializeField] private int dangerIncrement;
    private int dangerValue;

    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseDanger", delay, interval);
    }

    public void IncreaseDanger()
    {
        slider.value= slider.value + dangerIncrement;
        dangerValue = (int)slider.value;
    }
    public void DecreaseDanger(int decreasedAmount)
    {
        slider.value = slider.value - decreasedAmount;
    }


}
