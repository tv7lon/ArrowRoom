using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectArr;
    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;

    private float spawnY = 6f; 
    private float startDelay = 3f;
    private float newRatesInterval = 5f;
    [SerializeField] private float spawnFreq;
    [SerializeField] private float gravityMultiplier; 

    [SerializeField] private TMP_Text scoreboard;
    [SerializeField] private Image[] heartArr;
    [SerializeField] private GameObject basket;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TMP_Text gameOverScore;
    private int numHearts = 3; 
    private int score =0;
    private bool gameOver;
    private int bombChance;

    //sounds
    [SerializeField] private AudioSource addHeartSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource loseHeartSound;
    [SerializeField] private AudioSource everyTenSound;
    [SerializeField] private AudioSource caughtAppleSound;

    //textfiles
    private string scoresPath;
    private string barValuesPath; 
    private string activeUser; 

    void Start()
    {
        gameOver = false;
        gameOverCanvas.SetActive(false);
        //every 5 seconds the probability of bombs falling changes 
        InvokeRepeating("GenerateBombChance", startDelay, newRatesInterval);
        InvokeRepeating("SpawnGameObject", startDelay, spawnFreq);

        scoresPath = Application.dataPath + "/scores.txt";
        barValuesPath = Application.dataPath + "/barvalues.txt";
        activeUser = UserManager.Instance.ActiveUser;

    }
 
    private int GenerateBombChance()
    {
        //sometimes bombs have a higher frequency or chance of spawning than apples 
        bombChance = Random.Range(0, 100);
        return bombChance;
    }

    private void SpawnGameObject()
    {
        if (!gameOver)
        {
            float spawnX = Random.Range(leftBorder, rightBorder);
            Vector2 randPos = new Vector2(spawnX, spawnY);
            int chanceGenerated = Random.Range(0, 100);

            if (chanceGenerated > bombChance)
            {
                GameObject bomb = Instantiate(gameObjectArr[1], randPos, Quaternion.identity);
                bomb.GetComponent<Rigidbody2D>().gravityScale += gravityMultiplier * bomb.GetComponent<Rigidbody2D>().gravityScale * Time.deltaTime;
                //adding difficulty 
                if (chanceGenerated >= 85)
                {
                    Instantiate(gameObjectArr[1], (new Vector2(Random.Range(leftBorder, rightBorder), spawnY + 6)), Quaternion.identity);
                }
            }
            else
            {
                GameObject apple = Instantiate(gameObjectArr[0], randPos, Quaternion.identity);
                apple.GetComponent<Rigidbody2D>().gravityScale += apple.GetComponent<Rigidbody2D>().gravityScale * Time.deltaTime;
            }
            if (chanceGenerated % 15 == 0)
            {
                Instantiate(gameObjectArr[2], (new Vector2(Random.Range(leftBorder, rightBorder), spawnY)), Quaternion.identity);
            }
        }
        
    }

    public void AddScore()
    {
        caughtAppleSound.Play();
        score++;
        scoreboard.text = score.ToString(); 
        if(score%10 == 0)
        {
            everyTenSound.Play();
        }
    }

    public void MinusHeart()
    {
        if (!gameOver)
        {
            loseHeartSound.Play();
            numHearts--;
            heartArr[numHearts].color = Color.black;

            if (numHearts < 1)
            {
               DoGameOver();
            }
        }
        
        
    }

    private void DoGameOver()
    {
        gameOver = true;
        gameOverSound.Play();
        Destroy(basket);
        gameOverCanvas.SetActive(true);
        gameOverScore.text = "Score: " + score;
        UserManager.Instance.Coins += score * 50;
        Debug.Log("Coins after win:" + UserManager.Instance.Coins);
        if (score > UserManager.Instance.HighScore) {
            UserManager.Instance.HighScore = score;
        }
        if (score > 0) 
        { 
            SaveScore();
        }
        RewriteBoredom(); 
    }

    public void AddHeart()
    {

        if (numHearts < 3)
        {
            addHeartSound.Play();
            heartArr[numHearts].color = Color.white;
            numHearts++;
        }
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    private void SaveScore()
    {
        if (!File.Exists(scoresPath))
        {
            Debug.Log("File not found. Cannot save values.");
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(scoresPath, true))
            {
                writer.WriteLine(activeUser + "#" + score);
            }
        }
    }

    private void RewriteBoredom()
    {
        Debug.Log("running rewrite boredom");
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
                        Debug.Log("active user =");
                        boredomValue = int.Parse(lineArray[2]);
                        Debug.Log(boredomValue + " before ");
                        if (boredomValue > 20)
                        {
                            boredomValue -= 40;
                            Debug.Log(boredomValue + " after ");
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
                writer.WriteLine(activeUser + "#" + int.Parse(lineArray[1]) + "#" + boredomValue + "#" + int.Parse(lineArray[3]));
            }

           
        }
    }
   
    }
    





