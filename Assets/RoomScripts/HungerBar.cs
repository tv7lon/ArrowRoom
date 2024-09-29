using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    private int hungerValue;
    // Start is called before the first frame update
    void Start()
    {
        //calls a method after a certain amount of seconds, every certain seconds
        InvokeRepeating("IncreaseHunger", delay, interval);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //set public so it can be called outside this class
    public void IncreaseHunger()
    {
        slider.value++;
        hungerValue = (int)slider.value;
    }
    public void DecreaseHunger(int decreasedAmount)
    {
        slider.value= slider.value - decreasedAmount;
        UserManager.Instance.TotalCookiesFed++;
    }


}
