using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    SnatcherMain ms;

    void Start()
    {
        ms = GameObject.Find("Scripts").GetComponent<SnatcherMain>();
    }

    private void OnTriggerEnter2D(Collider2D heartCollision)
    {
        if (heartCollision.gameObject.name.Equals("Basket"))
        {
            ms.AddHeart();
            Destroy(this.gameObject);
        }
        else if (heartCollision.gameObject.name.Equals("Ground Border"))
        {
            Destroy(this.gameObject);
        }
        //only destroy object under these collisions 

    }

}
