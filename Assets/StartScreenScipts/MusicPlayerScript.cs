using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicArr;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomTrack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayRandomTrack()
    {
        if (musicArr.Length > 0)
        {
            int randInt = Random.Range(0, musicArr.Length);
            audioSource.clip = musicArr[randInt];
            audioSource.Play();
        }
        else
        {
            Debug.Log("No audio available");
        }
        }

}
