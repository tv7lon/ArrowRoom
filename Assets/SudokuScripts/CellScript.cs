using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellScript : Selectable
{
    [SerializeField] private TMP_Text numberText;
    private int cellNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayText()
    {
        if (cellNumber <= 0)
        {
            numberText.GetComponent<TMP_Text>().text = "";
        }
        else
        {
            numberText.GetComponent<TMP_Text>().text = cellNumber + "";
        }
    }

    public void SetNumber(int numberIn)
    {
        cellNumber = numberIn;
        DisplayText();
    }
}
