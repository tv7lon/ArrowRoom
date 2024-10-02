using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage rawImg;
    [SerializeField] private float xspeed, yspeed;

    // Update is called once per frame
    void Update()
    {
        rawImg.uvRect = new Rect(rawImg.uvRect.position + new Vector2(xspeed, yspeed) * Time.deltaTime, rawImg.uvRect.size);
    }
}
