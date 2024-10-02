using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    SnatcherMain ms;

    void Start()
    {
        ms = GameObject.Find("Scripts").GetComponent<SnatcherMain>();
    }

    private void OnTriggerEnter2D(Collider2D bombCollision)
    {
        if (bombCollision.gameObject.name.Equals("Basket"))
        {
            //minus a life 
            ms.MinusHeart();
            Destroy(this.gameObject);

        }
        else if (bombCollision.gameObject.name.Equals("GroundBorder"))
        {
            Destroy(this.gameObject);
        }
    }


}
