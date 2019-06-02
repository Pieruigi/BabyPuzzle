using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField]
    public Sprite mutedSprite;


    AudioManager audioManager;

    Sprite notMutedSprite;

    private void Awake()
    {
        notMutedSprite = GetComponent<Image>().sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteOnOff()
    {
        audioManager.MusicOnOff();

        if (audioManager.IsMuted)
            GetComponent<Image>().sprite = mutedSprite;
        else
        {
            audioManager.PlayClick();
            GetComponent<Image>().sprite = notMutedSprite;

        }
            
    }
}
