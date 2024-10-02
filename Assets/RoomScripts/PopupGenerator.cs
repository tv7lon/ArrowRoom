using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupGenerator : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private int leftBorder;
    [SerializeField] private int rightBorder;
    [SerializeField] private int topBorder;
    [SerializeField] private int bottomBorder;
    [SerializeField] private AudioSource spawnSound;
    [SerializeField] private AudioSource closeSound;

    private int dangerMultiplier;
    public int GetDangerMultipler() { return dangerMultiplier; }
    public void SetDangerMultiplier(int value) { dangerMultiplier = value; }

    void Start()
    {
        InvokeRepeating("SpawnPopup", delay, interval);
        dangerMultiplier = 10;
    }

    private void SpawnPopup()
    {
        //spawn a random gameobject from an array of popupPrefab gameobjects 
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float randX = Random.Range(leftBorder, rightBorder);
        float randY = Random.Range(bottomBorder, topBorder);
        Vector2 randPos = new Vector2(randX, randY);

        GameObject spawnedPopUp = Instantiate(popupPrefab, randPos, Quaternion.identity);
        spawnSound.Play();
        //set as child of canvas so spawns on canvas 
        spawnedPopUp.transform.SetParent(canvasRect.transform, false);

    }

    //close popup sound here bc cannot assign audiosource to prefab 
    public void CloseSound()
    {
        closeSound.Play();
    }
}
