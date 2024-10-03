using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoredomBar : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseBoredom", delay, interval);
    }

    private void IncreaseBoredom()
    {
        this.GetComponent<Slider>().value++;
    }
}
