using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryImageScript : MonoBehaviour
{
    [SerializeField]
    private Sprite SuccessSprite;
    [SerializeField]
    private Sprite FailSprite;

    public float timeToFade = 1;
    private float elapsedTime = 0;
    private bool startFade = false;
    [SerializeField]
    private float fade = 1;
    [SerializeField]
    private Image image;
    private void OnEnable()
    {
        if (PlayerStats.levelScoreCount >= (PlayerStats.pickupMax*0.7))
        {
            image.sprite = SuccessSprite;
        }
        else
        {
            image.sprite = FailSprite;
        }
        fade = Mathf.Lerp(0, 1, 0);
        image.color = new Color(255, 255, 255, fade);
        startFade = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            elapsedTime += Time.deltaTime;
            fade = Mathf.Lerp(0, 1, Mathf.Clamp(elapsedTime / timeToFade, 0, 1));
            image.color = new Color(255,255,255,fade);
        }
    }
}
