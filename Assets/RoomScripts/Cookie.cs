using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    [SerializeField] private float expiryTime;
    SpawnCookie spawnCookieMethod;

    void Update()
    {
        expiryTime -= Time.deltaTime;
        if (expiryTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            transform.position = worldPosition;
        }
    }
}
