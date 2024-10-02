using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DisplayDeadAccounts : MonoBehaviour
{
    [SerializeField] private TMP_Text usersLabel;
    private string deadAccountsPath;
    void Start()
    {
        deadAccountsPath = Application.dataPath + "/deadaccounts.txt";
        usersLabel.text = LoadDeadAccounts().Substring(0, LoadDeadAccounts().Length - 2);
    }

    private string LoadDeadAccounts()
    {
        string output = "";
        if (!File.Exists(deadAccountsPath))
        {
            Debug.Log("File not found. Cannot load dead accounts.");
        }
        else
        {
            string currentLine;
            using (StreamReader fileReader = new StreamReader(deadAccountsPath))
            {
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    output += currentLine + ", ";
                }
            }
        }
        return output;
    }
}
