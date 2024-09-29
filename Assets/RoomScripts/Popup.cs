using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    private Button closeButton;
    private Button popupButton;
    DangerBar DangerBar;
    [SerializeField] private int decreasedAmount;
    [SerializeField] private Sprite[] popupArray;
    private int dangerMultiplyer;
    private Image thisImage;
    PopupGenerator PopupGenerator;

    void Start()
    {
        closeButton= this.gameObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(DestroyPopup);
        closeButton = this.gameObject.transform.Find("PopupButton").GetComponent<Button>();     
        closeButton.onClick.AddListener(ClickedPopup);
        DangerBar = GameObject.Find("DangerBar").GetComponent<DangerBar>();
        thisImage = this.gameObject.GetComponent<Image>();
        setImage();
        dangerMultiplyer = 10;
        PopupGenerator = GameObject.Find("Scripts").GetComponent<PopupGenerator>();

    }

    //if you correctly close a popup
    private void DestroyPopup()
    {
        DangerBar.DecreaseDanger(decreasedAmount);
        Destroy(this.gameObject);
        dangerMultiplyer = 1;
        PopupGenerator.CloseSound();
    }
    private void ClickedPopup()
    {
        PopupGenerator.CloseSound();
        for (int i = 0; i < dangerMultiplyer; i++) {
            DangerBar.IncreaseDanger();
        }
        Destroy(this.gameObject);
        dangerMultiplyer+=10;
    }

    private void setImage()
    {
        thisImage.sprite = popupArray[Random.Range(0,popupArray.Length)];
    }
}
