using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{

    [SerializeField] private GameObject cookiePrefab;
    private int numCookiesSpawned;
    [SerializeField] private int maxCookiesAvailable;
    [SerializeField] private int resetTime;
    [SerializeField] private TMP_Text maxCookiesLabel; 

    public void OnButtonClick()
    {
        //prevents a cookie from spawning when space is pressed; numCookies initially set to 0 so make < maxCookiesAvailable
        if (!Input.GetKeyDown(KeyCode.Space)&& numCookiesSpawned<maxCookiesAvailable)
        {
            Vector2 mousePos = Input.mousePosition;

            //nearclipplane is = closest distance to camera + ensures correct depth away from camera 
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            Instantiate(cookiePrefab, new Vector3(worldPosition.x, worldPosition.y, -5f), Quaternion.identity);
            numCookiesSpawned++;
        }
        else if (!Input.GetKeyDown(KeyCode.Space))
        {
            //call the method that will wait a few seconds then call method to set numcookiesspawned to 0 again
            maxCookiesLabel.text = "Wait a few seconds!";
            Invoke("ResetCookiesSpawned", resetTime);
        }
            
        
       
    }

    private void ResetCookiesSpawned()
    {
        numCookiesSpawned = 0;
        maxCookiesLabel.text = "";
    }


}
