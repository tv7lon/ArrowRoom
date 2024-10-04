using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStoreManager : MonoBehaviour
{
    private bool panelIsActive;
    [SerializeField] private GameObject buyDogButton;
    [SerializeField] private GameObject buyCatButton;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] GameObject dogPrefab;
    [SerializeField] GameObject catPrefab;
    [SerializeField] private int petPrice;

    [SerializeField] private TMP_Text dogLabel;
    [SerializeField] private TMP_Text catLabel;
    [SerializeField] private TMP_Text shopLabel;

    private string inventoriesPath; 
    void Start()
    {
        shopPanel.SetActive(false);
        panelIsActive = false;

        inventoriesPath = Application.dataPath + "/inventories.txt";
        UserManager.Instance.LoadInventory();

        if (UserManager.Instance.HasDog)
        {
            SpawnPet("dog");
        }
        if (UserManager.Instance.HasCat)
        {
            SpawnPet("cat");
        }
    }


    public void TogglePanel()
    {
        shopPanel.SetActive(!panelIsActive);
        panelIsActive = !panelIsActive;
    }

    public void BuyPet(string petBought)
    {
        if (UserManager.Instance.Coins >=petPrice) 
        {
            if (!File.Exists(inventoriesPath))
            {
                Debug.Log("File not found. Cannot save bought pet.");
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(inventoriesPath, true))
                {
                    writer.WriteLine(UserManager.Instance.ActiveUser + "#" + petBought);
                }
            }
            UserManager.Instance.Coins-=petPrice;
            SpawnPet(petBought);
        }
        else
        {
            shopLabel.text = "Not enough coins!";
            Invoke("ResetText", 3);
        }
    }
    private void ResetText()
    {
        shopLabel.text = "";
    }

    private void SpawnPet(string petToSpawn)
    {
        //specific co ords
        Vector3 dogSpawn = new Vector3(0f, -3.38f, 0f);
        Vector3 catSpawn = new Vector3(-1.91f, -1.04f, 0f);
        if (petToSpawn.Equals("dog"))
        {
            Instantiate(dogPrefab, dogSpawn, Quaternion.identity);
            buyDogButton.SetActive(false);
            dogLabel.SetText("Dog bought!");
            UserManager.Instance.HasDog = true;
        }
        else if (petToSpawn.Equals("cat"))
        {
            Instantiate(catPrefab, catSpawn, Quaternion.identity);
            buyCatButton.SetActive(false);
            catLabel.text = ("Cat bought!");
            UserManager.Instance.HasCat = true;
        }
    }
}
