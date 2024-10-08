using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserManager : MonoBehaviour
{

    public static UserManager Instance { get; private set; }
    public string ActiveUser { get; set; }
    public float SessionTimeStart { get; set; }
    public float SessionTimeEnd { get; set; }
    public int TotalCookiesFed { get; set; }
    public int Coins { get; set; }
    public int HighScore { get; set; }
    public bool HasStartData { get; set; }
    public float LongestSession { get; set; }
    public bool HasDog { get; set; }
    public bool HasCat { get; set; }
    public float QuickestTime { get; set; }
    public int TotalApplesCaught { get; set; }

    private string sessionStatsPath;
    private string scoresPath;
    private string inventoriesPath;
    //exe before start method
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sessionStatsPath = Application.dataPath + "/sessionstats.txt";
        scoresPath = Application.dataPath + "/scores.txt";
        inventoriesPath = Application.dataPath + "/inventories.txt";
    }
    public void AssignStartData()
    {
        HasStartData = true;
        //time taken to solve unlikely to be more than 24 hours
        QuickestTime = 86400;
        if (!File.Exists(sessionStatsPath))
        {
            Debug.Log("File not found. Cannot load start values.");
        }
        else
        {

            string[] lineArray = null;
            string currentLine;
            using (StreamReader fileReader = new StreamReader(sessionStatsPath))
            {
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    lineArray = currentLine.Split('#');
                    if (lineArray[0].Equals(ActiveUser))
                    {
                        if (float.Parse(lineArray[1]) > LongestSession)
                        {
                            LongestSession = float.Parse(lineArray[1]);
                        }
                        if (float.Parse(lineArray[4]) < QuickestTime)
                        {
                            QuickestTime = float.Parse(lineArray[4]);
                        }
                        TotalCookiesFed = int.Parse(lineArray[2]);
                        Coins = int.Parse(lineArray[3]);
                    }
                }
            }
        }
    }

    public void LoadScore()
    {

        if (!File.Exists(scoresPath))
        {
            Debug.Log("File not found. Cannot load score values.");
        }
        else
        {
            string[] lineArray = null;
            string currentLine;
            using (StreamReader fileReader = new StreamReader(scoresPath))
            {
                while ((currentLine = fileReader.ReadLine()) != null)
                {
                    lineArray = currentLine.Split("#");
                    if (lineArray[0].Equals(ActiveUser) && int.Parse(lineArray[1]) > HighScore)
                    {
                        HighScore = int.Parse(lineArray[1]);
                    }
                    TotalApplesCaught += int.Parse(lineArray[1]);
                }
            }
        }
    }

    public void LoadInventory()
    {

        if (!File.Exists(inventoriesPath))
        {
            Debug.Log("File not found. Cannot load inventory.");
        }
        else
        {
            string[] lineArray = null;
            string currentLine;
            using (StreamReader fileReader = new StreamReader(inventoriesPath))
            {
                while ((currentLine = fileReader.ReadLine()) != null)
                {


                    lineArray = currentLine.Split("#");
                    if (lineArray[0].Equals(ActiveUser) && lineArray[1].Equals("dog"))
                    {
                        HasDog = true;
                    }
                    else if (lineArray[0].Equals(ActiveUser) && lineArray[1].Equals("cat"))
                    {
                        HasCat = true;
                    }
                }
            }
        }
    }
}
