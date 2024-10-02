using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    private Button closeButton;
    private Button popupButton;
    DangerBar db;
    [SerializeField] private int decreasedAmount;
    [SerializeField] private Sprite[] popupArray;
    private int dangerMultiplyer;
    private Image thisImage;
    PopupGenerator pg;

    void Start()
    {
        closeButton= this.gameObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(DestroyPopup);
        closeButton = this.gameObject.transform.Find("PopupButton").GetComponent<Button>();     
        closeButton.onClick.AddListener(ClickedPopup);
        db = GameObject.Find("DangerBar").GetComponent<DangerBar>();
        pg = GameObject.Find("Scripts").GetComponent<PopupGenerator>();

        //set image
        thisImage = this.gameObject.GetComponent<Image>();
        setImage();
    }

    //if you correctly close a popup
    private void DestroyPopup()
    {
        db.DecreaseDanger(decreasedAmount);
        Destroy(this.gameObject);
        //reset the multiplier
        pg.SetDangerMultiplier(10);
        pg.CloseSound();
    }
    private void ClickedPopup()
    {
        pg.CloseSound();
        for (int i = 0; i < pg.GetDangerMultipler(); i++) {
            db.IncreaseDanger();
        }
        Destroy(this.gameObject);
        pg.SetDangerMultiplier(pg.GetDangerMultipler()+10);
    }

    private void setImage()
    {
        thisImage.sprite = popupArray[Random.Range(0,popupArray.Length)];
    }
}
