using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : MonoBehaviour
{
    [SerializeField] private float basketSpeed;
    [SerializeField] private Rigidbody2D basketRb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        basketRb.velocity = new Vector2(horizontalInput * basketSpeed, basketRb.velocity.y);
    }

}
