using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    //need to separate this into two different scripts 
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_Text errorLabel;
    private string accountsPath;
    SceneManagerScript sm;

   
        void Start()
    {
        accountsPath = Application.dataPath + "/accounts.txt";
       
        sm = new SceneManagerScript();
    }

    public void CheckValidUser()
    {
        bool isValid = false;
        if (File.Exists(accountsPath))
        {

            using (StreamReader fileReader = new StreamReader(accountsPath))
            {
                string currentLine;
                while ((currentLine = fileReader.ReadLine()) != null)
                {

                    //splits the line with # into user: [username, password]
                    string[] user = currentLine.Split("#");
                    string correctUsername = user[0];
                    string correctPassword = user[1];

                    isValid = correctUsername.Equals(usernameField.text) && correctPassword.Equals(passwordField.text) && !IsDeadAccount(usernameField.text);
                    if (isValid)
                    {
                        UserManager.Instance.ActiveUser = correctUsername;
                        //get start time
                        UserManager.Instance.SessionTimeStart = Time.time;
                        sm.LoadScene("Room");
                        break;
                    }
                }
                if (IsDeadAccount(usernameField.text))
                {
                    errorLabel.text = "This username has already been used, you cannot restore this account :(";
                }else if(!isValid)
                {
                    errorLabel.text = "Hmm it seems this account doesn't exist or you've entered the wrong details :(";
                }     
            }
        }
        else
        {
            Debug.Log("File not found. Cannot check username and password");
        }

    }
    public bool IsDeadAccount(string userIn)
    {
        string deadAccountsPath = Application.dataPath + "/deadaccounts.txt";
        bool isDead = false;
        if (File.Exists(deadAccountsPath))
        {

            using (StreamReader fileReader = new StreamReader(deadAccountsPath))
            {
                string currentDeadUser;
                while ((currentDeadUser = fileReader.ReadLine()) != null)
                {
                    if (userIn.Equals(currentDeadUser)){
                        isDead = true;
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("File not found. Cannot check dead user");
        }
 
        return isDead;
    }
    }





