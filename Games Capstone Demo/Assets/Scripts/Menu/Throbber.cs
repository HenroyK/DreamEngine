using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throbber : MonoBehaviour
{
    //Vars
    public Sprite[] sprites;
    public Image image;

    private float fps = 14;
    private int frame = 0;

    // Start is called before the first frame update
    void Awake()
    {
        image.sprite = sprites[frame];
        StartCoroutine(Throb());
    }

    IEnumerator Throb()
    {
        while (gameObject.activeSelf != false)
        {
            //Animation because unity doesnt do this easy
            frame += 1;
            if (frame >= sprites.Length)
                frame = 0;
            image.sprite = sprites[frame];
            yield return new WaitForSeconds(1/fps);
        }
    }
}
