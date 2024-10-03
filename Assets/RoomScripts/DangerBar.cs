using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerBar : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    [SerializeField] private int dangerIncrement;

    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseDanger", delay, interval);
    }

    public void IncreaseDanger()
    {
        this.GetComponent<Slider>().value= this.GetComponent<Slider>().value + dangerIncrement;
    }
    public void DecreaseDanger(int decreasedAmount)
    {
        this.GetComponent<Slider>().value -= decreasedAmount;
    }


}
