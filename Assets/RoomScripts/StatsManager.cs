using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private bool panelIsActive;
    private string sessionStatsPath;
    private string scoresPath;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TMP_Text longestSessionLabel;
    [SerializeField] private TMP_Text cookiesFedLabel;
    [SerializeField] private TMP_Text highScoreLabel;
    [SerializeField] private TMP_Text coinsLabel;
    [SerializeField] private TMP_Text quickestTimeLabel;
    [SerializeField] private TMP_Text totalApplesCaughtLabel;
    private string activeUser;
    void Start()
    {
        statsPanel.SetActive(false);
        panelIsActive = false;

        //set numcookies and coins
        if (!UserManager.Instance.HasStartData)
        {
            UserManager.Instance.AssignStartData();
            UserManager.Instance.LoadHighScore();
        }
    }

    public void TogglePanel()
    {
        statsPanel.SetActive(!panelIsActive);
        panelIsActive = !panelIsActive;

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


