using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_Text outputLabel;
    [SerializeField] private TMP_Text passInfoText;
    private string accountsPath;

    void Start()
    {
        accountsPath = Application.dataPath + "/accounts.txt";
    }

    public void AddAccount()
    {
        char forbiddenChar = '#';

        if (!File.Exists(accountsPath))
        {
            Debug.Log("File not found. Cannot save username and password");
        }
        else if (usernameField.text.Replace(" ", "") == "" || passwordField.text.Replace(" ", "") == "")
        {
            outputLabel.text = "Uh oh! You forgot to enter something...";
        }
        else if (hasHashes(forbiddenChar, usernameField.text) || hasHashes(forbiddenChar, passwordField.text))
        {
            outputLabel.text = "No hashes are allowed!!!";
        }
        else
        {
            LoginManager lm = new LoginManager();
            if (IsStrongPass(passwordField.text) && !lm.IsDeadAccount(usernameField.text))
            {
                using (StreamWriter writer = new StreamWriter(accountsPath, true))
                {
                    writer.WriteLine(usernameField.text + "#" + passwordField.text);
                    outputLabel.text = "Account added ^-^";
                }
            }
            else if (lm.IsDeadAccount(usernameField.text))
            {
                outputLabel.text = "This username has already been used sorry!";

            }
            else
            {
                outputLabel.text = "Your password is very guessable right now :( try a stronger one!";
                highlightText();
            }

        }

    }

    private Boolean IsStrongPass(string password)
    {
        int minChars = 8;
        Boolean hasDigit = false, hasUpper = false, hasLower = false, hasSpecial = false;
        for (int i = 0; i < password.Length; i++)
        {
            char c = password[i];
            if (Char.IsDigit(c))
            {
                hasDigit = true;
            }
            else if (Char.IsUpper(c))
            {
                hasUpper = true;
            }
            else if (Char.IsLower(c))
            {
                hasLower = true;
            }
            else
            {
                hasSpecial = true;
            }

        }
        if (hasDigit && hasUpper && hasLower && hasSpecial && password.Length >= minChars)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //have to ensure no #s otherwise will affect txtfile
    private Boolean hasHashes(char forbiddenSpecial, string input)
    {
        Boolean hasHashes = false;
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (c == forbiddenSpecial)
            {
                hasHashes = true;
                break;
            }
        }
        return hasHashes;
    }

    private void highlightText()
    {
        string textToColour = " password with 8 letters, capitals, lowercases and of course, numbers! ";
        string originalText = passInfoText.text;
        string colouredText = "";
        string colorHex = "C4FF7F";

        //interpolation string: changes the string to have settings in itself + textmeshpro will convert the settings (inthe string) to the text being the hex colour  
        colouredText = originalText.Replace(textToColour, $"<color=#{colorHex}>{textToColour}</color>");
        passInfoText.text = colouredText;

    }
}
