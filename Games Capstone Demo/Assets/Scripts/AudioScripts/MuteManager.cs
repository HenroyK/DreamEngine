using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteManager : MonoBehaviour
{
    private bool isMuted;
    
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        //AudioListener.pause = isMuted;
        if (isMuted)
        {
            AudioListener.volume = 0;
        }
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        //AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
        if (isMuted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Mute"))
        {
            MutePressed();
        }
    }
}
