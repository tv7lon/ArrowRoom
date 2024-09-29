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
            UserManager.Instance.HasStartData = true;
        }
    }

    public void TogglePanel()
    {
        //each time shopPanel is toggled it must read from usermanager instance
        statsPanel.SetActive(!panelIsActive);
        panelIsActive = !panelIsActive;
       
    }

    private void Update()
    {
        if (panelIsActive)
        {
            cookiesFedLabel.text = "Cookies fed: " + UserManager.Instance.TotalCookiesFed;
            coinsLabel.text = "$ " + UserManager.Instance.Coins;
            highScoreLabel.text = "High score: " + UserManager.Instance.HighScore;
            longestSessionLabel.text = "Longest session: " + Math.Round(UserManager.Instance.LongestSession / 60, 2) + " min";
        }
    }
}

  
