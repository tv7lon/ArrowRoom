using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    SnatcherMain ms;
    [SerializeField] private float superSpeed;
    void Start()
    {
        //get existing mainScript in the same scene 
        ms = GameObject.Find("Scripts").GetComponent<SnatcherMain>();
        int randChance = Random.Range(0, 100);
        if (randChance >= 85)
        {
            //super speed
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = superSpeed;
        }
    }


    private void OnTriggerEnter2D(Collider2D appleCollision)
    {
        if (appleCollision.gameObject.name.Equals("Basket"))
        {
            ms.AddScore();
            Destroy(this.gameObject);
        }
        //dropped an apple
        else if (appleCollision.gameObject.name.Equals("GroundBorder"))
        {
            ms.MinusHeart();
            Destroy(this.gameObject);

        }
    }
}
