using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ValuesManager : MonoBehaviour
{

    [SerializeField] private Slider hungerBar;
    [SerializeField] private Slider boredomBar;
    [SerializeField] private Slider dangerBar;
    [SerializeField] private AudioSource gameOverSound;
    private string barValuesPath;
    private string sessionStatsPath;
    private string deadAccountsPath;
   
    LoginManager lm;
    private string activeUser ;
    private bool wroteDeadUser;

    void Start()
    {
        barValuesPath = Application.dataPath + "/barvalues.txt";
        sessionStatsPath = Application.dataPath + "/sessionstats.txt";
        deadAccountsPath = Application.dataPath + "/deadaccounts.txt";
        lm = new LoginManager();
        activeUser = UserManager.Instance.ActiveUser;
        LoadValues();
        
        }

    private void Update()
    {
        if (hungerBar.value ==100|| boredomBar.value==100|| dangerBar.value == 100)
        {
            DoGameOver();
        }
    }

    private void DoGameOver()
    {
        AddDeadAccount();
        SceneManagerScript sms = new SceneManagerScript();
        sms.LoadScene("NewGame");
    }

    private void AddDeadAccount()
    {
        if (!wroteDeadUser&&File.Exists(deadAccountsPath))
        {
            using (StreamWriter writer = new StreamWriter(deadAccountsPath, true))
            {
                writer.WriteLine(activeUser);
                wroteDeadUser = true;
            }
        }
        else
        {
            Debug.Log("Cannot save dead user");
        }
        
        
    }
    public void SaveBarValues()
    {

        if (!File.Exists(barValuesPath))
        {
            Debug.Log("File not found. Cannot save bar values.");
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(barValuesPath, true))
            {
                writer.WriteLine(activeUser + "#" + hungerBar.value + "#" + boredomBar.value +"#" + dangerBar.value);
            }
        }
    }



    public void SaveSessionStats()
    {
        UserManager.Instance.SessionTimeEnd = Time.time;
        if (!File.Exists(sessionStatsPath))
        {
            Debug.Log("File not found. Cannot save session values.");
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(sessionStatsPath, true))
            {
                //in seconds 
                writer.WriteLine(activeUser + "#"+ (UserManager.Instance.SessionTimeEnd - UserManager.Instance.SessionTimeStart) + "#"+  UserManager.Instance.TotalCookiesFed + "#"+ UserManager.Instance.Coins);
            }
        }
        //end this instance 
        Destroy(UserManager.Instance);
    }

    private void LoadValues()
    {

        if (!File.Exists(barValuesPath))
        {
            Debug.Log("File not found. Cannot load bar values.");
        }
        else
        {
              string[] lineArray = null;
    string currentLine;
            using (StreamReader fileReader = new StreamReader(barValuesPath))
            {
                //note** - read line automatically moves the reader to the next line
 
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    lineArray = currentLine.Split('#');
                    
                    if (lineArray[0].Equals(activeUser))
                    {
                        //set the values to the ones stores in places 1,2,3
                        hungerBar.value = int.Parse(lineArray[1]);
                        boredomBar.value = int.Parse(lineArray[2]);
                        dangerBar.value = int.Parse(lineArray[3]);
                    }
                }
            }
        }
    }
}
