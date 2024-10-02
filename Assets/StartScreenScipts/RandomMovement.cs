using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 targetPosition;
    private Animator anim;
    void Start()
    {
        SetRandomPos();
    }

    void Update()
    {
        MoveTowardsRandom();

        if (transform.position == targetPosition)
        {
            SetRandomPos();
        }

        //if going right
        if (targetPosition.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //if going left
        else if (targetPosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }


    private void SetRandomPos()
    {
        /*        not on canvas, cant use transform point 
                world space = actual position in unity scene 
                converts the left / right  boundary of camera, saves in a vector 3 and gets the x value of the vector 3
                0,0,0 bottom left; 1,0,0 bottom right screen*/

        float leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        int randomX = (int)UnityEngine.Random.Range(leftBoundary, rightBoundary);
        targetPosition = new Vector3(randomX, transform.position.y);
    }

    private void MoveTowardsRandom()
    {
        //move this objects transform's position to target with speed as time progresses
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
