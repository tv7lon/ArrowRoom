using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HelpScript : MonoBehaviour
{
    [SerializeField] private GameObject helpImg;
    bool isActive;
    public void ToggleHelp()
    {
        helpImg.SetActive(!isActive);
        isActive = !isActive;
    }
}
