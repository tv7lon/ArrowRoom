using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{

    [SerializeField] private float delay;
    [SerializeField] private float interval;

    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseHunger", delay, interval);
    }
    private void IncreaseHunger()
    {
        this.GetComponent<Slider>().value++;
    }
    public void DecreaseHunger(int decreasedAmount)
    {
        this.GetComponent<Slider>().value -= decreasedAmount;
        UserManager.Instance.TotalCookiesFed++;
    }


}
