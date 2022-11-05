using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteManager : MonoBehaviour
{
    private bool isMuted;
    
    // Get mute state on start
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        //AudioListener.pause = isMuted;
        if (isMuted)
        {
            AudioListener.volume = 0;
        }
    }
    // Toggle mute state
    public void MutePressed()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
        if (isMuted)
        { AudioListener.volume = 0; }
        else
        { AudioListener.volume = 1; }
    }
    void Update()
    {
        if (Input.GetButtonDown("Mute"))
        {
            MutePressed();
        }
    }
}
