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
    [SerializeField] private GameObject cookieImg;
    [SerializeField] private GameObject boredomImg;
    [SerializeField] private GameObject dangerImg;
    [SerializeField] private Sprite skullImg;
    [SerializeField] private AudioSource warningSound;
    private string barValuesPath;

    private string deadAccountsPath;
    private string activeUser;
    private bool wroteDeadUser;

    void Start()
    {
        barValuesPath = Application.dataPath + "/barvalues.txt";
        deadAccountsPath = Application.dataPath + "/deadaccounts.txt";
        activeUser = UserManager.Instance.ActiveUser;
        LoadValues();
    }

    private void Update()
    {
        bool hasFullStats = hungerBar.value == 100 || boredomBar.value == 100 || dangerBar.value == 100;
        if (hasFullStats)
        {
            DoGameOver();
        }
        else
        {
            if (hungerBar.value > 90)
            {
                cookieImg.GetComponent<Image>().sprite = skullImg;
                warningSound.Play();
            }
            else if (boredomBar.value > 90)
            {
                boredomImg.GetComponent<Image>().sprite = skullImg;
                warningSound.Play();
            }
            else if (dangerBar.value > 90)
            {
                dangerImg.GetComponent<Image>().sprite = skullImg;
                warningSound.Play();
            }
        }
    }

    private void DoGameOver()
    {
        AddDeadAccount();
        SceneManagerScript sms = GameObject.Find("Scripts").GetComponent<SceneManagerScript>();
        sms.LoadScene("GameOver");
    }

    private void AddDeadAccount()
    {
        if (!wroteDeadUser && File.Exists(deadAccountsPath))
        {
            using (StreamWriter writer = new StreamWriter(deadAccountsPath, true))
            {
                writer.WriteLine(activeUser);
                wroteDeadUser = true;
            }
        }
        else if (!File.Exists(deadAccountsPath))
        {
            Debug.Log("Cannot save dead user.");
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
                writer.WriteLine(activeUser + "#" + hungerBar.value + "#" + boredomBar.value + "#" + dangerBar.value);
            }
        }
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
    public void RewriteBoredom(int boredomToDecrease)
    {
        //being called from a different scene so assign again - the start of this script will not run again
        barValuesPath = Application.dataPath + "/barvalues.txt";
        int boredomValue = 0;
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
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    lineArray = currentLine.Split('#');
                    if (lineArray[0].Equals(activeUser))
                    {

                        boredomValue = int.Parse(lineArray[2]);
                        if (boredomValue > boredomToDecrease)
                        {
                            boredomValue -= boredomToDecrease;
                        }
                        else
                        {
                            boredomValue = 0;
                        }

                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(barValuesPath, true))
            {
                writer.WriteLine(UserManager.Instance.ActiveUser + "#" + int.Parse(lineArray[1]) + "#" + boredomValue + "#" + int.Parse(lineArray[3]));
            }


        }
    }
}
