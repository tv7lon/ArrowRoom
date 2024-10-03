using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private bool panelIsActive;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TMP_Text longestSessionLabel;
    [SerializeField] private TMP_Text cookiesFedLabel;
    [SerializeField] private TMP_Text highScoreLabel;
    [SerializeField] private TMP_Text coinsLabel;
    [SerializeField] private TMP_Text quickestTimeLabel;
    [SerializeField] private TMP_Text totalApplesCaughtLabel;
    private string sessionStatsPath;
    void Start()
    {
        statsPanel.SetActive(false);
        panelIsActive = false;
        sessionStatsPath = Application.dataPath + "/sessionstats.txt";

        //set numcookies and coins
        if (!UserManager.Instance.HasStartData)
        {
            UserManager.Instance.AssignStartData();
            UserManager.Instance.LoadScore();
        }
    }

    public void TogglePanel()
    {
        statsPanel.SetActive(!panelIsActive);
        panelIsActive = !panelIsActive;

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
                writer.WriteLine(UserManager.Instance.ActiveUser + "#" + (UserManager.Instance.SessionTimeEnd - UserManager.Instance.SessionTimeStart) + "#" + UserManager.Instance.TotalCookiesFed + "#" + UserManager.Instance.Coins + "#" + UserManager.Instance.QuickestTime);
            }
        }
        //end this instance otherwise there will be multiple UserManagers when the user logs in
        Destroy(UserManager.Instance.gameObject);
    }
    private void Update()
    {
        if (panelIsActive)
        {
            cookiesFedLabel.text = "Cookies fed: " + UserManager.Instance.TotalCookiesFed;
            coinsLabel.text = "$ " + UserManager.Instance.Coins;
            highScoreLabel.text = "Apple Snatcher HS: " + UserManager.Instance.HighScore;
            longestSessionLabel.text = "Longest session: " + Math.Round(UserManager.Instance.LongestSession / 60, 2) + " min";
            totalApplesCaughtLabel.text = "Total Apples Caught: " + UserManager.Instance.TotalApplesCaught;
            if (UserManager.Instance.QuickestTime == 86400)
            {
                quickestTimeLabel.text = "Quickest Sudoku Solve: None attempted.";
            }
            else
            {
                quickestTimeLabel.text = "Quickest Sudoku solve: " + Math.Round(UserManager.Instance.QuickestTime / 60, 2) + " min";
            }
        }
    }
}


