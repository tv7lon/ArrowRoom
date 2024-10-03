using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text messageLabel;
    private string messagesPath;
    private string[] messagesArray;
    void Start()
    {
        messagesPath = Application.dataPath + "/messages.txt";
        LoadMessages();
        messageLabel.text = GetRandMessage();
    }


    private void LoadMessages()
    {
        if (File.Exists(messagesPath))
        {
            //puts each line into the message array
            messagesArray = File.ReadAllLines(messagesPath);
        }
        else
        {
            Debug.Log("File not found. Cannot load messages");
        }
    }

    private string GetRandMessage()
    {
        int randInt = Random.Range(0, messagesArray.Length);
        return messagesArray[randInt];
    }
}
